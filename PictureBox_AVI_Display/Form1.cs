using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace PictureBox_AVI_Display
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        static int i = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            string temp = "";
            if (i++ % 2 == 0)
                temp = "左移.avi";
            else
                temp = "右移.avi";
            

            PictureBox PlayScreen = new PictureBox();
            PlayScreen = this.pictureBox1;
            string mciCommand;
            mciCommand = "open " + @"D:\project\DJI_Plane\DJIPlaneSystem_New_Using_VD\DJIPlaneSystem_New\bin\Debug\animations\"+temp + " alias MyAVI";
            mciCommand = mciCommand + " parent " + PlayScreen.Handle.ToInt32() + " style child";
            LibWrap.mciSendString(mciCommand, null, 0, 0);
            Rectangle r = PlayScreen.ClientRectangle;
            mciCommand = "put MyAVI window at 0 0 " + r.Width + " " + r.Height;
            LibWrap.mciSendString(mciCommand, null, 0, 0);
            LibWrap.mciSendString("play MyAVI", null, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LibWrap.mciSendString("pause MyAVI", null, 0, 0);
            LibWrap.mciSendString("close MyAVI", null, 0, 0);
        }
    }


    public class LibWrap
    {
        [DllImport(("winmm.dll"), EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        public static extern int mciSendString(string lpszCommand, string lpszReturnString,
                    uint cchReturn, int hwndCallback);
    }
}
