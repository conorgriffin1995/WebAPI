using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using PhoneBook2.Models;

namespace PhoneBook2.Controllers
{
    public class PhoneBookController : ApiController
    {
        private PhoneBookContext db = new PhoneBookContext();

        // GET api/PhoneBook
        public IHttpActionResult GetAllPhoneBook()
        {
            return Ok(db.PhoneBooks.OrderBy(p => p.ID).ToList());
        }
        // GET api/PhoneBook/5
        public IHttpActionResult GetMessageDetails(int id)
        {
            PhoneBook pbook = db.PhoneBooks.SingleOrDefault(m => m.ID == id);
            if (pbook == null)
            {
                return NotFound();
            }
            return Ok(pbook);
        }
        // POST api/PhoneBook
        public IHttpActionResult PostAddperson(PhoneBook person)
        {
            if (ModelState.IsValid)
            {
                var error1 = db.PhoneBooks.Where(p => p.Name.ToUpper() == person.Name.ToUpper());
                var error2 = db.PhoneBooks.Where(p => p.Number == person.Number);
                if(error1 != null || error2 != null)
                {
                    return BadRequest("Number or Name already exists");
                }
                else
                {
                    db.PhoneBooks.Add(person);
                    db.SaveChanges();

                    // create http response with created status code and listing serialised as content and Location header set to URI for new resource
                    string uri = Url.Link("DefaultApi", new { id = person.ID });
                    return Created(uri, person);

                }   
            }
            else
            {
                return BadRequest(ModelState);              // 400
            }
        }

        // update an entry (replace a number with new entry)
        // PUT /api/PhoneBook
        public IHttpActionResult PutUpdateEntry(int number, PhoneBook person)
        {
            if (ModelState.IsValid)
            {
                if(number == person.Number)
                {
                    var entry = db.PhoneBooks.SingleOrDefault(p => p.Number == person.Number);
                    if(entry == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        entry.Number = person.Number;
                        db.SaveChanges();
                        return Ok(entry);
                    }
                }
                else
                {
                    return BadRequest("Invalid Number.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/PhoneBook/085265465
        public IHttpActionResult DeleteEntry(int number)
        {
            var entry = db.PhoneBooks.SingleOrDefault(p => p.Number == number);
            if(entry != null)
            {
                db.PhoneBooks.Remove(entry);
                db.SaveChanges();
                return Ok(entry);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
