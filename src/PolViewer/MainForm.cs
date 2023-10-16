using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e) => await PopulateGPInfoAsync();

        private static async Task PopulateGPInfoAsync() =>
            await Task.Run(() =>
                {
                    foreach (var item in GPHelper.PopulateGPInfo())
                    {
                        var gpoName = item.Name;
                        var GpoId = item.Guid;
                    }
                });
    }
}