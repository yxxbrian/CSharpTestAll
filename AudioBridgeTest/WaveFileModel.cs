//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioBridgeTest
{
    //[JsonObject(MemberSerialization.OptIn)]
    public class WaveFileModel
    {
        //[JsonProperty]
        public List<WaveChannelModel> ChannelInfo { get; set; }
    }
}
