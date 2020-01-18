using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryGuard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;

            string[] protectedProcessFileNames = Properties.Settings.Default.protectedProcessFileNames.ToString().Split(';');
            ProcessGuard pGuard = new ProcessGuard(protectedProcessFileNames);
            pGuard.protectProcess();
        }
    }
}
