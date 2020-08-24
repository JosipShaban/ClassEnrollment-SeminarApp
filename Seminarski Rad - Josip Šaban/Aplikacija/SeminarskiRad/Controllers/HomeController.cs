using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeminarskiRad.Controllers
{
    //Jedini razlog zašto je ovo tu je zato što nisam znao kako da se nakon prijave stranica vrati na Predbilježbu.
    //Tražio sam, ali nisam našao.
    public class Homecontroller : Controller
    {
        public ActionResult Index()
        {
           return RedirectToAction("Predbiljezba", "Pocetak");
        }
     
    }
}