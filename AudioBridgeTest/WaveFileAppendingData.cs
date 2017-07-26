using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioBridgeTest
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WaveFileAppendingData
    {
        public WaveFileAppendingData()
        {
        }
        public WaveFileAppendingData(WaveFileViewModel waveFileViewModel)
        {
            Channels = new List<WaveChannelModel>();
            foreach (WaveChannelViewModel chVM in waveFileViewModel.WaveFileChannels)
            {
                WaveChannelModel chM = new WaveChannelModel(chVM);
                Channels.Add(chM);
            }
        }

        private List<WaveChannelModel> channels;
        [JsonProperty]
        public List<WaveChannelModel> Channels
        {
            get { return channels; }
            set { channels = value; }
        }

    }
}
