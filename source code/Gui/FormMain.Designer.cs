namespace Gui
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.برنامهToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_program_load = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_program_save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.راهنماییToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_program_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.الگوریتمToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_alg_greedy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_alg_seidel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_alg_lee_preparata = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_alg_reset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_alg_parameters = new System.Windows.Forms.ToolStripMenuItem();
            this.تستToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_polygon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_polygon_num = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_polygon_num_10 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_polygon_num_20 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_polygon_num_30 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_polygon_num_40 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_polygon_num_50 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_polygon_num_100 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_polygon_num_150 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_polygon_num_200 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_polygon_num_250 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Polygon_GenerateRandomPolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.نمایشToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_show_trapezoids = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_show_trapezoid_id = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_show_diagonals = new System.Windows.Forms.ToolStripMenuItem();
            this.دربارهToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.برنامهToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ddbAlgorithm = new System.Windows.Forms.ToolStripDropDownButton();
            this.statusbar_alg_greedy = new System.Windows.Forms.ToolStripMenuItem();
            this.statusbar_alg_seidel = new System.Windows.Forms.ToolStripMenuItem();
            this.statusbar_alg_lee_preparata = new System.Windows.Forms.ToolStripMenuItem();
            this.statusbar_alg_trapezoidal_decompose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMergesCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSplitsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDiagonalsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.lblMousePos = new System.Windows.Forms.ToolStripStatusLabel();
            this.tvNodes = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageVertices = new System.Windows.Forms.TabPage();
            this.dgvPoints = new System.Windows.Forms.DataGridView();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageNodes = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlDraw = new System.Windows.Forms.Panel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mnuMain.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageVertices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).BeginInit();
            this.tabPageNodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.BackColor = System.Drawing.Color.White;
            this.mnuMain.Font = new System.Drawing.Font("Tahoma", 9F);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.برنامهToolStripMenuItem,
            this.الگوریتمToolStripMenuItem,
            this.mnu_polygon,
            this.نمایشToolStripMenuItem,
            this.دربارهToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mnuMain.Size = new System.Drawing.Size(936, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // برنامهToolStripMenuItem
            // 
            this.برنامهToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_program_load,
            this.mnu_program_save,
            this.toolStripSeparator5,
            this.راهنماییToolStripMenuItem,
            this.toolStripSeparator6,
            this.mnu_program_exit});
            this.برنامهToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.برنامهToolStripMenuItem.Name = "برنامهToolStripMenuItem";
            this.برنامهToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.برنامهToolStripMenuItem.Text = "برنامه";
            // 
            // mnu_program_load
            // 
            this.mnu_program_load.Name = "mnu_program_load";
            this.mnu_program_load.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnu_program_load.Size = new System.Drawing.Size(211, 22);
            this.mnu_program_load.Text = "بارگذاری چندضلعی";
            this.mnu_program_load.Click += new System.EventHandler(this.mnu_program_load_Click);
            // 
            // mnu_program_save
            // 
            this.mnu_program_save.Name = "mnu_program_save";
            this.mnu_program_save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnu_program_save.Size = new System.Drawing.Size(211, 22);
            this.mnu_program_save.Text = "ذخیره چندضلعی";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(208, 6);
            // 
            // راهنماییToolStripMenuItem
            // 
            this.راهنماییToolStripMenuItem.Name = "راهنماییToolStripMenuItem";
            this.راهنماییToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.راهنماییToolStripMenuItem.Text = "راهنمایی";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(208, 6);
            // 
            // mnu_program_exit
            // 
            this.mnu_program_exit.ForeColor = System.Drawing.Color.Red;
            this.mnu_program_exit.Name = "mnu_program_exit";
            this.mnu_program_exit.Size = new System.Drawing.Size(211, 22);
            this.mnu_program_exit.Text = "خروج";
            this.mnu_program_exit.Click += new System.EventHandler(this.mnu_Program_Exit_Click);
            // 
            // الگوریتمToolStripMenuItem
            // 
            this.الگوریتمToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_alg_greedy,
            this.mnu_alg_seidel,
            this.mnu_alg_lee_preparata,
            this.toolStripSeparator1,
            this.mnu_alg_reset,
            this.toolStripSeparator3,
            this.mnu_alg_parameters,
            this.تستToolStripMenuItem});
            this.الگوریتمToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.الگوریتمToolStripMenuItem.Name = "الگوریتمToolStripMenuItem";
            this.الگوریتمToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.الگوریتمToolStripMenuItem.Text = "الگوریتم";
            // 
            // mnu_alg_greedy
            // 
            this.mnu_alg_greedy.Enabled = false;
            this.mnu_alg_greedy.Name = "mnu_alg_greedy";
            this.mnu_alg_greedy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.mnu_alg_greedy.Size = new System.Drawing.Size(277, 22);
            this.mnu_alg_greedy.Tag = "0";
            this.mnu_alg_greedy.Text = "اجرای الگوریتم حریص (پایان نامه)";
            this.mnu_alg_greedy.Click += new System.EventHandler(this.mnu_alg_decompose);
            // 
            // mnu_alg_seidel
            // 
            this.mnu_alg_seidel.Enabled = false;
            this.mnu_alg_seidel.Name = "mnu_alg_seidel";
            this.mnu_alg_seidel.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.mnu_alg_seidel.Size = new System.Drawing.Size(277, 22);
            this.mnu_alg_seidel.Tag = "1";
            this.mnu_alg_seidel.Text = "اجرای الگوریتم سایدل";
            this.mnu_alg_seidel.Click += new System.EventHandler(this.mnu_alg_decompose);
            // 
            // mnu_alg_lee_preparata
            // 
            this.mnu_alg_lee_preparata.Enabled = false;
            this.mnu_alg_lee_preparata.Name = "mnu_alg_lee_preparata";
            this.mnu_alg_lee_preparata.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.mnu_alg_lee_preparata.Size = new System.Drawing.Size(277, 22);
            this.mnu_alg_lee_preparata.Tag = "2";
            this.mnu_alg_lee_preparata.Text = "اجرای الگوریتم لی و پریپاراتا";
            this.mnu_alg_lee_preparata.Click += new System.EventHandler(this.mnu_alg_decompose);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(274, 6);
            // 
            // mnu_alg_reset
            // 
            this.mnu_alg_reset.Name = "mnu_alg_reset";
            this.mnu_alg_reset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnu_alg_reset.Size = new System.Drawing.Size(277, 22);
            this.mnu_alg_reset.Text = "شروع مجدد";
            this.mnu_alg_reset.Click += new System.EventHandler(this.mnu_alg_reset_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(274, 6);
            // 
            // mnu_alg_parameters
            // 
            this.mnu_alg_parameters.Name = "mnu_alg_parameters";
            this.mnu_alg_parameters.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.mnu_alg_parameters.Size = new System.Drawing.Size(277, 22);
            this.mnu_alg_parameters.Text = "پارامترهای الگوریتم ...";
            this.mnu_alg_parameters.Click += new System.EventHandler(this.mnu_alg_parameters_Click);
            // 
            // تستToolStripMenuItem
            // 
            this.تستToolStripMenuItem.Name = "تستToolStripMenuItem";
            this.تستToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.تستToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.تستToolStripMenuItem.Text = "تست";
            this.تستToolStripMenuItem.Click += new System.EventHandler(this.تستToolStripMenuItem_Click);
            // 
            // mnu_polygon
            // 
            this.mnu_polygon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_polygon_num,
            this.mnu_Polygon_GenerateRandomPolygon});
            this.mnu_polygon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.mnu_polygon.Name = "mnu_polygon";
            this.mnu_polygon.Size = new System.Drawing.Size(69, 20);
            this.mnu_polygon.Text = "چندضلعی";
            // 
            // mnu_polygon_num
            // 
            this.mnu_polygon_num.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_polygon_num_10,
            this.mnu_polygon_num_20,
            this.mnu_polygon_num_30,
            this.mnu_polygon_num_40,
            this.mnu_polygon_num_50,
            this.mnu_polygon_num_100,
            this.mnu_polygon_num_150,
            this.mnu_polygon_num_200,
            this.mnu_polygon_num_250});
            this.mnu_polygon_num.Name = "mnu_polygon_num";
            this.mnu_polygon_num.Size = new System.Drawing.Size(248, 22);
            this.mnu_polygon_num.Text = "تعداد راس های چندضلعی تصادفی";
            // 
            // mnu_polygon_num_10
            // 
            this.mnu_polygon_num_10.Name = "mnu_polygon_num_10";
            this.mnu_polygon_num_10.Size = new System.Drawing.Size(121, 22);
            this.mnu_polygon_num_10.Tag = "10";
            this.mnu_polygon_num_10.Text = "10 راس";
            this.mnu_polygon_num_10.Click += new System.EventHandler(this.mnu_polygon_num_vertices_Click);
            // 
            // mnu_polygon_num_20
            // 
            this.mnu_polygon_num_20.Name = "mnu_polygon_num_20";
            this.mnu_polygon_num_20.Size = new System.Drawing.Size(121, 22);
            this.mnu_polygon_num_20.Tag = "20";
            this.mnu_polygon_num_20.Text = "20 راس";
            this.mnu_polygon_num_20.Click += new System.EventHandler(this.mnu_polygon_num_vertices_Click);
            // 
            // mnu_polygon_num_30
            // 
            this.mnu_polygon_num_30.Checked = true;
            this.mnu_polygon_num_30.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnu_polygon_num_30.Name = "mnu_polygon_num_30";
            this.mnu_polygon_num_30.Size = new System.Drawing.Size(121, 22);
            this.mnu_polygon_num_30.Tag = "30";
            this.mnu_polygon_num_30.Text = "30 راس";
            this.mnu_polygon_num_30.Click += new System.EventHandler(this.mnu_polygon_num_vertices_Click);
            // 
            // mnu_polygon_num_40
            // 
            this.mnu_polygon_num_40.Name = "mnu_polygon_num_40";
            this.mnu_polygon_num_40.Size = new System.Drawing.Size(121, 22);
            this.mnu_polygon_num_40.Tag = "40";
            this.mnu_polygon_num_40.Text = "40 راس";
            this.mnu_polygon_num_40.Click += new System.EventHandler(this.mnu_polygon_num_vertices_Click);
            // 
            // mnu_polygon_num_50
            // 
            this.mnu_polygon_num_50.Name = "mnu_polygon_num_50";
            this.mnu_polygon_num_50.Size = new System.Drawing.Size(121, 22);
            this.mnu_polygon_num_50.Tag = "50";
            this.mnu_polygon_num_50.Text = "50 راس";
            this.mnu_polygon_num_50.Click += new System.EventHandler(this.mnu_polygon_num_vertices_Click);
            // 
            // mnu_polygon_num_100
            // 
            this.mnu_polygon_num_100.Name = "mnu_polygon_num_100";
            this.mnu_polygon_num_100.Size = new System.Drawing.Size(121, 22);
            this.mnu_polygon_num_100.Tag = "100";
            this.mnu_polygon_num_100.Text = "100 راس";
            this.mnu_polygon_num_100.Click += new System.EventHandler(this.mnu_polygon_num_vertices_Click);
            // 
            // mnu_polygon_num_150
            // 
            this.mnu_polygon_num_150.Name = "mnu_polygon_num_150";
            this.mnu_polygon_num_150.Size = new System.Drawing.Size(121, 22);
            this.mnu_polygon_num_150.Tag = "150";
            this.mnu_polygon_num_150.Text = "150 راس";
            this.mnu_polygon_num_150.Click += new System.EventHandler(this.mnu_polygon_num_vertices_Click);
            // 
            // mnu_polygon_num_200
            // 
            this.mnu_polygon_num_200.Name = "mnu_polygon_num_200";
            this.mnu_polygon_num_200.Size = new System.Drawing.Size(121, 22);
            this.mnu_polygon_num_200.Tag = "200";
            this.mnu_polygon_num_200.Text = "200 راس";
            this.mnu_polygon_num_200.Click += new System.EventHandler(this.mnu_polygon_num_vertices_Click);
            // 
            // mnu_polygon_num_250
            // 
            this.mnu_polygon_num_250.Name = "mnu_polygon_num_250";
            this.mnu_polygon_num_250.Size = new System.Drawing.Size(121, 22);
            this.mnu_polygon_num_250.Tag = "250";
            this.mnu_polygon_num_250.Text = "250 راس";
            this.mnu_polygon_num_250.Click += new System.EventHandler(this.mnu_polygon_num_vertices_Click);
            // 
            // mnu_Polygon_GenerateRandomPolygon
            // 
            this.mnu_Polygon_GenerateRandomPolygon.Name = "mnu_Polygon_GenerateRandomPolygon";
            this.mnu_Polygon_GenerateRandomPolygon.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.mnu_Polygon_GenerateRandomPolygon.Size = new System.Drawing.Size(248, 22);
            this.mnu_Polygon_GenerateRandomPolygon.Text = "تولید چندضلعی تصادفی";
            this.mnu_Polygon_GenerateRandomPolygon.Click += new System.EventHandler(this.mnu_Polygon_GenerateRandomPolygon_Click);
            // 
            // نمایشToolStripMenuItem
            // 
            this.نمایشToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_show_trapezoids,
            this.mnu_show_trapezoid_id,
            this.mnu_show_diagonals});
            this.نمایشToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.نمایشToolStripMenuItem.Name = "نمایشToolStripMenuItem";
            this.نمایشToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.نمایشToolStripMenuItem.Text = "نمایش";
            // 
            // mnu_show_trapezoids
            // 
            this.mnu_show_trapezoids.Checked = true;
            this.mnu_show_trapezoids.CheckOnClick = true;
            this.mnu_show_trapezoids.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnu_show_trapezoids.Name = "mnu_show_trapezoids";
            this.mnu_show_trapezoids.Size = new System.Drawing.Size(227, 22);
            this.mnu_show_trapezoids.Text = "تجزیه ذوزنقه ای (Trapezoidal)";
            this.mnu_show_trapezoids.Click += new System.EventHandler(this.mnu_show_trapezoids_Click);
            // 
            // mnu_show_trapezoid_id
            // 
            this.mnu_show_trapezoid_id.Checked = true;
            this.mnu_show_trapezoid_id.CheckOnClick = true;
            this.mnu_show_trapezoid_id.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnu_show_trapezoid_id.Name = "mnu_show_trapezoid_id";
            this.mnu_show_trapezoid_id.Size = new System.Drawing.Size(227, 22);
            this.mnu_show_trapezoid_id.Text = "شماره ذوزنقه ها";
            this.mnu_show_trapezoid_id.Click += new System.EventHandler(this.mnu_show_trapezoid_id_Click);
            // 
            // mnu_show_diagonals
            // 
            this.mnu_show_diagonals.Checked = true;
            this.mnu_show_diagonals.CheckOnClick = true;
            this.mnu_show_diagonals.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnu_show_diagonals.Name = "mnu_show_diagonals";
            this.mnu_show_diagonals.Size = new System.Drawing.Size(227, 22);
            this.mnu_show_diagonals.Text = "قطرها";
            this.mnu_show_diagonals.Click += new System.EventHandler(this.mnu_show_diagonals_Click);
            // 
            // دربارهToolStripMenuItem
            // 
            this.دربارهToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.برنامهToolStripMenuItem1});
            this.دربارهToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.دربارهToolStripMenuItem.Name = "دربارهToolStripMenuItem";
            this.دربارهToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.دربارهToolStripMenuItem.Text = "درباره";
            // 
            // برنامهToolStripMenuItem1
            // 
            this.برنامهToolStripMenuItem1.Name = "برنامهToolStripMenuItem1";
            this.برنامهToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.برنامهToolStripMenuItem1.Text = "برنامه";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(47, 17);
            this.toolStripStatusLabel1.Text = "الگوریتم:";
            // 
            // ddbAlgorithm
            // 
            this.ddbAlgorithm.AutoSize = false;
            this.ddbAlgorithm.BackColor = System.Drawing.Color.Olive;
            this.ddbAlgorithm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ddbAlgorithm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusbar_alg_greedy,
            this.statusbar_alg_seidel,
            this.statusbar_alg_lee_preparata,
            this.statusbar_alg_trapezoidal_decompose});
            this.ddbAlgorithm.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ddbAlgorithm.ForeColor = System.Drawing.Color.Black;
            this.ddbAlgorithm.Image = ((System.Drawing.Image)(resources.GetObject("ddbAlgorithm.Image")));
            this.ddbAlgorithm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbAlgorithm.Name = "ddbAlgorithm";
            this.ddbAlgorithm.Size = new System.Drawing.Size(150, 20);
            this.ddbAlgorithm.Text = "---";
            // 
            // statusbar_alg_greedy
            // 
            this.statusbar_alg_greedy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.statusbar_alg_greedy.ForeColor = System.Drawing.Color.Black;
            this.statusbar_alg_greedy.Name = "statusbar_alg_greedy";
            this.statusbar_alg_greedy.Size = new System.Drawing.Size(153, 22);
            this.statusbar_alg_greedy.Tag = "0";
            this.statusbar_alg_greedy.Text = "حریص (پایان نامه)";
            this.statusbar_alg_greedy.Click += new System.EventHandler(this.mnu_alg_decompose);
            // 
            // statusbar_alg_seidel
            // 
            this.statusbar_alg_seidel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.statusbar_alg_seidel.ForeColor = System.Drawing.Color.Black;
            this.statusbar_alg_seidel.Name = "statusbar_alg_seidel";
            this.statusbar_alg_seidel.Size = new System.Drawing.Size(153, 22);
            this.statusbar_alg_seidel.Tag = "1";
            this.statusbar_alg_seidel.Text = "سایدل";
            this.statusbar_alg_seidel.Click += new System.EventHandler(this.mnu_alg_decompose);
            // 
            // statusbar_alg_lee_preparata
            // 
            this.statusbar_alg_lee_preparata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.statusbar_alg_lee_preparata.ForeColor = System.Drawing.Color.Black;
            this.statusbar_alg_lee_preparata.Name = "statusbar_alg_lee_preparata";
            this.statusbar_alg_lee_preparata.Size = new System.Drawing.Size(153, 22);
            this.statusbar_alg_lee_preparata.Tag = "2";
            this.statusbar_alg_lee_preparata.Text = "لی و پریپاراتا";
            this.statusbar_alg_lee_preparata.Click += new System.EventHandler(this.mnu_alg_decompose);
            // 
            // statusbar_alg_trapezoidal_decompose
            // 
            this.statusbar_alg_trapezoidal_decompose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.statusbar_alg_trapezoidal_decompose.Name = "statusbar_alg_trapezoidal_decompose";
            this.statusbar_alg_trapezoidal_decompose.Size = new System.Drawing.Size(153, 22);
            this.statusbar_alg_trapezoidal_decompose.Tag = "3";
            this.statusbar_alg_trapezoidal_decompose.Text = "تجزیه ذوزنقه ای";
            this.statusbar_alg_trapezoidal_decompose.Click += new System.EventHandler(this.mnu_alg_decompose);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel2.Text = "راس ادغام:";
            // 
            // lblMergesCount
            // 
            this.lblMergesCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblMergesCount.Name = "lblMergesCount";
            this.lblMergesCount.Size = new System.Drawing.Size(11, 17);
            this.lblMergesCount.Text = "-";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel4.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(64, 17);
            this.toolStripStatusLabel4.Text = "راس تفکیک:";
            // 
            // lblSplitsCount
            // 
            this.lblSplitsCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblSplitsCount.Name = "lblSplitsCount";
            this.lblSplitsCount.Size = new System.Drawing.Size(11, 17);
            this.lblSplitsCount.Text = "-";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel6.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(66, 17);
            this.toolStripStatusLabel6.Text = "تعداد قطرها:";
            // 
            // lblDiagonalsCount
            // 
            this.lblDiagonalsCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblDiagonalsCount.Name = "lblDiagonalsCount";
            this.lblDiagonalsCount.Size = new System.Drawing.Size(11, 17);
            this.lblDiagonalsCount.Text = "-";
            // 
            // ssMain
            // 
            this.ssMain.BackColor = System.Drawing.Color.White;
            this.ssMain.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.ddbAlgorithm,
            this.toolStripStatusLabel2,
            this.lblMergesCount,
            this.toolStripStatusLabel4,
            this.lblSplitsCount,
            this.toolStripStatusLabel6,
            this.lblDiagonalsCount,
            this.lblMousePos});
            this.ssMain.Location = new System.Drawing.Point(0, 430);
            this.ssMain.Name = "ssMain";
            this.ssMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ssMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ssMain.Size = new System.Drawing.Size(936, 22);
            this.ssMain.SizingGrip = false;
            this.ssMain.TabIndex = 1;
            this.ssMain.Text = "statusStrip1";
            // 
            // lblMousePos
            // 
            this.lblMousePos.Name = "lblMousePos";
            this.lblMousePos.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblMousePos.Size = new System.Drawing.Size(457, 17);
            this.lblMousePos.Spring = true;
            this.lblMousePos.Text = "-";
            this.lblMousePos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tvNodes
            // 
            this.tvNodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvNodes.Location = new System.Drawing.Point(0, 0);
            this.tvNodes.Name = "tvNodes";
            this.tvNodes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tvNodes.Size = new System.Drawing.Size(173, 380);
            this.tvNodes.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPageVertices);
            this.tabControl1.Controls.Add(this.tabPageNodes);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(181, 406);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.TabStop = false;
            // 
            // tabPageVertices
            // 
            this.tabPageVertices.BackColor = System.Drawing.Color.White;
            this.tabPageVertices.Controls.Add(this.dgvPoints);
            this.tabPageVertices.Location = new System.Drawing.Point(4, 4);
            this.tabPageVertices.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageVertices.Name = "tabPageVertices";
            this.tabPageVertices.Size = new System.Drawing.Size(173, 380);
            this.tabPageVertices.TabIndex = 0;
            this.tabPageVertices.Text = "راس های چندضلعی";
            // 
            // dgvPoints
            // 
            this.dgvPoints.AllowUserToAddRows = false;
            this.dgvPoints.AllowUserToResizeRows = false;
            this.dgvPoints.BackgroundColor = System.Drawing.Color.White;
            this.dgvPoints.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPoints.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPoints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPoints.ColumnHeadersHeight = 22;
            this.dgvPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_id,
            this.col_x,
            this.col_y});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPoints.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPoints.EnableHeadersVisualStyles = false;
            this.dgvPoints.GridColor = System.Drawing.Color.Green;
            this.dgvPoints.Location = new System.Drawing.Point(0, 0);
            this.dgvPoints.Margin = new System.Windows.Forms.Padding(0);
            this.dgvPoints.MultiSelect = false;
            this.dgvPoints.Name = "dgvPoints";
            this.dgvPoints.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvPoints.RowHeadersVisible = false;
            this.dgvPoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPoints.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvPoints.RowTemplate.Height = 18;
            this.dgvPoints.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPoints.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvPoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPoints.ShowCellErrors = false;
            this.dgvPoints.ShowCellToolTips = false;
            this.dgvPoints.ShowRowErrors = false;
            this.dgvPoints.Size = new System.Drawing.Size(173, 380);
            this.dgvPoints.TabIndex = 1;
            // 
            // col_id
            // 
            this.col_id.HeaderText = "ID";
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_id.Width = 40;
            // 
            // col_x
            // 
            this.col_x.HeaderText = "X";
            this.col_x.Name = "col_x";
            this.col_x.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_x.Width = 66;
            // 
            // col_y
            // 
            this.col_y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_y.HeaderText = "Y";
            this.col_y.Name = "col_y";
            this.col_y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabPageNodes
            // 
            this.tabPageNodes.Controls.Add(this.tvNodes);
            this.tabPageNodes.Location = new System.Drawing.Point(4, 4);
            this.tabPageNodes.Name = "tabPageNodes";
            this.tabPageNodes.Size = new System.Drawing.Size(173, 380);
            this.tabPageNodes.TabIndex = 0;
            this.tabPageNodes.Text = "گره ها";
            this.tabPageNodes.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlDraw);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(936, 406);
            this.splitContainer1.SplitterDistance = 181;
            this.splitContainer1.TabIndex = 10;
            // 
            // pnlDraw
            // 
            this.pnlDraw.BackColor = System.Drawing.Color.White;
            this.pnlDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDraw.Location = new System.Drawing.Point(0, 0);
            this.pnlDraw.Name = "pnlDraw";
            this.pnlDraw.Size = new System.Drawing.Size(751, 406);
            this.pnlDraw.TabIndex = 0;
            this.pnlDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDraw_Paint);
            this.pnlDraw.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlDraw_MouseClick);
            this.pnlDraw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlDraw_MouseMove);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "بارگذاری چندضلعی";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(936, 452);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.mnuMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ForeColor = System.Drawing.Color.Black;
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.Text = "Monotone Polygon Decomposition";
            this.ResizeEnd += new System.EventHandler(this.FormMain_ResizeEnd);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageVertices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).EndInit();
            this.tabPageNodes.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem برنامهToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem الگوریتمToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem نمایشToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem دربارهToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_program_exit;
        private System.Windows.Forms.ToolStripMenuItem mnu_alg_greedy;
        private System.Windows.Forms.ToolStripMenuItem mnu_alg_lee_preparata;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnu_alg_parameters;
        private System.Windows.Forms.ToolStripMenuItem mnu_show_trapezoid_id;
        private System.Windows.Forms.ToolStripMenuItem mnu_show_trapezoids;
        private System.Windows.Forms.ToolStripMenuItem mnu_show_diagonals;
        private System.Windows.Forms.ToolStripMenuItem برنامهToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem راهنماییToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnu_program_save;
        private System.Windows.Forms.ToolStripMenuItem mnu_program_load;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton ddbAlgorithm;
        private System.Windows.Forms.ToolStripMenuItem statusbar_alg_greedy;
        private System.Windows.Forms.ToolStripMenuItem statusbar_alg_lee_preparata;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblMergesCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lblSplitsCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel lblDiagonalsCount;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.TreeView tvNodes;
        private System.Windows.Forms.ToolStripMenuItem statusbar_alg_seidel;
        private System.Windows.Forms.ToolStripMenuItem mnu_polygon;
        private System.Windows.Forms.ToolStripMenuItem mnu_Polygon_GenerateRandomPolygon;
        private System.Windows.Forms.ToolStripMenuItem mnu_polygon_num;
        private System.Windows.Forms.ToolStripMenuItem mnu_polygon_num_10;
        private System.Windows.Forms.ToolStripMenuItem mnu_polygon_num_20;
        private System.Windows.Forms.ToolStripMenuItem mnu_polygon_num_30;
        private System.Windows.Forms.ToolStripMenuItem mnu_polygon_num_40;
        private System.Windows.Forms.ToolStripMenuItem mnu_polygon_num_50;
        private System.Windows.Forms.ToolStripMenuItem mnu_polygon_num_100;
        private System.Windows.Forms.ToolStripMenuItem mnu_polygon_num_150;
        private System.Windows.Forms.ToolStripMenuItem mnu_polygon_num_200;
        private System.Windows.Forms.ToolStripMenuItem mnu_polygon_num_250;
        private System.Windows.Forms.ToolStripMenuItem mnu_alg_seidel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageNodes;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlDraw;
        private System.Windows.Forms.TabPage tabPageVertices;
        private System.Windows.Forms.DataGridView dgvPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_x;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_y;
        private System.Windows.Forms.ToolStripMenuItem mnu_alg_reset;
        private System.Windows.Forms.ToolStripMenuItem تستToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel lblMousePos;
        private System.Windows.Forms.ToolStripMenuItem statusbar_alg_trapezoidal_decompose;

    }
}

