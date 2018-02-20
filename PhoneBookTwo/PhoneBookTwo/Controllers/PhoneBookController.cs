using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhoneBookTwo.Models;
namespace PhoneBookTwo.Controllers
{
    [RoutePrefix("phonebook")]
    public class PhoneBookController : ApiController
    {
        private static List<PhoneBook> pbook = new List<PhoneBook>()
        {
            new PhoneBook { Number = "087451236", Name = "Tom Hardy", Address = "75 Forest Avenue"},
            new PhoneBook { Number = "0851235566", Name = "Lee Mack", Address = "33 Down the road"},
            new PhoneBook { Number = "0871256699", Name = "Graham Lalor", Address = "13 Tamarisk Walk"}
        };

        // GET: phonebook/all
        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllPhoneBook()
        {
            return Ok(pbook.OrderBy(p => p.Name).ToList());
        }

        // GET: phonebook/number/
        [Route("number/{number}")]
        [HttpGet]
        public IHttpActionResult GetNumber(String number)
        {
            // LINQ query, find matching ticker (case-insensitive) or default value (null) if none matching
            PhoneBook listing = pbook.SingleOrDefault(l => l.Number.ToUpper() == number.ToUpper());
            if (listing == null)
            {
                return NotFound();          // 404
            }
            return Ok(listing);
        }

        // POST phonebook/all
        [Route("all")]
        [HttpPost]
        public IHttpActionResult PostAddPerson(PhoneBook person)
        {
            if (ModelState.IsValid)
            {
                //var error1 = db.PhoneBooks.Where(p => p.Name.ToUpper() == person.Name.ToUpper());
                var error = pbook.SingleOrDefault(p => p.Number == person.Number);
                if (error == null)
                {
                    pbook.Add(person);
                    
                    // create http response with created status code and listing serialised as content and Location header set to URI for new resource
                    //string uri = Url.Link("DefaultApi", new { id = person.Number });
                    return Ok(person);
                }
                else
                {
                    return BadRequest("Number or Name already exists");
                }
            }
            else
            {
                return BadRequest(ModelState);              // 400
            }
        }

        // update an entry (replace a number with new entry)
        // PUT phonebook/number/{string}
        [Route("number/{number}")]
        [HttpPut]
        public IHttpActionResult PutUpdateEntry(string number, PhoneBook person)
        {
            if (ModelState.IsValid)
            {
                var existingData = pbook.SingleOrDefault(p => p.Number == number);
                if(existingData == null)
                {
                    return NotFound();
                }
                else
                {
                    var entry = pbook.SingleOrDefault(p => p.Name.ToUpper() == person.Name.ToUpper());
                    if(entry != null)
                    {
                        entry.Number = person.Number;
                        return Ok(entry);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE phonebook/{string}
        [Route("number/{number}")]
        [HttpDelete]
        public IHttpActionResult DeleteEntry(string number)
        {
            var entry = pbook.SingleOrDefault(p => p.Number == number);
            if (entry != null)
            {
                pbook.Remove(entry);
                return Ok(entry);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
