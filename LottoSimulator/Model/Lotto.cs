using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSimulator.Model
{
    public class Lotto
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "WinningRow")] 
        public string WinningRow { get; set; }
        [JsonProperty(PropertyName = "Cost")] 
        public long Cost { get; set; }
        [JsonProperty(PropertyName = "playerName")] 
        public string playerName { get; set; }
        [JsonProperty(PropertyName = "longitude")]
        public double longitude {get;set;}
        [JsonProperty(PropertyName = "latitude")]
        public double latitude { get; set; }
    }
}
