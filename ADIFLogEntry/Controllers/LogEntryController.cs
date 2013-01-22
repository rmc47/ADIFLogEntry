using ADIFLogEntry.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ADIFLogEntry.Controllers
{
    public class LogEntryController : Controller
    {
        private JavaScriptSerializer m_Serializer = new JavaScriptSerializer();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQso(QsoModel qso)
        {
            return Json(qso);
        }
    }
}
