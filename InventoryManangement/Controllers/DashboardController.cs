using InventoryManangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManangement.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            BaseEquipment baseEquipment = new BaseEquipment();
            List<BaseEquipment> lstEquipment = baseEquipment.EquipementList();
            Customer ct = new Customer();
            List<Customer> customers = ct.CustomerList();


            //If business logic requires to display only laptops

            //List<BaseEquipment> lstEquipment_laptop = new List<BaseEquipment>();
            //foreach (BaseEquipment obj in lstEquipment)
            //{
            //    if (obj.EquipmentName.Contains("Laptop"))
            //    {
            //        lstEquipment_laptop.Add(obj);
            //    }
            //}
            //ViewBag.lstEquipment = lstEquipment;



            ViewBag.lstEquipment = lstEquipment;
            ViewBag.customers = customers;
            return View();
        }
        [HttpPost]

        public ActionResult Index(string btnSubmit,string EquipeId,int AssignId, string btnDeleteAssign, FormCollection formCollection)
        {
            BaseEquipment baseEquipment = new BaseEquipment();
            int ReturnStatus = 0;
            if (btnSubmit == "Save")
            {
                baseEquipment.EquipmentName = formCollection["txtEquipName"].ToString();
                baseEquipment.Quantity = Convert.ToInt32(formCollection["txtQuantity"].ToString());
                baseEquipment.EntryDate = Convert.ToDateTime(formCollection["txtEntryDate"].ToString());
                baseEquipment.ReceiveDate = Convert.ToDateTime(formCollection["txtRcvDate"].ToString());
                ReturnStatus = baseEquipment.SaveEquipment();
            }

            if (btnSubmit == "Update")
            {
                baseEquipment.EquipmentId = Convert.ToInt32(formCollection["EquipmentID"].ToString());
                baseEquipment.EquipmentName = formCollection["txtEquipName"].ToString();
                baseEquipment.Quantity = Convert.ToInt32(formCollection["txtQuantity"].ToString());
                baseEquipment.EntryDate = Convert.ToDateTime(formCollection["txtEntryDate"].ToString());
                baseEquipment.ReceiveDate = Convert.ToDateTime(formCollection["txtRcvDate"].ToString());
                ReturnStatus = baseEquipment.SaveEquipment();
            }

            if (btnSubmit == "Delete")
            {
                baseEquipment.DeleteRow(int.Parse(EquipeId) );
            }

            if (btnDeleteAssign == "Delete")
            {
                BaseEquipment.DeleteEquipmentAssignment(AssignId);
            }
            List<BaseEquipment> lstEquipment = baseEquipment.EquipementList();
            Customer ct = new Customer();
            List<Customer> customers = ct.CustomerList();
            ViewBag.customers = customers;


            ViewBag.lstEquipment = lstEquipment;
            if (ReturnStatus > 0)
                ViewBag.OutMessage = "Operation Completed Successfully";
            return View();

        }


        public ActionResult NewEquipmentAssignment()
        {
            BaseEquipment baseEquipment = new BaseEquipment();
            List<BaseEquipment> lstEquipment = baseEquipment.EquipementList();
            Customer ct = new Customer();
            List<Customer> customers = ct.CustomerList();

            //int AssignmentId = Convert.ToInt32(Request.QueryString["id"].ToString());

            ViewBag.lstEquipment = lstEquipment;
            ViewBag.customers = customers;
            return View();
        }
        [HttpPost]
        public ActionResult NewEquipmentAssignment(FormCollection frmCol)
        {
            int returnval = BaseEquipment.SaveEquipmentAssignment(frmCol);
            if (returnval > 0)
            {
                ViewBag.OutMessage = "Operation Completed Successfully";
                return Redirect(Url.Action("Index", "Dashboard"));
            }
            ViewBag.OutMessage = "Operation failed";
            return View();
        }


        //public ActionResult ModelTest()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult ModelTest(BaseEquipment model)
        //{
        //    BaseEquipment sdsmodel = new BaseEquipment();
        //    ModelState.Clear();
        //    return View(sdsmodel);
        //}
    }
}