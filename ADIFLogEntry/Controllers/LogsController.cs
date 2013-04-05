using ADIFLogEntry.Engine;
using ADIFLogEntry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADIFLogEntry.Controllers
{
    public class LogsController : Controller
    {
        //
        // GET: /Logs/

        public ActionResult Index()
        {
            LogsModel model = new DataStore().LoadLogs(1);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateNew(LogModel log)
        {
            log.UserID = 1;
            new DataStore().CreateLog(log);
            return Redirect("~/logs");
        }
    }
}
