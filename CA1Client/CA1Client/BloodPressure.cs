using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA1Client.Models
{
    public class Patient
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<BloodPressure> Pressure { get; set; }

    }

    public class BloodPressure
    {
        public int Systolic { get; set; }

        public int Diastolic { get; set; }

        public Category Category { get; set; }
    }

    public enum Category
    {
        LowBloodPressure,
        NormalBloodPressure,
        PreHypertension,
        Stage1Hypertension,
        Stage2Hypertension
    }

}
