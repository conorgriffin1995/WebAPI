using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherAPI.Models
{
    public class WeatherInformation
    {
        [Required(ErrorMessage = "Invalid City")]
        public string City { get; set; }

        [Range(-50, 50, ErrorMessage ="Invalid Temperature")]
        [Display(Name = "Temerature C°")]
        public double Temperature { get; set; }

        [Range(0,200, ErrorMessage = "Invalid Wind Speed")]
        [Display(Name = "Wind Speed km/h")]
        public int WindSpeed { get; set; }

        [Required(ErrorMessage = "Invalid Conditions")]
        public string Condition { get; set; }

        public bool Warning { get; set; }
    }
}