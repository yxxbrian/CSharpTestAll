using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace AudioBridgeTest
{
    public partial class Form1 : Form
    {
        StreamWriter txtSW;
        FileStream txtFileStream;
        bool txtMessageFlag = false;
        private void logToFile(string msg, string fileName)
        {
            try
            {
                if (txtSW == null)
                {
                    string txtFilePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + fileName + ".txt";//@"\txtFile.txt";
                    txtFileStream = File.Open(txtFilePath, FileMode.Append);
                    //FileStream txtFileStream = new FileStream(txtFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                    txtSW = new StreamWriter(txtFileStream);
                    txtSW.AutoFlush = true;
                }
                txtSW.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t" + msg);
                txtSW.Dispose();
                txtSW = null;
            }
            catch (Exception ex)
            {
                if (txtMessageFlag == false)
                {
                    //XtraMessageBox.Show("无法记录到txt文件：" + ex.Message);
                    txtSW.Close();
                    txtFileStream.Close();
                }

                txtMessageFlag = true;
            }

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.AddExtension = true;
            op.Filter = "Wave file(*.wav)|*.wav|All files(*.*)|*.*";
            Nullable<DialogResult> result = op.ShowDialog();
            if (result == DialogResult.OK)
            {
                WaveFile wf = new WaveFile();
                wf.FILENAME = op.FileName;

                int DebugChannelNum = 0;
                int DebugStartSample_inData = 0;
                int groupCount_inFrame = 0;
                uint frameCount;
                string appendingData;
                bool rtn = wf.GetDebugChannel(ref DebugChannelNum, ref DebugStartSample_inData, ref groupCount_inFrame,ref frameCount);
                if (rtn == true)
                {
                    //存在Debug通道，现在询问用户是否进行扩展
                    //MessageBox.Show("Debug channel exists!");
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (DialogResult.OK == dialog.ShowDialog())
                    {
                        int[] extendingIndexArray = getExtendingIndex();
                        string new_appendingData = null;

                        wf.OpenWaveFileWithAppendingData(out appendingData);

                        if (appendingData != null)
                        {
                            WaveFileAppendingData waveFileAppendingData = JsonConvert.DeserializeObject<WaveFileAppendingData>(appendingData);
                            WaveFileAppendingData newWaveFileAppendingData = new WaveFileAppendingData();
                            foreach (WaveChannelModel waveChannelModel in waveFileAppendingData.Channels) 
                            {
                                if (waveChannelModel.Index < DebugChannelNum) 
                                {
                                    newWaveFileAppendingData.Channels.Add(waveChannelModel);
                                }
                                if (waveChannelModel.Index == DebugChannelNum)
                                {
                                    int offset = 0;
                                    foreach(int extendingIndex in extendingIndexArray)
                                    {
                                        WaveChannelModel wcm = new WaveChannelModel();
                                        wcm.Index = waveChannelModel.Index + offset++;
                                        //通过Config文件获取对应名称
                                        wcm.Name = "D" + extendingIndex;
                                        newWaveFileAppendingData.Channels.Add(wcm);
                                    }
                                }
                                else if (waveChannelModel.Index > DebugChannelNum)
                                {
                                    //
                                }
                            }

                            //已生成newWaveFileAppendingData,下面进行序列化处理
                            new_appendingData = JsonConvert.SerializeObject(waveFileAppendingData);
                        }

                        wf.ExtendWav("D:\\ExtendedWave.wav", DebugChannelNum, DebugStartSample_inData,frameCount, groupCount_inFrame, extendingIndexArray, new_appendingData);
                    }
                }
                else
                {
                    //MessageBox.Show("Debug channel not exists!");
                }
            }
        }

        private int[] getExtendingIndex() 
        {
            return new int[]{1,2};
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.StartupPath+"\\DebugChannelName.config");
            string result = config.ConnectionStrings.ConnectionStrings["ServerIP"].ConnectionString.ToString();
            //string result1 = config.AppSettings
            MessageBox.Show(result);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WaveFile wf = new WaveFile();
            //wf.FILENAME = op.FileName;
            wf.WavToNewTxt("D:\\ExtendedWave.wav","ExtendWav");
        }

        public string GetAppConfig(string strKey)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            file = @"E:\CSharpProjects\ZHPAT_Test\AudioBridgeTest\App.config";
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == strKey)
                {
                    return config.AppSettings.Settings[strKey].Value.ToString();
                }
            }
            return null;
        }



    }
}
