using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Analyzer
{
    static class DocumentManager
    {
        public static bool SavePolygon(List<Vertex> polygonVertices, string filename)
        {
            bool success = true;
            System.IO.StreamWriter sw;

            try
            {
                sw = new System.IO.StreamWriter(filename, false);

                sw.WriteLine(polygonVertices.Count);

                foreach (Vertex p in polygonVertices)
                {
                    sw.WriteLine(p.X.ToString() + "," + p.Y.ToString());
                }

                sw.Close();
            }
            catch
            {
                success = false;
            }

            return success;
        }


        public static bool LoadPolygon(out List<Vertex> points, string filename)
        {
            bool success = true;
            System.IO.StreamReader sr = null;

            try
            {
                sr = new System.IO.StreamReader(filename);

                int numPoints;
                numPoints = Int32.Parse(sr.ReadLine());

                points = new List<Vertex>(numPoints);
                string[] corrdinates;

                // Read vertices
                for (int i = 0; i < numPoints; i++)
                {
                    Vertex v = new Vertex();

                    corrdinates = sr.ReadLine().Split(',');
                    v.X = Int32.Parse(corrdinates[0]);
                    v.Y = Int32.Parse(corrdinates[1]);
                    points.Add(v);
                }

            }
            catch
            {
                points = null;
                success = false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }

            return success;
        }
    }
}
