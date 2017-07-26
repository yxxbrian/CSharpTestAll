using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioBridgeTest
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WaveChannelModel
    {
        public WaveChannelModel()
        {
        }
        /*
        public WaveChannelModel(WaveChannelViewModel waveChannelViewModel)
        {
            Index = waveChannelViewModel.ChannelIndex;
            Name = waveChannelViewModel.ChannelName;
        }*/

        [JsonProperty]
        public int Index { get; set; }

        [JsonProperty]
        public string Name { get; set; }
    }
}
