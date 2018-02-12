using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SMSService.Models;

namespace SMSService.Controllers
{
    public class SMSServiceController : ApiController
    {
        /*
         * GET api/SMSService              get all messages in database
         */
        private SMSContext db = new SMSContext();

        // GET api/SMSService
        public IHttpActionResult GetAllMessages()
        {
            if (db.Messages.Count() == 0)
            {
                return NotFound();
            }

            else
            {
                return Ok(db.Messages.OrderBy(s => s.ID).ToList());       // 200 OK, listings serialized in response body 
            }
        }

        // Get api/SMSService/1
        public IHttpActionResult GetMessageDetails(int id)
        {
            TextMessage msg = db.Messages.SingleOrDefault(m => m.ID == id);
            if(msg == null)
            {
                return NotFound();
            }
            return Ok(msg);            
        }


        // POST api/SMSService
        public IHttpActionResult PostSendMessage(TextMessage message)
        {
            if (ModelState.IsValid)
            {
                // check for duplicate
                // linq get message
                
                var msgError = db.Messages.SingleOrDefault(m => m.ID == message.ID);

                if(msgError == null)
                {
                    db.Messages.Add(message);
                    db.SaveChanges();

                    // create http response with created status code and listing serialised as content and Location header set to URI for new resource
                    string uri = Url.Link("DefaultApi", new { id = message.ID });
                    return Created(uri, message);
                }
                else
                {
                    return BadRequest("Number error!!");    // 400, number error
                }
            }
            else
            {
                return BadRequest(ModelState);      // 400
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
