using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Gui
{
    public enum VertexType { Regular, Start, End, Split, Merge }

    public struct Vertex
    {
        public float X, Y;

        public static explicit operator PointF(Vertex v)
        {
            return new PointF(v.X, v.Y);
        }
    }

    public struct VertexTag2
    {
        VertexType type;
        int diagonally_adjecent; // Index of the vertex that is 'diagonally' adjecent with this vertex 
        int adjecent_trapezoid; // Trapezoid that is adjecent with this vertex (only for Split and Merge Vertices)
    }

    public struct LineSegment
    {
        public int UpperIndex;  // index of upper vertex
        public int LowerIndex;  // index of lower vertex
        public Vertex Upper;	 // upper vertex of the line segment
        public Vertex Lower;	 // lower vertex of the line segment
    }

    public struct Trapezoid : IComparable<Trapezoid>
    {
        public int ID;
        public Vertex TopLeft, TopRight, BottomLeft, BottomRight; // vertices of the trapezoid
        public Int32 TopLeftTrap, TopRightTrap, BottomLeftTrap, BottomRightTrap;  // ID of adjecent trapezoids
        public Int32 Left, Right;

        int IComparable<Trapezoid>.CompareTo(Trapezoid other)
        {
            if (this.ID < other.ID)
                return -1;
            else if (this.ID > other.ID)
                return 1;
            else
                return 0;
        }
    }

    public class Polygon
    {
        public List<Vertex> Vertices {set; get;}
        public List<List<Vertex>> Holes { set; get; }

        public Polygon()
        {
            Vertices = new List<Vertex>(100);
            Holes = new List<List<Vertex>>(10);
        }
    }

    public struct XNode : IComparable<XNode>
    {
        public int ID;
        public Int32 Left;
        public Int32 Right;

        int IComparable<XNode>.CompareTo(XNode other)
        {
            if (this.ID < other.ID)
                return -1;
            else if (this.ID > other.ID)
                return 1;
            else
                return 0;
        }
    }

    public struct YNode : IComparable<YNode>
    {
        public int ID;
        public Int32 Top;
        public Int32 Bottom;

        int IComparable<YNode>.CompareTo(YNode other)
        {
            if (this.ID < other.ID)
                return -1;
            else if (this.ID > other.ID)
                return 1;
            else
                return 0;
        }
    }
}
