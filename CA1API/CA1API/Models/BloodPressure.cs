// Conor Griffin
// X00111602
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CA1API.Models
{
    public class Patient
    {
        public int ID { get; set; }
       
        public string Name { get; set; }
         
        public List<BloodPressure> Pressure { get; set; }

        public int GetAverage(Patient p)
        {
            BloodPressure b = new BloodPressure();
            int systolic = 0, diastolic = 0, pressure = 0;
            for (int i = 0; i < p.Pressure.Count(); i++)
            {
                systolic += b.Systolic;
                diastolic += b.Diastolic;
            }
            pressure = systolic / diastolic;
            return pressure;
        }

    }

    public class BloodPressure
    {
        [Range(1, 200, ErrorMessage = "Invalid, outside range")]
        public int Systolic { get; set; }

        [Range(1, 200, ErrorMessage = "Invalid, outside range")]
        public int Diastolic { get; set; }

        public Category Category { get; set; }

        public string CalcCategory()
        {
            string category;
            if(Systolic < 90 || Diastolic < 60)
            {
                Category = Category.LowBloodPressure;
                category = Category.ToString();
            }
            else if(Systolic < 120 && Diastolic < 80)
            {
                Category = Category.NormalBloodPressure;
                category = Category.ToString();
            }
            else if(Systolic < 140 || Diastolic < 90)
            {
                Category = Category.PreHypertension;
                category = Category.ToString();
            }
            else if(Systolic < 160 || Diastolic < 100)
            {
                Category = Category.Stage1Hypertension;
                category = Category.ToString();
            }
            else if(Systolic >= 160 || Diastolic >= 100)
            {
                Category = Category.Stage2Hypertension;
                category = Category.ToString();
            }
            else
            {
                return category = "Invalid";
            }
            return category;
        }

    }

    public enum Category
    {
        [Display(Name = "Low Blood Pressure")]
        LowBloodPressure,
        [Display(Name = "Normal Blood Pressure")]
        NormalBloodPressure,
        [Display(Name = "Pre-Hypertension")]
        PreHypertension,
        [Display(Name = "Stage 1 Hypertension")]
        Stage1Hypertension,
        [Display(Name = "Stage 2 Hypertention")]
        Stage2Hypertension
    }
}