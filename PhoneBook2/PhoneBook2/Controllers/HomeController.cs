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
            new PhoneBook {Number = 085265465, Name = "Tom", Address = "14 Down the road" },
            new PhoneBook {Number = 0877845621, Name = "James", Address = "33 Hollywood Avenue" }
        };

        public void AddPhoneBook()
        {
            list.ForEach(p => db.PhoneBooks.Add(p));
            db.SaveChanges();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            // AddPhoneBook();
            return View();
        }
    }
}
