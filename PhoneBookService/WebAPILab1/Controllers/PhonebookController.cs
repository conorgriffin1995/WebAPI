using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebAPILab1.Models;

namespace WebAPILab1.Controllers
{
    [RoutePrefix("phonebook")]
    public class PhonebookController : ApiController
    {
        /*
         * GET /phonebook/all               get phonebook information for all people in phonebook
         * GET /phonebook/name/Conor        get phonebook infornation for name
         * GET /phonebook/number/0851470045            get phonebook information for number
         */ 
        private static IList<Phonebook> phonebook = new List<Phonebook>()
        {
            new Phonebook { Name = "Conor", Number = 0851470045, Address = "13 Long Road" },
            new Phonebook { Name = "John", Number = 0874579921, Address = "24 Upper Way" },
            new Phonebook { Name = "Gary", Number = 0831245789, Address = "18 Tamarisk Avenue" },
            new Phonebook { Name = "Tom", Number = 0867744112, Address = "33 Down The Road" }
        };

        // GET Phonebook/all
        [Route("all")]
        [HttpGet]
        public IHttpActionResult RetrieveAllPhonebookInformation()
        {
            return Ok(phonebook.OrderBy(p => p.Name).ToList());
        }

        // GET phonebook/name
        [Route("name/{personName:alpha}")]
        public IHttpActionResult GetInformationForName(string personName)
        {
            Phonebook person = phonebook.FirstOrDefault(p => p.Name.ToUpper() == personName.ToUpper());
            if(person == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(person);
            }
        }

        // GET phonebook/number
        [Route("number/{personNumber:int}")]
        public IHttpActionResult GetInformationForNumber(int personNumber)
        {
            Phonebook person = phonebook.FirstOrDefault(p => p.Number == personNumber);
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(person);
            }
        }

 
    }
}
