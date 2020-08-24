using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeminarskiRad.Controllers.MojiKontroleri
{
    //Ništa posebno, glavno da nema yellow screen - nadam se da ga neće biti :O

    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Default()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult InternalError()
        {
            return View();
        }
    }
}