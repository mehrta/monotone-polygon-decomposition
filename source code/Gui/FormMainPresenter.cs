using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Gui
{
    public enum DecompositionAlgorithm { Greedy = 0, Seidel = 1, Lee_Preparata = 2, Trapezoidal = 3 };

    class FormMainPresenter
    {
        IFormMain _form;
        GraphicManager _gManager;
        public List<Vertex> _vertices;
        bool _isPolygonClosed;
        bool _isPolygonGeneratedByCgal;
        bool _algorithmExecuted;

        // Properties
        public int RandomPolygonSize { set; get; }
        public DecompositionAlgorithm Algorithm { set; get; }
        public int GreedyAlgorithm_MaxDepthTraverse { set; get; }

        //
        public FormMainPresenter(IFormMain form)
        {
            _form = form;
            _vertices = new List<Vertex>(100);
            _gManager = new GraphicManager();
            _gManager.PolygonVertices = _vertices;
            RandomPolygonSize = 30;
            Algorithm = DecompositionAlgorithm.Greedy;
            GreedyAlgorithm_MaxDepthTraverse = 100;
        }

        public void HandleMouseClick(MouseEventArgs e)
        {
            if (_isPolygonClosed)
                return;

            Graphics g = _form.GetGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // Click
                Vertex currentPoint = new Vertex();
                currentPoint.X = e.X;
                currentPoint.Y = _form.BoundingBox.Height - e.Y;

                // Add point
                _form.AddPointToGridView(_vertices.Count, currentPoint.X, currentPoint.Y);
                _vertices.Add(currentPoint);

                // Draw line segment
                g.DrawEllipse(Pens.Black, e.X - 1, e.Y - 1, 2, 2);

                if (_vertices.Count > 1)
                {
                    Vertex p2;
                    p2 = _vertices[_vertices.Count - 2];
                    g.DrawLine(Pens.Blue, e.X, e.Y, p2.X, _form.BoundingBox.Height - p2.Y);
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                // Right click
                if (_vertices.Count < 3)
                    return;

                // Close polygon
                _isPolygonClosed = true;
                _gManager.IsPolygonClosed = true;
                Vertex firstPoint, lastPoint;

                firstPoint = _vertices.First();
                lastPoint = _vertices.Last();

                g.DrawLine(Pens.Blue, firstPoint.X, _form.BoundingBox.Height - firstPoint.Y, lastPoint.X,
                    _form.BoundingBox.Height - lastPoint.Y);

                _form.ReadyToExecute();
            }
        }

        public void HandleFormPaint(Graphics g)
        {
            _gManager.BoundingBox = _form.BoundingBox;
            _gManager.G = g;

            if (_algorithmExecuted)
            {
                _gManager.RedrawExecutionResult(Algorithm);
            }
            else
            {
                if (_vertices.Count > 0)
                    _gManager.DrawPolygon(_vertices, _isPolygonClosed);
            }
        }

        public void Reset()
        {
            _vertices.Clear();
            _isPolygonClosed = false;
            _isPolygonGeneratedByCgal = false;
            _algorithmExecuted = false;
            _gManager.IsPolygonClosed = false;
            _gManager.GreedyOrSeidelExecutionResult.Reset();
            _form.ResetForm();
            _form.GetGraphics().Clear(Color.White);
        }

        public void ExecuteDecomposition()
        {
            // Check for y-coordinates of vertices
            if ((_isPolygonGeneratedByCgal == false) && (Algorithm != DecompositionAlgorithm.Lee_Preparata))
            {
                System.Collections.Generic.HashSet<float> set = new HashSet<float>();
                foreach (Vertex v in _vertices)
                {
                    if (!set.Add(v.Y))
                    {
                        MessageBox.Show(".یکسان دارند y دو نقطه مختلف مولفه");
                        return;
                    }
                }
            }

            // Execute Algorithm
            _gManager.G = _form.GetGraphics();
            _algorithmExecuted = true;
            _gManager.PolygonVertices = _vertices;
            if (Algorithm == DecompositionAlgorithm.Greedy || Algorithm == DecompositionAlgorithm.Seidel || Algorithm == DecompositionAlgorithm.Trapezoidal)
            {
                int k;
                Dll.ExecutionResult_GreedyOrSeidel er;

                k = (Algorithm == DecompositionAlgorithm.Trapezoidal) ? 0 : GreedyAlgorithm_MaxDepthTraverse;

                Dll.SetBoundingBox(_form.BoundingBox.Height, 0, 0, _form.BoundingBox.Width);
                er = Dll.DecomposePolygon_GreedyOrSeidel((int)Algorithm, _vertices.ToArray(), k);
                _gManager.GreedyOrSeidelExecutionResult = er;
                _gManager.RedrawExecutionResult(DecompositionAlgorithm.Greedy);
                _form.Update(er);
            }
            else
            {
                Dll.ExecutionResult_LeePreparata er;
                er = Dll.DecomposePolygon_LeePreparata(_vertices.ToArray());
                _gManager.LeePreparataExecutionResult = er;
                _gManager.RedrawExecutionResult(DecompositionAlgorithm.Lee_Preparata);
                _form.Update(er);
            }
        }

        public void GenerateRandomPolygon()
        {
            const int GAP = 2;
            Random rnd = new Random();

            Reset();

            Dll.RandomPolygon(
                this.RandomPolygonSize,
                GAP,
                _form.BoundingBox.Width - GAP,
                GAP,
                _form.BoundingBox.Height - GAP, (uint)rnd.Next(), _vertices);

            _isPolygonClosed = true;
            _gManager.IsPolygonClosed = true;
            _isPolygonGeneratedByCgal = true;

            // Update GUI
            LoadPolygonInGui(_vertices);
        }

        public void ShowTrapezoids(bool show)
        {
            _gManager.G = _form.GetGraphics();
            _gManager.ShowTrapezoids = show;
            _gManager.RedrawExecutionResult(Algorithm);
        }

        public void ShowTrapezoidID(bool show)
        {
            _gManager.G = _form.GetGraphics();
            _gManager.ShowTrapezoidId = show;
            _gManager.RedrawExecutionResult(Algorithm);
        }

        public void ShowDiagonals(bool show)
        {
            _gManager.G = _form.GetGraphics();
            _gManager.ShowDiagonals = show;
            _gManager.RedrawExecutionResult(Algorithm);
        }

        public void OpenFile(string fileName)
        {
            DocumentManager.LoadPolygon(out _vertices, fileName);
            _isPolygonClosed = true;
            LoadPolygonInGui(_vertices);
        }

        private void LoadPolygonInGui(List<Vertex> vertices)
        {
            _gManager.G = _form.GetGraphics();
            _form.AddPointsToGridView(vertices);
            _gManager.DrawPolygon(vertices, true);
            _form.ReadyToExecute();
        }

    }
}
