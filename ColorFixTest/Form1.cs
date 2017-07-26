using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ColorFixTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }


        private void RefreshColor() 
        {
            int R1 = trackBar_R.Value;
            int G1 = trackBar_G.Value;
            int B1 = trackBar_B.Value;
            Color color1 = Color.FromArgb(255,R1,G1,B1);
            this.panel_1_1.BackColor = color1;

            int R2 = trackBar3.Value;
            int G2 = trackBar2.Value;
            int B2 = trackBar1.Value;
            Color color2 = Color.FromArgb(255, R2, G2, B2);
            this.panel2_1.BackColor = color2;

            
            int R3 = trackBar6.Value;
            int G3 = trackBar5.Value;
            int B3 = trackBar4.Value;
            Color color3 = Color.FromArgb(255, R3, G3, B3);
            this.panel3_1.BackColor = color3;

            ///
            int R_fix_12 = (R1 + R2) / 2;
            int G_fix_12 = (G1 + G2) / 2;
            int B_fix_12 = (B1 + B2) / 2;
            Color color_fix_12 = Color.FromArgb(255, R_fix_12, G_fix_12, B_fix_12);
            this.panel_fix_12.BackColor = color_fix_12;

            int R_fix = (R3 + R2) / 2;
            int G_fix = (G3 + G2) / 2;
            int B_fix = (B3 + B2) / 2;
            Color color_fix_23 = Color.FromArgb(255, R_fix, G_fix, B_fix);
            this.panel_fix_23.BackColor = color_fix_23;


        }

        private void trackBar_R_Scroll(object sender, EventArgs e)
        {
            RefreshColor();
            
        }

        private void trackBar_G_Scroll(object sender, EventArgs e)
        {
            RefreshColor();
        }

        private void trackBar_B_Scroll(object sender, EventArgs e)
        {
            RefreshColor();
        }



        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            RefreshColor();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            RefreshColor();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            RefreshColor();
        }



        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            RefreshColor();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            RefreshColor();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            RefreshColor();
        }

    }
}
