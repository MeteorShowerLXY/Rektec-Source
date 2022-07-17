using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rektec.LXY.DataMigrate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnFlow_Click(object sender, EventArgs e)
        {
            ImportFlow importFlow = new ImportFlow();
            importFlow.ShowDialog();
        }

        private void btnStandard_Click(object sender, EventArgs e)
        {
            ImportStandard importStandard = new ImportStandard();
            importStandard.ShowDialog();
        }
    }
}
