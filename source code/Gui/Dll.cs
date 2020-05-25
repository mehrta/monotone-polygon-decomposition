using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Gui
{
    public static class Dll
    {
        private struct _ExecutionResult_GreedyOrSeidel
        {
            public int NumMonotoneComponents;
            public int NumMergs;
            public int NumSplits;
            public int NumDiagonals;
            public int NumTrapezoids;
            public int NumXNodes;
            public int NumYNodes;
            public IntPtr Diagonals;
            public IntPtr Trapezoids;
            public IntPtr XNodes;
            public IntPtr YNodes;
            public IntPtr VerticesTags;
        }

        private struct _ExecutionResult_LeePreparata
        {
            public int NumComponents; // Number of y-monotone components
            public IntPtr Components; // y-monotone components (simple polygons)
            public int NumSplits;
            public int NumMerges;
        };

        public struct ExecutionResult_GreedyOrSeidel
        {
            public int NumMonotoneComponents;
            public int NumMerges;
            public int NumSplits;
            public LineSegment[] Diagonals;
            public Trapezoid[] Trapezoids;
            public XNode[] XNodes;
            public YNode[] YNodes;
            public VertexTag2[] VerticesTags;

            public void Reset()
            {
                Diagonals = null;
                Trapezoids = null;
                XNodes = null;
                YNodes = null;
                NumMonotoneComponents = NumMerges = NumSplits = 0;
            }
        }

        public struct ExecutionResult_LeePreparata
        {
            public List<List<Vertex>> Components; // y-monotone components (simple polygons)
            public int NumSplits;
            public int NumMerges;
        };

        public struct SimplePolygon
        {
            public int Size;
            public IntPtr Vertices;
        }

        //
        // MonotoneDecomposition.dll
        //
        const string MONOTONE_DLL_PATH = "MonotoneDecomposition.dll";

        [DllImport(MONOTONE_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetBoundingBox(float Top, float Bottom, float Left, float Right);

        [DllImport(MONOTONE_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern void FreeMemory_Greedy();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="algorithm">0: Tajedini's greedy algorithm, 1: Seidel's algorithm</param>
        /// <param name="vertices">Vertices of the polygon (clockwise order)</param>
        /// <param name="numVertices">Number of vertices</param>
        /// <param name="maxDepthTraverse">Maximum depth of traverse (only used for greedy algorithm)</param>
        /// <returns></returns>
        [DllImport(MONOTONE_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern _ExecutionResult_GreedyOrSeidel Decompose_GreedyOrSeidel(int algorithm, Vertex[] vertices, int numVertices, int maxDepthTraverse);


        //
        // CGAL.dll
        //
        const string CGAL_DLL_PATH = @"cgal.dll";

        [DllImport(CGAL_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern SimplePolygon GenerateRandomPolygon(int size, int xMin, int xMax, int yMin, int yMax, uint randomSeed);

        [DllImport(CGAL_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern void FreePolygon(IntPtr vertices);

        [DllImport(CGAL_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern _ExecutionResult_LeePreparata Decompose_LeePreparata(Vertex[] vertices, int numVertices);

        [DllImport(CGAL_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        private static extern void FreeMemory_LeePreparata(_ExecutionResult_LeePreparata er);


        //
        // Public (wrapper) Methods
        //
        public static ExecutionResult_GreedyOrSeidel DecomposePolygon_GreedyOrSeidel(int algorithm, Vertex[] vertices, int maxDepthTraverse)
        {
            _ExecutionResult_GreedyOrSeidel tmpEr;
            ExecutionResult_GreedyOrSeidel er = new ExecutionResult_GreedyOrSeidel();

            tmpEr = Decompose_GreedyOrSeidel(algorithm, vertices, vertices.Length, maxDepthTraverse);

            er.NumMonotoneComponents = tmpEr.NumMonotoneComponents;
            er.NumMerges = tmpEr.NumMergs;
            er.NumSplits = tmpEr.NumSplits;

            // X Nodes
            er.XNodes = new XNode[tmpEr.NumXNodes];
            for (int i = 0; i < tmpEr.NumXNodes; i++)
                er.XNodes[i] = (XNode)Marshal.PtrToStructure(IntPtr.Add(tmpEr.XNodes, i * Marshal.SizeOf(typeof(XNode))), typeof(XNode));
            Array.Sort<XNode>(er.XNodes);

            // Y Nodes
            er.YNodes = new YNode[tmpEr.NumYNodes];
            for (int i = 0; i < tmpEr.NumYNodes; i++)
                er.YNodes[i] = (YNode)Marshal.PtrToStructure(IntPtr.Add(tmpEr.YNodes, i * Marshal.SizeOf(typeof(YNode))), typeof(YNode));
            Array.Sort<YNode>(er.YNodes);

            // Trapezoid Nodes
            er.Trapezoids = new Trapezoid[tmpEr.NumTrapezoids];
            for (int i = 0; i < tmpEr.NumTrapezoids; i++)
                er.Trapezoids[i] = (Trapezoid)Marshal.PtrToStructure(
                    IntPtr.Add(tmpEr.Trapezoids, i * Marshal.SizeOf(typeof(Trapezoid))), typeof(Trapezoid));
            Array.Sort<Trapezoid>(er.Trapezoids);

            er.VerticesTags = new VertexTag2[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
                er.VerticesTags[i] = (VertexTag2)Marshal.PtrToStructure(
                    IntPtr.Add(tmpEr.VerticesTags, i * Marshal.SizeOf(typeof(VertexTag2))), typeof(VertexTag2));


            er.Diagonals = new LineSegment[tmpEr.NumDiagonals];
            for (int i = 0; i < tmpEr.NumDiagonals; i++)
                er.Diagonals[i] = (LineSegment)Marshal.PtrToStructure(
                    IntPtr.Add(tmpEr.Diagonals, i * Marshal.SizeOf(typeof(LineSegment))), typeof(LineSegment));

            //
            FreeMemory_Greedy();
            return er;
        }

        public static ExecutionResult_LeePreparata DecomposePolygon_LeePreparata(Vertex[] vertices)
        {
            _ExecutionResult_LeePreparata _er;
            ExecutionResult_LeePreparata er;

            er = new ExecutionResult_LeePreparata();
            _er = Decompose_LeePreparata(vertices, vertices.Length);

            er.NumSplits = _er.NumSplits;
            er.NumMerges = _er.NumMerges;
            er.Components = new List<List<Vertex>>(_er.NumComponents);
            for (int i = 0; i < _er.NumComponents; i++)
            {
                SimplePolygon sp;
                List<Vertex> polygon;

                sp = (SimplePolygon)Marshal.PtrToStructure(
                    IntPtr.Add(_er.Components, i * Marshal.SizeOf(typeof(SimplePolygon))), typeof(SimplePolygon));

                polygon = new List<Vertex>(sp.Size);
                for (int j = 0; j < sp.Size; j++)
                {
                    Vertex v;
                    v = (Vertex)Marshal.PtrToStructure(IntPtr.Add(sp.Vertices, j * Marshal.SizeOf(typeof(Vertex))), typeof(Vertex));
                    polygon.Add(v);
                }

                er.Components.Add(polygon);
            }

            return er;
        }

        public static void RandomPolygon(int size, int xMin, int xMax, int yMin, int yMax, uint randomSeed, List<Vertex> polygonVertices)
        {
            SimplePolygon pi;
            Vertex[] vertices;

            pi = GenerateRandomPolygon(size, xMin, xMax, yMin, yMax, randomSeed);
            vertices = new Vertex[pi.Size];

            polygonVertices.Clear();
            for (int i = 0; i < pi.Size; i++)
                polygonVertices.Add(
                    (Vertex)Marshal.PtrToStructure(IntPtr.Add(pi.Vertices, i * Marshal.SizeOf(typeof(Vertex))), typeof(Vertex)));
            Dll.FreePolygon(pi.Vertices);
        }
    }
}