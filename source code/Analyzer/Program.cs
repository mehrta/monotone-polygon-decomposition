using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            int executionTimes;
            float diagonalsAverage_Greedy_K5, diagonalsAverage_Greedy_K10, diagonalsAverage_Greedy_K20, diagonalsAverage_Greedy_K30;
            float diagonalsAverage_Seidel, diagonalsAverage_LeePreparata;
            int diagonalsSum_Seidel, diagonalsSum_LeePreparata;
            int diagonalsSum_Greedy_K5, diagonalsSum_Greedy_K10, diagonalsSum_Greedy_K20, diagonalsSum_Greedy_K30;
            Size BoundingBox; // Boundign box that contains polygon
            List<Vertex> polygonVertices;
            Vertex[] polygonVerticesArr;
            const int GAP = 5; // A gap bettwen bounding box and polygon
            Dll.ExecutionResult_Greedy erGreedy;
            Dll.ExecutionResult_LeePreparata erLeePreparata;
            string fileName;
            Random rnd = new Random();
            bool polygonLoaded;
            DateTime timeStart, timeEnd;
            TimeSpan timeGreedy_K5, timeGreedy_K10, timeGreedy_K20, timeGreedy_K30, timeLeePreparata, timeSeidel;

            //
            // Initialize
            //
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
            executionTimes = 100;
            BoundingBox = new Size(1000000, 1000000);
            Dll.SetBoundingBox(BoundingBox.Height, 0, 0, BoundingBox.Width);
            timeGreedy_K5 = timeGreedy_K10 = timeGreedy_K20 = timeGreedy_K30 = TimeSpan.Zero;
            timeLeePreparata = TimeSpan.Zero;
            timeSeidel = TimeSpan.Zero;

            //
            // Execution and compare algorithms (Greedy, Seidel, Lee_Preparata)
            //
            int[] polygonSize = new int[] { 1000, 1100, 1200, 1300, 1400, 1500 }; //10, 50, 100, 200, 300, 400, 500, 600, 700, 800 900, };
            polygonVertices = new List<Vertex>();

            foreach (int polySize in polygonSize)
            {
                diagonalsSum_Greedy_K10 = diagonalsSum_Greedy_K20 = 0;
                diagonalsSum_Greedy_K30 = diagonalsSum_Greedy_K5 = 0;
                diagonalsSum_Seidel = diagonalsSum_LeePreparata = 0;
                timeGreedy_K5 = timeGreedy_K10 = timeGreedy_K20 = timeGreedy_K30 = TimeSpan.Zero;
                timeLeePreparata = TimeSpan.Zero;
                timeSeidel = TimeSpan.Zero;

                for (int i = 1; i <= executionTimes; i++)
                {
                    fileName = @"d:\poly\Polygon-" + polySize.ToString() + "-" + i.ToString("D2") + ".txt";

                    //// Generate random polygons
                    //if (System.IO.File.Exists(fileName))
                    //    continue;
                    //Dll.RandomPolygon(polySize, GAP, BoundingBox.Width - GAP, GAP, BoundingBox.Height - GAP, (uint)rnd.Next(), polygonVertices);
                    //DocumentManager.SavePolygon(polygonVertices, @"D:\poly\Polygon-" + polySize.ToString() + "-" + i.ToString("D2") + ".txt");
                    //polygonVertices.Clear();

                    polygonLoaded = DocumentManager.LoadPolygon(out polygonVertices, fileName);
                    if (!polygonLoaded)
                        throw new Exception();
                    polygonVerticesArr = polygonVertices.ToArray();

                    // Execute greedy algorithm (K=5)
                    timeStart = DateTime.Now;
                    erGreedy = Dll.DecomposePolygon_GreedyOrSeidel(0, polygonVerticesArr, 5);
                    diagonalsSum_Greedy_K5 += erGreedy.Diagonals.Length;
                    timeGreedy_K5 += (DateTime.Now - timeStart);

                    // Execute greedy algorithm (K=10)
                    timeStart = DateTime.Now;
                    erGreedy = Dll.DecomposePolygon_GreedyOrSeidel(0, polygonVerticesArr, 10);
                    diagonalsSum_Greedy_K10 += erGreedy.Diagonals.Length;
                    timeGreedy_K10 += (DateTime.Now - timeStart);

                    // Execute greedy algorithm (K=20)
                    timeStart = DateTime.Now;
                    erGreedy = Dll.DecomposePolygon_GreedyOrSeidel(0, polygonVerticesArr, 20);
                    diagonalsSum_Greedy_K20 += erGreedy.Diagonals.Length;
                    timeGreedy_K20 += (DateTime.Now - timeStart);

                    // Execute greedy algorithm (K=30)
                    timeStart = DateTime.Now;
                    erGreedy = Dll.DecomposePolygon_GreedyOrSeidel(0, polygonVerticesArr, polySize);
                    diagonalsSum_Greedy_K30 += erGreedy.Diagonals.Length;
                    timeGreedy_K30 += (DateTime.Now - timeStart);

                    // Execute Seidel's algorithm
                    timeStart = DateTime.Now;
                    erGreedy = Dll.DecomposePolygon_GreedyOrSeidel(1, polygonVerticesArr, -1);
                    diagonalsSum_Seidel += erGreedy.Diagonals.Length;
                    timeSeidel += (DateTime.Now - timeStart);

                    // Execute Lee & Preparata algorithm
                    timeStart = DateTime.Now;
                    erLeePreparata = Dll.DecomposePolygon_LeePreparata(polygonVerticesArr);
                    diagonalsSum_LeePreparata += (erLeePreparata.Components.Count - 1);
                    timeLeePreparata += (DateTime.Now - timeStart);
                }

                diagonalsAverage_Greedy_K5 = (float)diagonalsSum_Greedy_K5 / executionTimes;
                diagonalsAverage_Greedy_K10 = (float)diagonalsSum_Greedy_K10 / executionTimes;
                diagonalsAverage_Greedy_K20 = (float)diagonalsSum_Greedy_K20 / executionTimes;
                diagonalsAverage_Greedy_K30 = (float)diagonalsSum_Greedy_K30 / executionTimes;
                diagonalsAverage_Seidel = (float)diagonalsSum_Seidel / executionTimes;
                diagonalsAverage_LeePreparata = (float)diagonalsSum_LeePreparata / executionTimes;

                // 
                System.Console.WriteLine("Number of vertices:" + polySize.ToString());
                System.Console.WriteLine("Seidel algorithm average diagonals:          " + diagonalsAverage_Seidel.ToString("F2") + "   Time: " + timeSeidel.TotalSeconds.ToString("F2"));
                System.Console.WriteLine("Lee & Preparata algorithm average diagonals: " + diagonalsAverage_LeePreparata.ToString("F2") + "   Time: " + timeLeePreparata.TotalSeconds.ToString("F2"));
                System.Console.WriteLine("Greedy algorithm (K=5)  average diagonals:   " + diagonalsAverage_Greedy_K5.ToString("F2") + "   Time: " + timeGreedy_K5.TotalSeconds.ToString("F2"));
                System.Console.WriteLine("Greedy algorithm (K=10) average diagonals:   " + diagonalsAverage_Greedy_K10.ToString("F2") + "   Time: " + timeGreedy_K10.TotalSeconds.ToString("F2"));
                System.Console.WriteLine("Greedy algorithm (K=20) average diagonals:   " + diagonalsAverage_Greedy_K20.ToString("F2") + "   Time: " + timeGreedy_K20.TotalSeconds.ToString("F2"));
                System.Console.WriteLine("Greedy algorithm (K=30) average diagonals:   " + diagonalsAverage_Greedy_K30.ToString("F2") + "   Time: " + timeGreedy_K30.TotalSeconds.ToString("F2"));
                System.Console.WriteLine("------------------------------------------------");

            }
        }
    }
}
