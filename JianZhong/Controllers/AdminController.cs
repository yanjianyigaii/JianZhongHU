using JianZhong.Business;
using JianZhong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JianZhong.Controllers
{
    public class AdminController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetImages()
        {
            var formdataList = AdminHelper.GetUploadData();
            return Json(formdataList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Upload(ImgWallForm imageData, HttpPostedFileBase image)
        {
            bool result = true;
            AdminHelper.PostUploadData(imageData, image);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}