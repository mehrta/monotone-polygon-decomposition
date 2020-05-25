using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gui
{
    public partial class FormAlgorithmParameters : Form
    {
        public int MaximumDepthTraverse { set; get; }

        public FormAlgorithmParameters()
        {
            InitializeComponent();
        }

        private void FormAlgorithmParameters_Load(object sender, EventArgs e)
        {
            numMaxDepthTraverse.Value = MaximumDepthTraverse;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            MaximumDepthTraverse = (int)numMaxDepthTraverse.Value;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

        }
    }
}
