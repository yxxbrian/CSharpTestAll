using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace MonitorInfoTest
{


    public class Util
    {
        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MonitorInfo lpmi);

        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public long left;
            public long top;
            public long right;
            public long bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class MonitorInfo
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public UInt32 dwFlags;
        }

        public Util() 
        {
            //GetMonitorInfo(MonitorFromWindow();

        }


        internal static bool MonitorCallback( IntPtr hMonitor)
        {
            System.Console.WriteLine("Monitor callback called.");

            MonitorInfo mi = new MonitorInfo();
            mi.cbSize = Marshal.SizeOf(mi);

            bool result = GetMonitorInfo( hMonitor, mi ) ;
            if ( result )
            {
                Console.WriteLine("Successfully called GetMonitorInfo().");
                Console.WriteLine("Monitor position/size is ({0},{1}) - ({2},{3})",
                mi.rcWork.left, mi.rcWork.top,
                mi.rcWork.right, mi.rcWork.bottom );
            }
            else
            {
                Console.WriteLine("Call to GetMonitorInfo failed");
            }

            return result;
            }

            

    }
}
