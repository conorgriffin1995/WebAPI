// Conor Griffin
// X00111602
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CA1API.Models;

namespace CA1API.Controllers
{
    [RoutePrefix("pressure")]
    public class BloodPressureController : ApiController
    {
        private static List<Patient> patientList = new List<Patient>()
        {
            new Patient { ID = 1, Name = "John Doe", Pressure = new List<BloodPressure>()
            {
                new BloodPressure { Systolic = 100, Diastolic = 80 }, 
                new BloodPressure { Systolic = 110, Diastolic = 90 },
                new BloodPressure { Systolic = 90, Diastolic = 70 }
            } },
            new Patient { ID = 2, Name = "Sarah Jane", Pressure = new List<BloodPressure>()
            {
                new BloodPressure { Systolic = 110, Diastolic = 60},
                new BloodPressure { Systolic = 120, Diastolic = 90},
                new BloodPressure { Systolic = 105, Diastolic = 80}
            } },
            new Patient { ID = 3, Name = "Mike Dunne", Pressure = new List<BloodPressure>()
            {
                new BloodPressure { Systolic = 150, Diastolic = 105},
                new BloodPressure {Systolic = 130, Diastolic = 100},
                new BloodPressure { Systolic = 120, Diastolic = 90 }
            } }
        };
        
        // GET: /all
        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(patientList.ToList()); // all
        }

        [Route("patient/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetByID(int id)
        {
            var patient = patientList.Where(p => p.ID == id);
            if(patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [Route("patient/{systolic:int},{diastolic:int}")]
        [HttpGet]
        public IHttpActionResult GetBloodPressureCategory(int systolic, int diastolic)
        {
            try
            {
                BloodPressure b = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
                return Ok(b.CalcCategory());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("pressure/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetAverageBloodPressure(int id)
        {
            try
            {
                Patient patient = patientList.Find(p => p.ID == id);

                return Ok(patient.GetAverage(patient));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
