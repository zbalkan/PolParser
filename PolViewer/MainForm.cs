using System;
using System.Windows.Forms;

namespace PolViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            foreach (var item in GPHelper.PopulateGPInfo())
            {
                var gpoName = item.Name;
                var GpoId = item.Guid;
            }
        }
    }
}