using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Transmax_Programming_Test
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Main From Load Fuction
        private void MainFrom_Load(object sender, EventArgs e)
        {
            // Load window in the middle of the screen
            int Width = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int Height = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Location = new Point(Width, Height);
            // on Load - Disable Run Button
            btnRun.Enabled = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog LoadInputFile = new OpenFileDialog();
            LoadInputFile.Title = "Select Text File";
            LoadInputFile.Filter = "Text Files|inputFile.txt|All Files|*.*";
            DialogResult LoadInputFileResult = LoadInputFile.ShowDialog();

            if (LoadInputFileResult == DialogResult.OK)
            {
                if (LoadInputFile.FileName.ToString() != "")
                {
                    txtFilePath.Text = LoadInputFile.FileName;
                    btnRun.Enabled = true;
                }
            }

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Program.readFile(txtFilePath.Text);
        }


    }
}
