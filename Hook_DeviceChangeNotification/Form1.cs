using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Hook_DeviceChangeNotification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += OnMainWindowLoaded;
            Hook_Singleton.USBDetector.Instance.MsgShow();
        }

        

        

        private void OnMainWindowLoaded(object sender,EventArgs e)
        {
            stateChanged += Form1_stateChanged;

        }

        void Form1_stateChanged(Form1.USBChangeType changeType)
        {
            MessageBox.Show(changeType.ToString());
        }

        

        /// <summary>
        /// USB_CHANGE_MESSAGE
        /// </summary>

        private const int WM_DEVICECHANGE = 0x0219;
        public enum WM_DEVICECHANGE_MSG
        {
            DBT_DEVICEARRIVAL = 0x8000,
            DBT_DEVICEQUERYREMOVE = 0x8001,
            DBT_DEVICEREMOVECOMPLETE = 0x8004,
            DBT_CONFIGCHANGECANCELED = 0x19,
            DBT_CONFIGCHANGED = 0x18,
            DBT_CUSTOMEVENT = 0x8006,
            DBT_DEVICEQUERYREMOVEFAILED = 0x8002,
            DBT_DEVICEREMOVEPENDING = 0x8003,
            DBT_DEVICETYPESPECIFIC = 0x8005,
            DBT_DEVNODES_CHANGED = 0x7,
            DBT_QUERYCHANGECONFIG = 0x17,
            DBT_USERDEFINED = 0xFFFF
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_DEVICECHANGE && m.LParam != IntPtr.Zero) 
            {
                switch (m.WParam.ToInt32()) 
                {
                    case (Int32)WM_DEVICECHANGE_MSG.DBT_DEVICEARRIVAL:
                        SignalDeviceChange(USBChangeType.added);
                        break;
                    case (Int32)WM_DEVICECHANGE_MSG.DBT_DEVICEQUERYREMOVE:
                        SignalDeviceChange(USBChangeType.removing);
                        break;
                    case (Int32)WM_DEVICECHANGE_MSG.DBT_DEVICEREMOVECOMPLETE:
                        SignalDeviceChange(USBChangeType.removed);
                        break;
                    default:
                        break;


                }

            }

        }

        private void SignalDeviceChange(USBChangeType type) 
        {
            if (stateChanged != null) 
            {
                stateChanged(type);
            }
        }


        public delegate void USBStateChangedEventHandler(USBChangeType changeType);
        public event USBStateChangedEventHandler stateChanged;


        public enum USBChangeType 
        {
            added,
            removing,
            removed
        }


    }
}
