using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManangement.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Console.WriteLine("username: " + username);
            Console.WriteLine("password: " + password);        
            ViewBag.username = username;
            return View();
        }
    }
}