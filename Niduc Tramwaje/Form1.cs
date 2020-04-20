using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace Niduc_Tramwaje
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;
            SimulationControl.test_fill_map();
            UpdateMap();
            tmrGraphics.Start();
        }

        void UpdateMap()
        {
            picMap.Image = SimulationControl.Display();
        }

        private void btnTest_RuszTramwajem_Click(object sender, EventArgs e)
        {
            SimulationControl.Simulation();
        }

        private void tmrGraphics_Tick(object sender, EventArgs e)
        {
            UpdateMap();
        }
    }
}
