using Edytor_mapy.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Niduc_Tramwaje;
using System.Reflection;

namespace Edytor_mapy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            picMap.Image = Resources.Wrocław;
        }

        private void btnDodajPrzystanek_Click(object sender, EventArgs e)
        {
            
        }
    }
}
