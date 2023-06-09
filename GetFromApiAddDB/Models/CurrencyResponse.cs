using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFromApiAddDB.Models
{
    public class CurrencyResponse
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("symbol")]
        public string? Symbol { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}