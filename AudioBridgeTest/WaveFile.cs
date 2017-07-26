using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace AudioBridgeTest
{
    public class WaveFileHeadFormat
    {
        /************************************************************************** 
        Here is where the file will be created. A wave file is a RIFF file, which has chunks 
        of data that describe what the file contains. 
        A wave RIFF file is put together like this: 
        The 12 byte RIFF chunk is constructed like this: 
        Bytes 0 ? 3 : ‘R’ ‘I’ ‘F’ ‘F’ 
        Bytes 4 ? 7 : Length of file, minus the first 8 bytes of the RIFF description. 
        (4 bytes for "WAVE" + 24 bytes for format chunk length + 8 bytes for data chunk description + actual sample data size.) 


        Bytes 8 ? 11: ‘W’ ‘A’ ‘V’ ‘E’ 
        The 24 byte FORMAT chunk is constructed like this: 
        Bytes 0 ? 3 : ‘f’ ‘m’ ‘t’ ‘ ‘ 
        Bytes 4 ? 7 : The format chunk length. This is always 16. 
        Bytes 8 ? 9 : File padding. Always 1. 
        Bytes 10- 11: Number of channels. Either 1 for mono, or 2 for stereo. 
        Bytes 12- 15: Sample rate. 
        Bytes 16- 19: Number of bytes per second. 
        Bytes 20- 21: Bytes per sample. 1 for 8 bit mono, 2 for 8 bit stereo or 16 bit mono, 4 for 16 bit stereo. 
        Bytes 22- 23: Number of bits per sample. 

        The DATA chunk is constructed like this: 
        Bytes 0 ? 3 : ‘d’ ‘a’ ‘t’ ‘a’ 
        Bytes 4 ? 7 : Length of data, in bytes. 
        Bytes 8 -…: Actual sample data. 
        ***************************************************************************/
        public char[] ChunkRiff = { 'R', 'I', 'F', 'F' };
        public uint ChunkSize { get; set; }
        public char[] Format = { 'W', 'A', 'V', 'E' };
        public char[] SubChunk1ID = { 'f', 'm', 't', ' ' };
        public uint SubChunk1Size { get; set; }
        public UInt16 AudioFormat { get; set; }
        public UInt16 NumChannels { get; set; }
        public uint SampleRate { get; set; }
        public uint ByteRate { get; set; }
        public UInt16 BlockAlign { get; set; }
        public UInt16 BitsPerSample { get; set; }
        public char[] SubChunk2ID = { 'd', 'a', 't', 'a' };
        public uint SubChunk2Size { get; set; }
    }

    public struct WaveFileHead
    {
        /************************************************************************** 
        Here is where the file will be created. A wave file is a RIFF file, which has chunks 
        of data that describe what the file contains. 
        A wave RIFF file is put together like this: 
        The 12 byte RIFF chunk is constructed like this: 
        Bytes 0 ? 3 : ‘R’ ‘I’ ‘F’ ‘F’ 
        Bytes 4 ? 7 : Length of file, minus the first 8 bytes of the RIFF description. 
        (4 bytes for "WAVE" + 24 bytes for format chunk length + 8 bytes for data chunk description + actual sample data size.) 


        Bytes 8 ? 11: ‘W’ ‘A’ ‘V’ ‘E’ 
        The 24 byte FORMAT chunk is constructed like this: 
        Bytes 0 ? 3 : ‘f’ ‘m’ ‘t’ ‘ ‘ 
        Bytes 4 ? 7 : The format chunk length. This is always 16. 
        Bytes 8 ? 9 : File padding. Always 1. 
        Bytes 10- 11: Number of channels. Either 1 for mono, or 2 for stereo. 
        Bytes 12- 15: Sample rate. 
        Bytes 16- 19: Number of bytes per second. 
        Bytes 20- 21: Bytes per sample. 1 for 8 bit mono, 2 for 8 bit stereo or 16 bit mono, 4 for 16 bit stereo. 
        Bytes 22- 23: Number of bits per sample. 

        The DATA chunk is constructed like this: 
        Bytes 0 ? 3 : ‘d’ ‘a’ ‘t’ ‘a’ 
        Bytes 4 ? 7 : Length of data, in bytes. 
        Bytes 8 -…: Actual sample data. 
        ***************************************************************************/
        public UInt32 ChunkRiff;
        public uint ChunkSize { get; set; }
        public UInt32 Format;
        public UInt32 SubChunk1ID;
        public uint SubChunk1Size { get; set; }
        public UInt16 AudioFormat { get; set; }
        public UInt16 NumChannels { get; set; }
        public uint SampleRate { get; set; }
        public uint ByteRate { get; set; }
        public UInt16 BlockAlign { get; set; }
        public UInt16 BitsPerSample { get; set; }
        public UInt32 SubChunk2ID;
        public uint SubChunk2Size { get; set; }
    }
    public class WaveFile
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


        private WaveFileHeadFormat m_WaveFileHead;
        private Byte[] Data { get; set; }
        private string m_sFileName = null;

        public string FILENAME
        {
            get
            {
                return m_sFileName;
            }
            set
            {
                m_sFileName = value;
                if (!OpenWaveFile())
                    m_sFileName = null;
            }
        }

        public UInt64 SAMPLE_NUM
        {
            get
            {
                if (m_sFileName == null)
                    return 0;

                return (UInt64)(m_WaveFileHead.SubChunk2Size * 8 / (m_WaveFileHead.BitsPerSample * m_WaveFileHead.NumChannels));
            }
        }

        //public uint SampleRate
        //{
        //    get
        //    {
        //        if (m_sFileName == null)
        //            return 0;

        //        return m_WaveFileHead.SampleRate;
        //    }
        //}

        //public UInt16 Channel
        //{
        //    get
        //    {
        //        if (m_sFileName == null)
        //            return 0;

        //        return m_WaveFileHead.NumChannels;
        //    }
        //}

        //public UInt16 Bitdepth
        //{
        //    get
        //    {
        //        if (m_sFileName == null)
        //            return 0;

        //        return m_WaveFileHead.BitsPerSample;
        //    }
        //}

        public WaveFileHeadFormat Head
        {
            get
            {
                return m_WaveFileHead;
            }
        }

        public WaveFile()
        {
            m_WaveFileHead = new WaveFileHeadFormat();
        }

        public bool OpenWaveFile(string filename)
        {
            FILENAME = filename;
            return OpenWaveFile();
        }

        public bool OpenWaveFile()
        {
            FileStream filesr;
            try
            {
                filesr = new FileStream(m_sFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader mReader = new BinaryReader(filesr);

                filesr.Seek(m_WaveFileHead.ChunkRiff.Length, SeekOrigin.Current);
                m_WaveFileHead.ChunkSize = mReader.ReadUInt32();

                filesr.Seek(m_WaveFileHead.Format.Length + m_WaveFileHead.SubChunk1ID.Length, SeekOrigin.Current);
                m_WaveFileHead.SubChunk1Size = mReader.ReadUInt32();

                m_WaveFileHead.AudioFormat = mReader.ReadUInt16();
                m_WaveFileHead.NumChannels = mReader.ReadUInt16();
                m_WaveFileHead.SampleRate = mReader.ReadUInt32();
                m_WaveFileHead.ByteRate = mReader.ReadUInt32();
                m_WaveFileHead.BlockAlign = mReader.ReadUInt16();
                if (m_WaveFileHead.SubChunk1Size == 16)
                    m_WaveFileHead.BitsPerSample = mReader.ReadUInt16();
                else if (m_WaveFileHead.SubChunk1Size == 18)
                    m_WaveFileHead.BitsPerSample = (UInt16)mReader.ReadUInt32();

                filesr.Seek(4, SeekOrigin.Current);//跳过‘d’ ‘a’ ‘t’ ‘a’4个字节 
                m_WaveFileHead.SubChunk2Size = mReader.ReadUInt32();

                //try to read to the last byte of wav data.If can not ,it will throw an Exception.
                if (m_WaveFileHead.SubChunk2Size > 0)
                {
                    filesr.Seek(m_WaveFileHead.SubChunk2Size - 1, SeekOrigin.Current);
                    mReader.ReadByte();
                }

                mReader.Close();
                filesr.Close();
            }
            catch (IOException e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("OpenWaveFile");
                //log.Fatal(e);
                //MessageWindow messageWindow = new MessageWindow(0, 1, "Unidentifiable File!Choose another one!");
                //messageWindow.Owner = Application.Current.MainWindow;
                //messageWindow.ShowDialog();

                return false;
            }
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("OpenWaveFile");
                //log.Fatal(e);
                //MessageWindow messageWindow = new MessageWindow(0, 1, "Failed to open file:" + m_sFileName + "!");
                //messageWindow.Owner = Application.Current.MainWindow;
                //messageWindow.ShowDialog();

                return false;
            }
            return true;
        }

        FileStream filesr;
        BinaryReader mReader;

        //Wav Data部分转换成txt
        public bool WavToNewTxt(string filePath,string txtName)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + txtName))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + txtName);
            }

            


            
            try
            {
                if (filesr != null)
                    filesr.Close();
                if (mReader != null)
                    mReader.Close();
                filesr = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                mReader = new BinaryReader(filesr);

                filesr.Seek(22, SeekOrigin.Begin);
                int NumChannels = mReader.ReadUInt16();
                filesr.Seek(20, SeekOrigin.Current);
                //if (m_WaveFileHead.SubChunk2Size > 0)
                {
                    //if (m_WaveFileHead.BitsPerSample == 16)
                    {
                        int indexSample = 0;
                        while (indexSample < 256 * NumChannels)
                        {
                            logToFile("\r\nGroup:" + indexSample + "\tNumChannels:" + NumChannels, txtName);
                            for (int i = 0; i < NumChannels; i++)
                            {
                                //MessageBox.Show(mReader.ReadInt16().ToString("X"));
                                logToFile(mReader.ReadUInt16().ToString("X"), txtName);
                                continue;
                            }
                            indexSample++;
                        }
                    }
                    //else
                    //{
                    //    throw new NotImplementedException();
                    //}
                }
                if (filesr != null)
                    filesr.Close();
                if (mReader != null)
                    mReader.Close();
            }
            catch (IOException e)
            {
                if (filesr != null)
                    filesr.Close();
                if (mReader != null)
                    mReader.Close();
                return false;
            }
            catch (Exception e)
            {
                if (filesr != null)
                    filesr.Close();
                if (mReader != null)
                    mReader.Close();
                return false;
            }
            return false;
        }

        /// <summary>
        /// 获取Debug Channel相关信息
        /// </summary>
        /// <param name="DebugChannelNum">Debug channel的通道号</param>
        /// <param name="DebugStartSample_inData">有效Debug sample在Data中的位置</param>
        /// <param name="groupCount_inFrame">一个frame中包含的组数</param>
        /// <returns></returns>
        public bool GetDebugChannel(ref int DebugChannelNum, ref int DebugStartSample_inData,ref int groupCount_inFrame,ref uint frameCount)
        {
            bool isDebugChannelExist = false;
            DebugChannelNum = 0;
            DebugStartSample_inData = 0;
            groupCount_inFrame = 0;
            
            FileStream filesr;
            try
            {
                filesr = new FileStream(m_sFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader mReader = new BinaryReader(filesr);

                filesr.Seek(m_WaveFileHead.ChunkRiff.Length, SeekOrigin.Current);
                m_WaveFileHead.ChunkSize = mReader.ReadUInt32();
                filesr.Seek(m_WaveFileHead.Format.Length + m_WaveFileHead.SubChunk1ID.Length, SeekOrigin.Current);
                m_WaveFileHead.SubChunk1Size = mReader.ReadUInt32();
                m_WaveFileHead.AudioFormat = mReader.ReadUInt16();
                m_WaveFileHead.NumChannels = mReader.ReadUInt16();
                m_WaveFileHead.SampleRate = mReader.ReadUInt32();
                m_WaveFileHead.ByteRate = mReader.ReadUInt32();
                m_WaveFileHead.BlockAlign = mReader.ReadUInt16();
                if (m_WaveFileHead.SubChunk1Size == 16)
                    m_WaveFileHead.BitsPerSample = mReader.ReadUInt16();
                else if (m_WaveFileHead.SubChunk1Size == 18)
                    m_WaveFileHead.BitsPerSample = (UInt16)mReader.ReadUInt32();

                filesr.Seek(4, SeekOrigin.Current);//跳过‘d’ ‘a’ ‘t’ ‘a’4个字节 
                m_WaveFileHead.SubChunk2Size = mReader.ReadUInt32();

                
                if (m_WaveFileHead.SubChunk2Size > 0)
                {
                    if (m_WaveFileHead.BitsPerSample == 16)
                    {
                        int indexSample = 0;
                        while (indexSample < 256 * m_WaveFileHead.NumChannels && isDebugChannelExist == false)
                        {
                            //logToFile("\r\nGroup:"+indexSample+"\tNumChannels:"+m_WaveFileHead.NumChannels, "Log");
                            for (int i = 0; i < m_WaveFileHead.NumChannels; i++)
                            {
                                //logToFile(mReader.ReadUInt16().ToString("X"), "Log");
                                //MessageBox.Show(mReader.ReadUInt16().ToString());
                                //continue;
                                if (mReader.ReadUInt16() == 0xadde) 
                                {
                                    indexSample++;
                                    filesr.Seek(2 * (m_WaveFileHead.NumChannels - 1), SeekOrigin.Current);
                                    indexSample += (m_WaveFileHead.NumChannels - 1);
                                    if (mReader.ReadUInt16() == 0xefbe)
                                    {
                                        indexSample++;
                                        filesr.Seek(2 * (m_WaveFileHead.NumChannels - 1), SeekOrigin.Current);
                                        indexSample += (m_WaveFileHead.NumChannels - 1);
                                        if (isDebugChannelExist == false)
                                        {
                                            isDebugChannelExist = true;
                                            DebugChannelNum = i;
                                            DebugStartSample_inData = indexSample;

                                            //已经找到了第一个dead beef，开始找第二个，并确定frameLength
                                            groupCount_inFrame = 0;
                                            for (int j = 0; j < 256; j++)
                                            {
                                                if (mReader.ReadUInt16() == 0xadde) 
                                                {
                                                    filesr.Seek(2 * (m_WaveFileHead.NumChannels - 1), SeekOrigin.Current);
                                                    groupCount_inFrame++;
                                                    
                                                    if (mReader.ReadUInt16() == 0xefbe)
                                                    {
                                                        filesr.Seek(2 * (m_WaveFileHead.NumChannels - 1), SeekOrigin.Current);
                                                        groupCount_inFrame++;

                                                        //此时帧长度检测完毕，计算Data中包含的帧数
                                                        frameCount = m_WaveFileHead.SubChunk2Size / (uint)(m_WaveFileHead.NumChannels * m_WaveFileHead.BitsPerSample / 8 * groupCount_inFrame);
                                                        return true;
                                                    }
                                                    else 
                                                    {
                                                        filesr.Seek(2 * (m_WaveFileHead.NumChannels - 1), SeekOrigin.Current);
                                                        groupCount_inFrame++;
                                                    }
                                                }
                                                else
                                                {
                                                    filesr.Seek(2 * (m_WaveFileHead.NumChannels - 1), SeekOrigin.Current);
                                                    groupCount_inFrame++;
                                                }
                                            }
                                        }
                                    }
                                    else 
                                    {
                                        indexSample++;
                                        filesr.Seek(-2 * m_WaveFileHead.NumChannels, SeekOrigin.Current);
                                        indexSample -= m_WaveFileHead.NumChannels;
                                    }
                                }
                                else 
                                {
                                    indexSample++;
                                }
                            }
                            
                            indexSample++;
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                mReader.Close();
                filesr.Close();
            }
            catch (IOException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }


        /// <summary>
        /// 扩展原m_sFileName文件到新的m_dFilename文件
        /// </summary>
        /// <param name="m_dFilename">目标文件路径</param>
        /// <param name="DebugChannelNum">调试信息所在的Channel</param>
        /// <param name="DebugStartPosition">Data部分有效Debug信息起始位置（单位sample）</param>
        /// <param name="frameLength">单个frame的长度（单位：sample）</param>
        /// <param name="extendingIndex">即将扩展的Debug信息通道号</param>
        /// <param name="appendingData">附加信息的数据部分</param>
        /// <returns></returns>
        public bool ExtendWav(string m_dFilename, int DebugChannelNum, int DebugStartSample_inData,uint frameCount, int groupCount_inFrame, int[] extendingIndex, string appendingData) 
        {
            FileStream filesr;
            try
            {
                //读原wave文件流
                filesr = new FileStream(m_sFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader mReader = new BinaryReader(filesr);

                //文件Data部分包含的帧数
                
                //通过增加变化值的方式计算新的参数
                UInt16 new_NumChannels = (UInt16)(m_WaveFileHead.NumChannels + extendingIndex.Length-1);
                uint new_ByteRate = m_WaveFileHead.NumChannels * m_WaveFileHead.SampleRate * m_WaveFileHead.BitsPerSample / 8;
                ushort new_BlockAlign = (ushort)(m_WaveFileHead.NumChannels * m_WaveFileHead.BitsPerSample / 8);
                uint new_SubChunk2Size = m_WaveFileHead.SubChunk2Size + (uint)((extendingIndex.Length - 1) * frameCount * groupCount_inFrame * m_WaveFileHead.BitsPerSample / 8);
                uint new_ChunkSize = 16 + m_WaveFileHead.SubChunk1Size + 8 + m_WaveFileHead.SubChunk2Size;
                
                if (!string.IsNullOrWhiteSpace(appendingData))
                {
                    new_ChunkSize += (uint)(4 + 4 + appendingData.Length);
                }

                if (File.Exists(m_dFilename))
                {
                    File.Delete(m_dFilename);
                }

                //写目标文件流
                FileStream wStream = new FileStream(m_dFilename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bWrite = new BinaryWriter(wStream);

                bWrite.Write(m_WaveFileHead.ChunkRiff);
                bWrite.Write(new_ChunkSize);
                bWrite.Write(m_WaveFileHead.Format);
                bWrite.Write(m_WaveFileHead.SubChunk1ID);
                bWrite.Write(m_WaveFileHead.SubChunk1Size);
                bWrite.Write(m_WaveFileHead.AudioFormat);
                bWrite.Write(new_NumChannels);
                bWrite.Write(m_WaveFileHead.SampleRate);
                bWrite.Write(new_ByteRate);
                bWrite.Write(new_BlockAlign);
                bWrite.Write(m_WaveFileHead.BitsPerSample);
                bWrite.Write(m_WaveFileHead.SubChunk2ID);
                bWrite.Write(new_SubChunk2Size);


                for (uint frameIndex = 0; frameIndex < frameCount; frameIndex++) 
                {
                    //获取一帧内的全部40个debug sample
                    UInt16[] debugSamples = new UInt16[40];
                    for(int i=0;i<40;i++)
                    {
                        filesr.Seek(44 + (DebugStartSample_inData + (frameIndex * groupCount_inFrame + i) * m_WaveFileHead.NumChannels) * 2, SeekOrigin.Begin);
                        debugSamples[i] = mReader.ReadUInt16();
                    }

                    //遍历一帧中的每一组
                    for (uint groupIndex = 0; groupIndex < groupCount_inFrame; groupIndex++) 
                    {
                        //遍历每一组中的每一个通道
                        for (uint groupOffset = 0; groupOffset < m_WaveFileHead.NumChannels; groupOffset++)
                        {
                            filesr.Seek(44 + (frameIndex * groupCount_inFrame + groupIndex) * m_WaveFileHead.NumChannels * 2, SeekOrigin.Begin);
                            if (groupOffset < DebugChannelNum)
                            {
                                bWrite.Write(mReader.ReadUInt16());
                            }
                            else if (groupOffset == DebugChannelNum)
                            {
                                for(int j=0;j<extendingIndex.Length;j++)
                                {
                                    bWrite.Write(debugSamples[extendingIndex[j]]);
                                }
                            }
                            else 
                            {
                                bWrite.Write(mReader.ReadUInt16());
                            }
                        }
                    }
                }

                //Data信息写入完毕，下面写AppendingData信息
                //写入追加的信息，格式：{‘F', 'M', 'E', 'X'}|  int（数据量）|  data(数据)
                if (!string.IsNullOrWhiteSpace(appendingData))
                {
                    //标识
                    char[] MyChunkID = { 'F', 'M', 'E', 'X' };
                    bWrite.Write(MyChunkID);
                    //长度
                    bWrite.Write(appendingData.Length);
                    //信息
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(appendingData);
                    bWrite.Write(data);
                }

                bWrite.Close();
                wStream.Close();
                
                mReader.Close();
                filesr.Close();
                return true;


                //最后要把这个Wavefile对象销毁
            }
            catch (IOException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        

        public bool OpenWaveFileWithAppendingData(out string appendingData)
        {
            FileStream filesr;
            appendingData = null;
            try
            {
                filesr = new FileStream(m_sFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader mReader = new BinaryReader(filesr);

                filesr.Seek(m_WaveFileHead.ChunkRiff.Length, SeekOrigin.Current);
                m_WaveFileHead.ChunkSize = mReader.ReadUInt32();

                filesr.Seek(m_WaveFileHead.Format.Length + m_WaveFileHead.SubChunk1ID.Length, SeekOrigin.Current);
                m_WaveFileHead.SubChunk1Size = mReader.ReadUInt32();

                m_WaveFileHead.AudioFormat = mReader.ReadUInt16();
                m_WaveFileHead.NumChannels = mReader.ReadUInt16();
                m_WaveFileHead.SampleRate = mReader.ReadUInt32();
                m_WaveFileHead.ByteRate = mReader.ReadUInt32();
                m_WaveFileHead.BlockAlign = mReader.ReadUInt16();
                if (m_WaveFileHead.SubChunk1Size == 16)
                    m_WaveFileHead.BitsPerSample = mReader.ReadUInt16();
                else if (m_WaveFileHead.SubChunk1Size == 18)
                    m_WaveFileHead.BitsPerSample = (UInt16)mReader.ReadUInt32();



                filesr.Seek(4, SeekOrigin.Current);//跳过‘d’ ‘a’ ‘t’ ‘a’4个字节 
                m_WaveFileHead.SubChunk2Size = mReader.ReadUInt32();

                //try to read to the last byte of wav data.If can not ,it will throw an Exception.
                if (m_WaveFileHead.SubChunk2Size > 0)
                {
                    filesr.Seek(m_WaveFileHead.SubChunk2Size - 1, SeekOrigin.Current);
                    mReader.ReadByte();
                }

                //Format:4byte
                //SubChunk1ID:4byte
                //SubChunk1Size:4byte
                //SubChunk2ID:4byte
                //SubChunk2Size:4byte
                if (m_WaveFileHead.ChunkSize > 4 + 4 + 4 + m_WaveFileHead.SubChunk1Size + 4 + 4 + m_WaveFileHead.SubChunk2Size)
                {
                    //读取追加到文件里的信息
                    char[] MyChunckId = { 'F', 'M', 'E', 'X' };
                    bool isMyChunck = true;
                    char[] fileChunckID = mReader.ReadChars(4);
                    for (int i = 0; i < fileChunckID.Length; i++)
                    {
                        if (fileChunckID[i] != MyChunckId[i])
                        {
                            isMyChunck = false;
                            break;
                        }
                    }
                    if (isMyChunck)
                    {
                        int appendingDataSize = (int)mReader.ReadUInt32();
                        byte[] data = new byte[appendingDataSize];
                        data = mReader.ReadBytes(appendingDataSize);
                        appendingData = System.Text.Encoding.UTF8.GetString(data);
                    }
                }

                mReader.Close();
                filesr.Close();
            }
            catch (IOException e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("OpenWaveFileWithAppendingData");
                //log.Fatal(e);
                return false;
            }
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("OpenWaveFileWithAppendingData");
                //log.Fatal(e);
                return false;
            }
            return true;
        }
        
        public bool SetFileName(string sFilename, WaveformSetting inSetting)
        {
            m_sFileName = sFilename;

            m_WaveFileHead.AudioFormat = 1;
            m_WaveFileHead.NumChannels = (ushort)inSetting.WAVE_TOTAL;
            m_WaveFileHead.SampleRate = (uint)inSetting.SampleRate;
            m_WaveFileHead.BitsPerSample = 16;
            m_WaveFileHead.ByteRate = m_WaveFileHead.NumChannels * m_WaveFileHead.SampleRate * m_WaveFileHead.BitsPerSample / 8;
            m_WaveFileHead.BlockAlign = (ushort)(m_WaveFileHead.NumChannels * m_WaveFileHead.BitsPerSample / 8);

            return true;
        }
        

        
        public bool WriteToFile(string sFilename, WaveformSetting inSetting, MemoryMappedFileManager mmfm, Int64 startSample, Int64 endSample)
        {
            m_WaveFileHead.AudioFormat = 1;
            m_WaveFileHead.NumChannels = (ushort)inSetting.WAVE_TOTAL;
            m_WaveFileHead.SampleRate = (uint)inSetting.SampleRate;
            m_WaveFileHead.BitsPerSample = 16;
            m_WaveFileHead.ByteRate = m_WaveFileHead.NumChannels * m_WaveFileHead.SampleRate * m_WaveFileHead.BitsPerSample / 8;
            m_WaveFileHead.BlockAlign = (ushort)(m_WaveFileHead.NumChannels * m_WaveFileHead.BitsPerSample / 8);
            m_WaveFileHead.SubChunk1Size = 16;
            m_WaveFileHead.SubChunk2Size = (uint)(endSample - startSample) * (uint)m_WaveFileHead.NumChannels * m_WaveFileHead.BitsPerSample / 8;
            m_WaveFileHead.ChunkSize = 16 + m_WaveFileHead.SubChunk1Size + 8 + m_WaveFileHead.SubChunk2Size;

            if (File.Exists(sFilename))
            {
                File.Delete(sFilename);
            }

            FileStream wStream = new FileStream(sFilename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            BinaryWriter bWrite = new BinaryWriter(wStream);

            bWrite.Write(m_WaveFileHead.ChunkRiff);
            bWrite.Write(m_WaveFileHead.ChunkSize);
            bWrite.Write(m_WaveFileHead.Format);
            bWrite.Write(m_WaveFileHead.SubChunk1ID);
            bWrite.Write(m_WaveFileHead.SubChunk1Size);
            bWrite.Write(m_WaveFileHead.AudioFormat);
            bWrite.Write(m_WaveFileHead.NumChannels);
            bWrite.Write(m_WaveFileHead.SampleRate);
            bWrite.Write(m_WaveFileHead.ByteRate);
            bWrite.Write(m_WaveFileHead.BlockAlign);
            bWrite.Write(m_WaveFileHead.BitsPerSample);
            bWrite.Write(m_WaveFileHead.SubChunk2ID);
            bWrite.Write(m_WaveFileHead.SubChunk2Size);

            for (Int64 i = startSample; i < endSample; i++)
            {
                for (int j = 0; j < m_WaveFileHead.NumChannels; j++)
                {
                    bWrite.Write(mmfm.GetData(j, i));
                }
            }

            bWrite.Close();
            wStream.Close();
            return true;
        }
        

    }
}
