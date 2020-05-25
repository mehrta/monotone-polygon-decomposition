using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Gui
{
    class GraphicManager
    {
        Font _idFont;

        public List<Vertex> PolygonVertices { set; get; }
        public bool IsPolygonClosed { set; get; }
        public Graphics G { set; get; }
        public Pen TrapezoidPen { set; get; }
        public Pen DiagonalPen { set; get; }
        public Pen PolygonPen { set; get; }
        public Size BoundingBox { set; get; }
        public bool ShowTrapezoidId { set; get; }
        public bool ShowTrapezoids { set; get; }
        public bool ShowDiagonals { set; get; }
        public Dll.ExecutionResult_GreedyOrSeidel GreedyOrSeidelExecutionResult;
        public Dll.ExecutionResult_LeePreparata LeePreparataExecutionResult;

        public GraphicManager()
        {
            TrapezoidPen = new Pen(Color.Gray);
            TrapezoidPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            TrapezoidPen.DashPattern = new float[] { 3, 3 };
            DiagonalPen = new Pen(Color.Green);
            PolygonPen = new Pen(Color.Black);
            _idFont = new Font("Arial", 8);
            ShowDiagonals = true;
            ShowTrapezoids = true;
            ShowTrapezoidId = true;
        }

        public void RedrawExecutionResult(DecompositionAlgorithm algorithm)
        {
            // Clear all
            G.Clear(Color.White);

            if (algorithm == DecompositionAlgorithm.Lee_Preparata)
                DrawLeePreparataExecutionResult();
            else
                DrawGreedyOrSeidelExecutionResult();
        }

        public void DrawPolygon(List<Vertex> vertices, bool closedPolygon)
        {
            PointF[] points = new PointF[vertices.Count];

            int i = 0;
            foreach (Vertex v in vertices)
            {
                points[i].X = v.X;
                points[i].Y = BoundingBox.Height - v.Y;

                // Draw small circles on each vertex
                G.DrawEllipse(Pens.Black, v.X - 1, BoundingBox.Height - v.Y - 1, 2, 2);
                i++;
            }

            // Draw polygon
            if (closedPolygon)
            {
                G.FillPolygon(Brushes.Gold, points);
                G.DrawPolygon(PolygonPen, points);
            }
            else
                if (points.Length > 1)
                    G.DrawLines(PolygonPen, points);
        }

        private void DrawGreedyOrSeidelExecutionResult()
        {
            if (GreedyOrSeidelExecutionResult.Trapezoids == null)
                return;

            int h = BoundingBox.Height;
            // Draw Polygon
            DrawPolygon(PolygonVertices, true);

            // Draw trapezoids
            if (ShowTrapezoids)
                foreach (Trapezoid t in GreedyOrSeidelExecutionResult.Trapezoids)
                {
                    G.DrawLine(TrapezoidPen, t.TopLeft.X, h - t.TopLeft.Y, t.TopRight.X, h - t.TopRight.Y);

                    // Draw ID of trapezoid
                    if (ShowTrapezoidId)
                    {
                        string id = t.ID.ToString();
                        PointF idLocation = new Point();
                        SizeF idBox;

                        float midSegmentLenght = ((t.TopRight.X - t.TopLeft.X) + (t.BottomRight.X - t.BottomLeft.X)) / 2;
                        idBox = G.MeasureString(id, _idFont);
                        idLocation.X = ((t.TopLeft.X + t.BottomLeft.X) / 2) + (midSegmentLenght / 2);
                        idLocation.Y = (t.TopLeft.Y + t.BottomLeft.Y) / 2;
                        idLocation.X = idLocation.X - (idBox.Width / 2);
                        idLocation.Y = h - idLocation.Y - (idBox.Height / 2);

                        G.DrawString(id, _idFont, Brushes.Blue, idLocation);
                    }
                }

            // Draw diagonals
            if (ShowDiagonals)
                foreach (LineSegment d in GreedyOrSeidelExecutionResult.Diagonals)
                    G.DrawLine(DiagonalPen,
                        d.Lower.X,
                        h - d.Lower.Y,
                        d.Upper.X,
                        h - d.Upper.Y);

        }

        private void DrawLeePreparataExecutionResult()
        {
            foreach (List<Vertex> polygon in LeePreparataExecutionResult.Components)
            {
                DrawPolygon(polygon, true);
            }
        }

    }
}
