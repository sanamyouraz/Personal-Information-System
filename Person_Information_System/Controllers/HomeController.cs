using Person_Information_System.DB;
using Person_Information_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Person_Information_System.Controllers
{
    public class HomeController : Controller
    {
        DbConn conn = new DbConn();
        // GET: Home

        [HttpPost]
        public ActionResult GetJsonData(PersonInfo data)
        {
            string message;
            conn.SaveData(data, out message);
            return Json(new JsonResult { Data = message });
            //return Json(data);
        }
        [HttpPost]
        public ActionResult GetAllDetails()
        {
          

            List <ShowDetails>ShowList = conn.ShowData();
            return Json(new JsonResult { Data = ShowList });
            //return Json(data);
        }

        [HttpPost]
        public ActionResult DeleteDetails(int? Id)
        {
            string message;
            conn.DeleteData(Id, out message);
            return Json(new JsonResult { Data = message });
            //return Json(data);
        }

        [HttpPost]
        public ActionResult EditDetails(int? Id)
        {


            PersonInfo PersonalList = conn.EditData(Id);

            return Json(new JsonResult { Data = PersonalList, JsonRequestBehavior = JsonRequestBehavior.DenyGet });
            //return Json(data);
        }
        public ActionResult UpdateDatails(PersonInfo data)
        {


            string message;
            conn.UpdateData(data, out message);

            return Json(new JsonResult { Data = message });
           
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
     
    }
}