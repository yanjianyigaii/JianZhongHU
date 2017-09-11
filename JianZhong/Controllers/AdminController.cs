using JianZhong.Business;
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
            var formdataList = AdminHelper.GetUploadData();
            return View(formdataList);
        }
    }
}