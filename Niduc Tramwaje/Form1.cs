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
        static TextBox textBox;

        public Form1() {
            InitializeComponent();
            textBox = textBox1;
            
            trackBar1.Value = (int)((trackBar1.Minimum + trackBar1.Maximum) * TramStop.GenerationSpeedSlider);
            TramStop.GenerationSpeedSlider = (float)trackBar1.Value / trackBar1.Maximum;

            trackBar2.Value = (int)((trackBar2.Minimum + trackBar2.Maximum) * SimulationControl.TimeScaleSlider);
            SimulationControl.TimeScaleSlider = (float)trackBar2.Value / trackBar2.Maximum;
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
            
        }

        private void tmrGraphics_Tick(object sender, EventArgs e)
        {
            SimulationControl.Simulation();
            UpdateMap();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        public static void WriteToConsole(string text) {
            textBox.AppendText(text);
            textBox.AppendText("\n");
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            TramStop.GenerationSpeedSlider = (float)trackBar1.Value / trackBar1.Maximum;
        }

        private void trackBar2_Scroll(object sender, EventArgs e) {
            SimulationControl.TimeScaleSlider = (float)trackBar2.Value / trackBar2.Maximum;
        }

        private void label3_Click(object sender, EventArgs e) {

        }
    }
}
