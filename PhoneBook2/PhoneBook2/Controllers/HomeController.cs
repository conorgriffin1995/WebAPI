using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneBook2.Models;

namespace PhoneBook2.Controllers
{
    public class HomeController : Controller
    {
        private PhoneBookContext db = new PhoneBookContext();
        private PhoneBook book = new PhoneBook();
        private static List<PhoneBook> list = new List<PhoneBook>()
        {
            new PhoneBook { Number = 0851715566, Name = "Conor Griffin", Address = "13 Tamarisk Walk" },
            new PhoneBook { Number = 0851470074, Name = "Tom O'Dowd", Address = "15 Down the road" },
            new PhoneBook {Number = 0877927436, Name = "Declan Griffin", Address = "33 Tamarisk Avenue" }

        };

        public void AddPhoneBook()
        {
            list.ForEach(p => db.PhoneBooks.Add(p));
            db.SaveChanges();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //AddPhoneBook();
            return View();
        }
    }
}
