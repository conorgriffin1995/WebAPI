using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SMSService.Models;

namespace SMSService.Controllers
{
    public class HomeController : Controller
    {
        private SMSContext db = new SMSContext();
        TextMessage service = new TextMessage();
        private static List<TextMessage> list = new List<TextMessage>()
        {
           new TextMessage {PhoneNumberSender = "0831239988", Content="Hello, how r u?", PhoneNumberReceiver="0895541236"},
           new TextMessage {PhoneNumberSender = "0874455889", Content="Hey there, what time are we meeting at?", PhoneNumberReceiver="0851245698"},
           new TextMessage {PhoneNumberSender = "0855568947", Content="Are you out tonight?", PhoneNumberReceiver="0869874422"}
        };
        public void AddMessages()
        {
            list.ForEach(m => db.Messages.Add(m));
            db.SaveChanges();
        }

        // GET: Home
        public ActionResult Index()
        {
            //AddMessages();
            return View();
        }
    }
}