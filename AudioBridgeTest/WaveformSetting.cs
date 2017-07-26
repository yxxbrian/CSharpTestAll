using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioBridgeTest
{
    [Serializable]
    public class TestState
    {
        private TestStateValue m_States = TestStateValue.Idling;

        public bool isState(TestStateValue inState)
        {
            if (m_States == inState)
                return true;
            else if (m_States == TestStateValue.TestEditing && inState == TestStateValue.Testing)
            {
                return true;
            }
            else if (m_States == TestStateValue.AdvanceEditing && inState == TestStateValue.Editing)
            {
                return true;
            }
            else
                return false;
        }

        public void setState(TestStateValue inState)
        {
            m_States = inState;
        }

        public bool isRealState(TestStateValue inState)
        {
            if (m_States == inState)
                return true;
            else
                return false;
        }
    }

    public enum TestStateValue
    {
        Testing = 0x1,
        Editing = 0x2,
        Playing = 0x4,
        Idling = 0x8,
        TestEditing = 0x10,
        AdvanceEditing = 0x20,
    }


    public enum WaveType
    {
        Recording,
        Playing,
    }

    public class PointPair
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public bool DrawPoint1 = false;
        public bool DrawPoint2 = false;

        public PointPair(double inX1, double inY1, double inX2, double inY2, bool bDraw1 = false, bool bDraw2 = false)
        {
            X1 = inX1;
            Y1 = inY1;
            X2 = inX2;
            Y2 = inY2;

            DrawPoint1 = bDraw1;
            DrawPoint2 = bDraw2;
        }
    }

    public class WAVE_TOOLS
    {
        //public static void SetLinePoint(Line line, double X1, double Y1, double X2, double Y2)
        //{
        //    if (line != null)
        //    {
        //        line.X1 = X1;
        //        line.Y1 = Y1;
        //        line.X2 = X2;
        //        line.Y2 = Y2;
        //    }
        //}
    }

    public class DataBlock
    {
        //public Line RulerLine { get; set; }
        //public TextBlock TextBlock { get; set; }
        //public TextBox Textbox { get; set; }
        //public int CH { get; set; }
    }

    public enum RulerSpace
    {
        ratio1X = 10000,
        ratio2X = 5000,
        ratio4X = 2500,
        ratio8X = 1250,
        ratio16X = 650,
        ratio32X = 300,
        ratio64X = 150,
        ratio128X = 50,
        ratio256X = 20,
        ratio512X = 10,
        ratio1024X = 5,
        ratio2048X = 2,
        ratio4096X = 1,
    };

    public enum RulerSpaceMedium
    {
        ratio1X = 20000,
        ratio2X = 10000,
        ratio4X = 5000,
        ratio8X = 2500,
        ratio16X = 1250,
        ratio32X = 300,
        ratio64X = 150,
        ratio128X = 50,
        ratio256X = 20,
        ratio512X = 10,
        ratio1024X = 1,
    };

    public enum RulerSpaceSmall
    {
        ratio1X = 30000,
        ratio2X = 15000,
        ratio4X = 7500,
        ratio8X = 3750,
        ratio16X = 1950,
    };

    [Serializable]
    public class WaveformSetting
    {
        public double XRATIO { get; set; }
        public double YRATIO { get; set; }
        public double RECORD_RATIO { get; set; }

        public const double DEF_RECORD_DISTANCE_INITIAL = 1;
        public const double DEF_RECORD_DISTANCE_MAX = 8;
        public double DEF_RECORD_DISTANCE { get; set; }
        public double PAINT_PIXEL { get; set; }
        public const double PAINT_PIXEL_PERIMG = 8;

        public double WAVE_HEIGHT_MAX_THRES = 0;
        public double WAVE_HEIGHT_MIN_THRES 
        {
            get{
                if (WAVE_TOTAL != 0)
                    return WAVE_HEIGHT_MAX_THRES / WAVE_TOTAL;
                else
                    return WAVE_HEIGHT_MAX_THRES;
            }
        }

        public List<double> WAVE_HEIGHT_STACK;

        public double WAVE_HEIGHT { get; set; }
        public int WAVE_TOTAL { get; set; }

        public const double BITRATE_MAX = Int16.MaxValue;
        public double DEF_BITRATE_MAX { get { return BITRATE_MAX; } }

        public TestState STATES;
        public string FILE_NAME { get; set; }

        //User can set channel name in the record/play settings window.And we must show it in our WavRecord/WavPlay window.
        //So i add these properties.
        
        //public List<ChannelViewModel> SaveInFileChannels { get; set; }
        //public List<ChannelViewModel> PlayOutChannels { get; set; }
        
        
        //用于确定标尺区的channel名是否可更改
        public bool ChannelNameIsReadOnly { get; set; }

        ////Samsoft Input
        //public List<Samsoft> Samsofts { get; set; }
        /*
         * WindowMode 
         * 0:play mode;     use PlayOutChannels
         * 1:record mode;   use SaveInFileChannels
         */
        public int WindowMode { get; set; }

        public double SAMPLE_PER_SECOND { get; set; }
        public const double SAMPLE_PER_SECOND_INITIAL = 100;
        private Int64 SAMPLERATE_DEFINE;
        public Int64 SAMPLE_MAX_DRAW = 1000;
        private double[] SAMPLE_RATE_SAMPLEING8K = { 2, 4, 10, 20, 50, 100, 160, 200, 320, 400, 500, 800, 1000, 1600, 2000, 4000, 8000 };
        private double[] SAMPLE_RATE_SAMPLEING16K = { 2, 4, 10, 20, 50, 100, 160, 200, 320, 400, 500, 800, 1000, 1600, 2000, 4000, 8000, 16000 };
        private double[] SAMPLE_RATE_SAMPLEING24K = { 2, 4, 10, 20, 50, 100, 120, 150, 160, 192, 200, 240, 250, 300, 320, 400, 480, 500, 600, 750, 800, 960, 1000, 1200, 1500, 1600, 2000, 3000, 4000, 4800, 6000, 8000, 12000, 24000 };
        private double[] SAMPLE_RATE_SAMPLEING32K = { 2, 4, 10, 20, 50, 100, 160, 200, 320, 400, 500, 800, 1000, 1600, 2000, 4000, 8000, 16000, 32000 };
        private double[] SAMPLE_RATE_SAMPLEING441K = { 2, 4, 10, 20, 50, 100, 126, 140, 150, 180, 196, 210, 252, 294, 300, 350, 420, 450, 490, 588, 630, 700, 882, 900, 980, 1050, 1260, 1470, 1764, 2100, 2450, 2940, 3150, 4410, 4900, 6300, 7350, 8820, 14700, 22050, 44100 };
        private double[] SAMPLE_RATE_SAMPLEING48K = { 2, 4, 10, 20, 50, 100, 160, 200, 320, 400, 500, 800, 1000, 1600, 2000, 4000, 8000, 9600, 12000, 16000, 24000, 48000 };
        private double[] SAMPLE_RATE_SAMPLEING = { 2, 4, 10, 20, 50, 100 };
        //public double SAMPLE_MINSUMMRY_SAMPER = 2 ;
        public double SAMPLE_MINSUMMRY_THORED = 2.0;
        public double SAMPLE_MINSUMMRY_XRATIO ;

        public Int64 SampleRate
        {
            get
            {
                return SAMPLERATE_DEFINE;
            }
            set
            {
                SAMPLERATE_DEFINE = value;
                SAMPLE_PER_SECOND = SAMPLE_PER_SECOND_INITIAL;
            }
        }

        public WaveformSetting()
        {
            WAVE_TOTAL = 3;
            RECORD_RATIO = 1;
            //SaveInFileChannels = null;
            //PlayOutChannels = null;
            WindowMode = -1;
            ChannelNameIsReadOnly = false;
            STATES = new TestState();
            // initial
            Initial();
            WAVE_HEIGHT_STACK = new List<double>();

            int PowerTimesMini = 0;
            int PowerSummury = 0;

            for (int i = 0; i < SAMPLE_RATE_SAMPLEING.Length; i++)
            {
                if (SAMPLE_RATE_SAMPLEING[i] == SAMPLE_MINSUMMRY_THORED)
                {
                    PowerTimesMini = i;
                }

                if (SAMPLE_RATE_SAMPLEING[i] == SAMPLE_PER_SECOND_INITIAL)
                {
                    PowerSummury = i;
                }
            }

            double powerMini = Math.Pow(2.0, (double)(PowerSummury - PowerTimesMini));

            SAMPLE_MINSUMMRY_XRATIO = 1.0 / powerMini;
        }

        public void Initial()
        {
            // initial ratio
            XRATIO = 1;
            YRATIO = 1;

            DEF_RECORD_DISTANCE = DEF_RECORD_DISTANCE_INITIAL * RECORD_RATIO;
            PAINT_PIXEL = PAINT_PIXEL_PERIMG;

            //STATES = RecordState.Idling;
            //PLY_STATES = RecordState.Idling;
            STATES.setState(TestStateValue.Idling);
            SAMPLE_PER_SECOND = SAMPLE_PER_SECOND_INITIAL;
            //  SampleRate = 48000;
        }

        public double GetXRatioReal()
        {
            return XRATIO > 1 ? XRATIO : 1;
            //return XRATIO ;
        }

        public double GetYRatioReal()
        {
            return YRATIO > 1 ? YRATIO : 1;
            //return YRATIO;
        }

        //public double GetWaveHeightMin()
        //{
        //    return WAVE_HEIGHT_MIN;
        //}

        public string SampleToTime(double iSample)
        {
            if (SampleRate <= 0) return "";

            Int64 SampleRemain = (Int64)(iSample);
            Int64 HourSample = (Int64)(SampleRate * 60 * 60);
            Int64 MinuteSample = (Int64)(SampleRate * 60);
            Int64 SecondSample = (Int64)(SampleRate);

            Int64 hour = SampleRemain / HourSample;
            SampleRemain -= hour * HourSample;

            Int64 minute = SampleRemain / MinuteSample;
            SampleRemain -= minute * MinuteSample;

            Int64 second = SampleRemain / SecondSample;
            SampleRemain -= second * SecondSample;

            Int64 msecond = SampleRemain * 1000 / SecondSample;

            return hour.ToString() + ":" + minute.ToString().PadLeft(2, '0') + ":" + second.ToString().PadLeft(2, '0') + "." + msecond.ToString().PadLeft(3, '0');
        }

        public double TimeToSample(Int64 milisecond)
        {
            return (double)(milisecond * (Int64)SampleRate / 1000);
        }

        public double TimeToSample(string strHHMMSSDOTMS)
        {
            int hourEndPos = strHHMMSSDOTMS.IndexOf(':');
            if (hourEndPos <= 0)
                return 0;

            Int64 hour = Convert.ToInt64(strHHMMSSDOTMS.Substring(0, hourEndPos));

            int minuteEndPos = strHHMMSSDOTMS.IndexOf(':', hourEndPos+1);
            if (minuteEndPos <= 0)
                return 0;

            Int64 minute = Convert.ToInt64(strHHMMSSDOTMS.Substring(hourEndPos + 1, minuteEndPos - hourEndPos - 1));

            Int64 second = 0;
            Int64 misecond = 0;
            int secondEndPos = strHHMMSSDOTMS.IndexOf('.', minuteEndPos+1);
            if (secondEndPos > 0)
            {
                second = Convert.ToInt64(strHHMMSSDOTMS.Substring(minuteEndPos + 1, secondEndPos - minuteEndPos - 1));
                misecond = Convert.ToInt64(strHHMMSSDOTMS.Substring(secondEndPos + 1));
            }
            else
            {
                second = Convert.ToInt64(strHHMMSSDOTMS.Substring(minuteEndPos + 1));
            }

            Int64 milisecond = (hour * 3600 + minute * 60 + second)*1000 + misecond;
            return TimeToSample(milisecond);
        }

        public Int64 SampleToMiliTime(double iSample)
        {
            return (Int64)(iSample * 1000 / SampleRate);
        }

        public bool XRatioMulti(double ratio)
        {
            if (ratio != 2) XRatioMulti(ratio / 2);

            if (!isXRatioMul()) return false;

            if (DEF_RECORD_DISTANCE < DEF_RECORD_DISTANCE_INITIAL)
            {
                DEF_RECORD_DISTANCE *= 2;
            }
            else if (XRatioPerSecondAdjustMul())
            {
                /* do noting */
            }
            else
            {
                DEF_RECORD_DISTANCE *= 2;
            }
            XRATIO *= 2;
            return true;
        }

        public bool XRatioPerSecondAdjustMul()
        {
            bool bCheckFlag = false;

            double[] tmp = null;
            switch (SampleRate)
            {
                case 8000:
                    tmp = SAMPLE_RATE_SAMPLEING8K;
                    break;
                case 16000:
                    tmp = SAMPLE_RATE_SAMPLEING16K;
                    break;
                case 24000:
                    tmp = SAMPLE_RATE_SAMPLEING24K;
                    break;
                case 32000:
                    tmp = SAMPLE_RATE_SAMPLEING32K;
                    break;
                case 44100:
                    tmp = SAMPLE_RATE_SAMPLEING441K;
                    break;
                case 48000:
                    tmp = SAMPLE_RATE_SAMPLEING48K;
                    break;
            }

            if (SAMPLE_PER_SECOND < tmp[0])
            {
                SAMPLE_PER_SECOND *= 2;
                return true;
            }

            for (int i = 0; i < (tmp.Length - 1); i++)
            {
                if (tmp[i] == SAMPLE_PER_SECOND)
                {
                    SAMPLE_PER_SECOND = tmp[i + 1];
                    return true;
                }
            }

            return bCheckFlag;
        }

        public bool XRatioDiv(double ratio)
        {
            if (ratio != 2) XRatioDiv(ratio / 2);

            if (!isXRatioDiv()) return false;

            if (DEF_RECORD_DISTANCE > DEF_RECORD_DISTANCE_INITIAL)
            {
                DEF_RECORD_DISTANCE /= 2;
            }
            else if (XRatioPerSecondAdjustDiv())
            {
                /* do noting */
            }
            //else if (DEF_RECORD_DISTANCE > DEF_RECORD_DISTANCE_INITIAL / 32)
            else
            {
                DEF_RECORD_DISTANCE /= 2;
            }
            XRATIO /= 2;
            return true;
        }

        public bool XRatioPerSecondAdjustDiv()
        {
            bool bCheckFlag = false;
            double[] tmp = null;
            switch (SampleRate)
            {
                case 8000:
                    tmp = SAMPLE_RATE_SAMPLEING8K;
                    break;
                case 16000:
                    tmp = SAMPLE_RATE_SAMPLEING16K;
                    break;
                case 24000:
                    tmp = SAMPLE_RATE_SAMPLEING24K;
                    break;
                case 32000:
                    tmp = SAMPLE_RATE_SAMPLEING32K;
                    break;
                case 44100:
                    tmp = SAMPLE_RATE_SAMPLEING441K;
                    break;
                case 48000:
                    tmp = SAMPLE_RATE_SAMPLEING48K;
                    break;
            }

            for (int i = 1; i < tmp.Length; i++)
            {
                if (tmp[i] == SAMPLE_PER_SECOND)
                {
                    SAMPLE_PER_SECOND = tmp[i - 1];
                    return true;
                }
            }
            SAMPLE_PER_SECOND /= 2;
            return true;
        }

        public bool isXRatioMul()
        {
            if (DEF_RECORD_DISTANCE >= DEF_RECORD_DISTANCE_MAX) return false;
            return true;
        }

        public bool isXRatioDiv()
        {
            //if (DEF_RECORD_DISTANCE <= DEF_RECORD_DISTANCE_INITIAL / 32) return false;
            return true;
        }

        public bool YRatioMulti(double ratio)
        {
            if (ratio != 2) return YRatioMulti(ratio / 2);
            if (isYRatioMul(2.0))
            {
                YRATIO *= 2;
            }
            return true;
        }

        public bool YRatioDiv(double ratio)
        {
            if (ratio != 2) return YRatioDiv(ratio / 2);
            if (isYRatioDiv(2.0))
            {
                YRATIO /= 2;
            }
            return true;
        }

        public bool isYRatioMul(double ratio)
        {
            if (GetVisulYRation() >= 4096)
                return false;
            return true;
        }

        public bool isYRatioDiv(double ratio)
        {
            if (YRATIO <= 1.0 / 64)
                return false;
            return true;
        }

        public double GetVisulYRation()
        {
            int ratioInner = 1;
            //int ratioInner = ratio;
            if (YRATIO <= 1)
            {
                return YRATIO;
            }
            else
            {
                for (int index = WAVE_HEIGHT_STACK.Count - 1; index >=0 ; index--)
                {
                    if (WAVE_HEIGHT_MAX_THRES == WAVE_HEIGHT_STACK[index])
                    {
                        ratioInner *= 2;
                    }
                    else
                    {
                        break;
                    }
                }

                if (ratioInner == 1)
                {
                    return 1;
                }
                else
                {
                    return ratioInner /= 2;
                }
            }
        }
    }
}
