using InventoryManangement.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

            BaseMember baseMember = new BaseMember();

            //Data Table login by Sp

            DataTable dt = baseMember.validateasTableBySp(username, password);
            List<BaseMember> baseMembers = new List<BaseMember>();
            foreach (DataRow dr in dt.Rows)
            {
                BaseMember bm = new BaseMember();
                bm.Id = Convert.ToInt32(dr["Id"]);
                bm.Username = dr["Username"].ToString();
                bm.Password = dr["PasswordHash"].ToString();
                baseMembers.Add(bm);
            }
            if (baseMembers.Count > 0)
            {
                Session["username"] = username;
            }
            else
            {
                ViewBag.error = "Invalid username or password";
            }








            //List Login

            //List<BaseMember> baseMembers = baseMember.validateasList(username, password);
            //foreach (BaseMember member in baseMembers)
            //{
            //    if (member.Username == username && member.Password == password)
            //    {
            //        //return RedirectToAction("Index", "Home", new { username = username });
            //        Session["username"] = username;
            //        break;
            //    }
            //    else
            //    {
            //        ViewBag.error = "Invalid username or password";

            //    }

            //}
            return View();
        }
    }
}