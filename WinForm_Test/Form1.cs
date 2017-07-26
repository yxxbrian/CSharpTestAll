using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForm_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Variable_Declared_Null_Or_Not();
            //distanceFormDeltaGPS(longitude1, latitude1, longitude2, latitude2);
            //MessageBox.Show(getDistance222(new PointF((float)longitude1,(float)latitude1),new PointF((float)longitude2,(float)latitude2)).ToString());
            getDistance333(longitude2, latitude2, longitude1, latitude1);
            MessageBox.Show("start:" + string.Format("{0:f15}", start) + "\r\nend :" + string.Format("{0:f15}", end)+"\r\n"+(end-start)*6378137);
        }

        private void Variable_Declared_Null_Or_Not() 
        {
            ListBox listBox = null;
            MessageBox.Show("listBox declared but not initialized");
            listBox = new ListBox();
            MessageBox.Show("listBox initialzied, not null");
        }


        double longitude1 = 118.78333;
        double latitude1 = 32.05000;
        double longitude2 = 116.41667;
        double latitude2 = 39.91667;
        double longitude3 = 116.58;
        double latitude3 = 33.38;
        double longitude4 = 114.10000;
        double latitude4 = 22.20000;
        private void distanceFormDeltaGPS(double long1, double lat1, double long2, double lat2) 
        {
            double rad1x = long1 * (Math.PI) / 180;
            double rad1y = lat1 * (Math.PI) / 180;
            double rad2x = long2 * (Math.PI) / 180;
            double rad2y = lat2 * (Math.PI) / 180;
            double P = Math.Asin(Math.Pow(Math.Sin((rad2x-rad1x)/2),2)+Math.Cos(rad1x)*Math.Cos(rad2x)*Math.Pow(Math.Sin((rad2y-rad1y)/2),2));
            double distance = 2 * 6378137 * Math.Asin(Math.Sqrt(P));
            MessageBox.Show("distance is "+string.Format("{0:f1}",distance));
        }


        double getDistance(PointF p1, PointF p2) 
        {
            int r = 6378137;
            var x1 = p1.X * Math.PI / 180;
            var x2 = p2.X * Math.PI / 180;
            var y1 = p1.Y * Math.PI / 180;
            var y2 = p2.Y * Math.PI / 180;
            var dx = Math.Abs(x1 - x2);
            var dy = Math.Abs(y1 - y2); 
            var p = Math.Pow(Math.Sin(dx / 2), 2) + Math.Cos(x1) * Math.Cos(x2) * Math.Pow(Math.Sin(dy / 2), 2);
            var d= r * 2 * Math.Asin(Math.Sqrt(p));  
            return d;
        }

        double getDistance222(PointF p1, PointF p2) 
        {
            int r = 6378137;
            var x1 = p1.X * Math.PI / 180;
            var x2 = p2.X * Math.PI / 180;
            var y1 = p1.Y * Math.PI / 180;
            var y2 = p2.Y * Math.PI / 180;
            var dx = Math.Abs(x1 - x2);
            var dy = Math.Abs(y1 - y2);
            double value = Math.Sqrt(Math.Pow(dx * r, 2) + Math.Pow(dx * r, 2));
            return value;
        }

        void getDistance333(double p1X, double p1Y,double p2X,double p2Y) 
        {
            int r = 6378137;
            var x1 = p1X * Math.PI / 180;
            var x2 = p2X * Math.PI / 180;
            var y1 = p1Y * Math.PI / 180;
            var y2 = p2Y * Math.PI / 180;
            var dx = Math.Abs(x1 - x2);
            var dy = Math.Abs(y1 - y2);
            double phy = Math.Acos(Math.Cos(y1)*Math.Cos(y2)*Math.Cos(x2-x1)+Math.Sin(y1)*Math.Sin(y2));
            double distance = phy * r;
            double distance1 = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2)) * r;
            MessageBox.Show(distance.ToString()+"\r\n"+distance1.ToString());
        }

        static byte[] startBytes = new byte[]{0x4E,0xEC,0x4C,0xBC,0xC5,0xD2,0xFF,0x3F};
        static byte[] Bytes1 = new byte[]{0x4E,0xEC,0x00,0xC5,0xD2,0xFF,0x3F,0xD4};
        static byte[] Bytes2 = new byte[] {0xB7,0x78,0x83,0x47,0XC7,0xD2,0xFF,0x3F};
        static byte[] Bytes3 = new byte[] {0xFB,0x96,0x87,0x48,0xC8,0xD2,0xFF,0x3F};
        double start = BitConverter.ToDouble(startBytes,0)/Math.PI*180;
        double end = BitConverter.ToDouble(Bytes3, 0) / Math.PI * 180;
    }
}
