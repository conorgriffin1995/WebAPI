using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherClient.Models
{
    class WeatherInformation
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public int WindSpeed { get; set; }
        public string Condition { get; set; }
        public bool Warning { get; set; }
    }
}
