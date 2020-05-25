using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gui
{
    interface IFormMain
    {
        Size BoundingBox { get; }
        void ResetForm();
        void ReadyToExecute();
        void AddPointToGridView(int id, float x, float y);
        void AddPointsToGridView(List<Vertex> vertices);
        void Update(Dll.ExecutionResult_GreedyOrSeidel er);
        void Update(Dll.ExecutionResult_LeePreparata er);
        Graphics GetGraphics();
    }

    public partial class FormMain : Form, IFormMain
    {
        FormMainPresenter _presenter;
        public Size BoundingBox
        {
            get
            {
                return pnlDraw.Size;
            }
        }

        public FormMain()
        {
            InitializeComponent();
            //
            _presenter = new FormMainPresenter(this);
        }

        //
        // IFormMain interface 
        //

        public Graphics GetGraphics()
        {
            Graphics g = pnlDraw.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            return g;
        }

        public void ResetForm()
        {
            dgvPoints.Rows.Clear();
            tvNodes.Nodes.Clear();
            mnu_alg_greedy.Enabled =
                mnu_alg_seidel.Enabled =
                mnu_alg_lee_preparata.Enabled = false;
            ddbAlgorithm.Enabled = false;
            lblMergesCount.Text = "-";
            lblSplitsCount.Text = "-";
            lblDiagonalsCount.Text = "-";
            ddbAlgorithm.Text = "---";
        }

        public void AddPointToGridView(int id, float x, float y)
        {
            object[] row = new object[] { id, x, y };
            dgvPoints.Rows.Add(row);
            dgvPoints.Rows[dgvPoints.Rows.Count - 1].Selected = true;
            dgvPoints.FirstDisplayedScrollingRowIndex = dgvPoints.Rows.Count - 1;
        }

        public void AddPointsToGridView(List<Vertex> vertices)
        {
            object[] row;

            int id = 0;
            foreach (Vertex v in vertices)
            {
                row = new object[] { id, v.X, v.Y };
                dgvPoints.Rows.Add(row);
                id++;
            }

            if (vertices.Count > 0)
                dgvPoints.FirstDisplayedScrollingRowIndex = dgvPoints.Rows.Count - 1;
        }

        public void ReadyToExecute()
        {
            mnu_alg_greedy.Enabled =
                mnu_alg_seidel.Enabled =
                mnu_alg_lee_preparata.Enabled =
                ddbAlgorithm.Enabled = true;
        }

        public void Update(Dll.ExecutionResult_GreedyOrSeidel er)
        {
            //
            // Update TreeView
            //
            tvNodes.BeginUpdate();
            TreeNode tn = new TreeNode();
            tn.Text = "Total: " + (er.XNodes.Length + er.YNodes.Length + er.Trapezoids.Length).ToString() + " Nodes";
            tvNodes.Nodes.Add(tn);

            // X Nodes
            foreach (XNode n in er.XNodes)
            {
                tn = new TreeNode("X Node (" + n.ID.ToString() + ")");
                tn.Nodes.Add(new TreeNode("Left : " + n.Left.ToString()));
                tn.Nodes.Add(new TreeNode("Right: " + n.Right.ToString()));
                tn.ForeColor = Color.Blue;
                tvNodes.Nodes.Add(tn);
            }

            // Y Nodes
            foreach (YNode n in er.YNodes)
            {
                tn = new TreeNode("Y Node (" + n.ID.ToString() + ")");
                tn.Nodes.Add(new TreeNode("Top   : " + n.Top.ToString()));
                tn.Nodes.Add(new TreeNode("Bottom: " + n.Bottom.ToString()));
                tn.ForeColor = Color.Red;
                tvNodes.Nodes.Add(tn);
            }

            // Trapezoid Nodes
            foreach (Trapezoid n in er.Trapezoids)
            {
                tn = new TreeNode("Trapezoid (" + n.ID.ToString() + ")");
                tn.Nodes.Add(new TreeNode("TopLeft : " + n.TopLeftTrap.ToString()));
                tn.Nodes.Add(new TreeNode("TopRight: " + n.TopRightTrap.ToString()));
                tn.Nodes.Add(new TreeNode("BottomLeft : " + n.BottomLeftTrap.ToString()));
                tn.Nodes.Add(new TreeNode("BottomRight: " + n.BottomRightTrap.ToString()));
                tn.Nodes.Add(new TreeNode("Left : " + n.Left.ToString()));
                tn.Nodes.Add(new TreeNode("Right: " + n.Right.ToString()));

                tvNodes.Nodes.Add(tn);
            }

            tvNodes.EndUpdate();
            //
            //
            //
            lblMergesCount.Text = er.NumMerges.ToString();
            lblSplitsCount.Text = er.NumSplits.ToString();
            lblDiagonalsCount.Text = er.Diagonals.Length.ToString();
        }

        public void Update(Dll.ExecutionResult_LeePreparata er)
        {
            lblMergesCount.Text = er.NumMerges.ToString();
            lblSplitsCount.Text = er.NumSplits.ToString();
            lblDiagonalsCount.Text = (er.Components.Count - 1).ToString();
        }
        //
        // Methods
        //

        private void btnReset_Click(object sender, EventArgs e)
        {
            _presenter.Reset();
        }

        private void mnu_Program_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnu_Polygon_GenerateRandomPolygon_Click(object sender, EventArgs e)
        {
            _presenter.GenerateRandomPolygon();
        }

        private void mnu_polygon_num_vertices_Click(object sender, EventArgs e)
        {
            mnu_polygon_num_10.Checked =
            mnu_polygon_num_20.Checked =
            mnu_polygon_num_30.Checked =
            mnu_polygon_num_40.Checked =
            mnu_polygon_num_50.Checked =
            mnu_polygon_num_100.Checked =
            mnu_polygon_num_150.Checked =
            mnu_polygon_num_200.Checked =
            mnu_polygon_num_250.Checked = false;

            ToolStripMenuItem s = (ToolStripMenuItem)sender;
            _presenter.RandomPolygonSize = Int32.Parse(s.Tag.ToString());
            s.Checked = true;
        }

        private void mnu_alg_decompose(object sender, EventArgs e)
        {
            int tag = Int32.Parse((sender as ToolStripMenuItem).Tag.ToString());
            if (tag == 0)
            {
                // Execute greedy algorithm
                _presenter.Algorithm = DecompositionAlgorithm.Greedy;
                ddbAlgorithm.Text = statusbar_alg_greedy.Text;
            }
            else if (tag == 1)
            {
                // Execute Seidel's algorithm
                _presenter.Algorithm = DecompositionAlgorithm.Seidel;
                ddbAlgorithm.Text = statusbar_alg_seidel.Text;
            }
            else if (tag == 2)
            {
                // Execute Lee and Preparata's algorithm
                _presenter.Algorithm = DecompositionAlgorithm.Lee_Preparata;
                ddbAlgorithm.Text = statusbar_alg_lee_preparata.Text;
            }
            else if (tag == 3)
            {
                // Trapezoidal Decomposition
                _presenter.Algorithm = DecompositionAlgorithm.Trapezoidal;
                ddbAlgorithm.Text = statusbar_alg_trapezoidal_decompose.Text;
            }

            //
            tvNodes.Nodes.Clear();
            _presenter.ExecuteDecomposition();

        }

        private void pnlDraw_MouseClick(object sender, MouseEventArgs e)
        {
            _presenter.HandleMouseClick(e);
        }

        private void pnlDraw_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            _presenter.HandleFormPaint(e.Graphics);
        }

        private void mnu_alg_reset_Click(object sender, EventArgs e)
        {
            _presenter.Reset();
        }

        private void FormMain_ResizeEnd(object sender, EventArgs e)
        {
            pnlDraw.Invalidate();
        }

        private void mnu_show_trapezoids_Click(object sender, EventArgs e)
        {
            bool show;

            show = mnu_show_trapezoids.Checked;
            _presenter.ShowTrapezoids(show);
        }

        private void mnu_show_trapezoid_id_Click(object sender, EventArgs e)
        {
            _presenter.ShowTrapezoidID(mnu_show_trapezoid_id.Checked);
        }

        private void mnu_show_diagonals_Click(object sender, EventArgs e)
        {
            _presenter.ShowDiagonals(mnu_show_diagonals.Checked);
        }

        private void تستToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _presenter.RandomPolygonSize = 15;
            _presenter.GenerateRandomPolygon();
            _presenter.ExecuteDecomposition();
        }

        private void mnu_program_load_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _presenter.OpenFile(openFileDialog.FileName);
            }
        }

        private void pnlDraw_MouseMove(object sender, MouseEventArgs e)
        {
            lblMousePos.Text = "(X=" + e.X.ToString("D2") + ", Y=" + (BoundingBox.Height - e.Y).ToString("D2") + ")";
        }

        private void mnu_alg_parameters_Click(object sender, EventArgs e)
        {
            FormAlgorithmParameters frm = new FormAlgorithmParameters();
            frm.MaximumDepthTraverse = _presenter.GreedyAlgorithm_MaxDepthTraverse;

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _presenter.GreedyAlgorithm_MaxDepthTraverse = frm.MaximumDepthTraverse;
            }
        }

    }
}
