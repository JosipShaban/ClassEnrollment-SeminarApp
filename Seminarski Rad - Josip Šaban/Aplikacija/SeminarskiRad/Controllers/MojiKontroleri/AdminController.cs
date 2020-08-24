using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SeminarskiRad.Models;
using SeminarskiRad.Models.MojiModeli;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SeminarskiRad.Controllers.MojiKontroleri
{
    //Ovaj kontroler je više "Gratis", ali nije mi imalo smisla ovo raditi u SQL-u

    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        
       ApplicationDbContext db = new ApplicationDbContext();

        //Stvaram UserManagera da mogu davati ovlasi - Ovo mi je trebalo par dana da shvatim 

        UserManager<ApplicationUser> userManager;
       public AdminController()
        {
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        //Stvaranje uloga. 
        //Nisam stavio u layout, jer nema smisla ako nisi programer. 
        //Htio naučiti kako se to radi. Na to sam potrošio nekih 7 sati sigurno.

        public ActionResult CreateRole()
        {
            return View(new IdentityRole());
        }
        [HttpPost]
        public ActionResult CreateRole(IdentityRole roleName)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(roleName);
                db.SaveChanges();
                ViewBag.CreateRole = "Uspješno stvorena uloga";
                return View();
            }
            else
            {
                ViewBag.CreateRole = "Nešto je pošlo u krivu";
                return View();

            }
        }

        public ActionResult AssignRole()
        {
        //Popis Uloga

            List<IdentityRole> ListOfRoles = new List<IdentityRole>();
            List<string> ListOfRoleNames = new List<string>();

            //Ovo sam samo radio da dobim imena uloga
            ListOfRoles = db.Roles.ToList();
            foreach (var item in ListOfRoles)
            {
                ListOfRoleNames.Add(item.Name);
            }
                  
            ViewBag.Roles = new SelectList(ListOfRoleNames);

        //Popis Korisničkih imena - ista procedura

            List<ApplicationUser> ListOfUsers = new List<ApplicationUser>();
            List<string> ListOfUserNames = new List<string>();

            ListOfUsers = db.Users.ToList();
            foreach (var item in ListOfUsers)
            {
                ListOfUserNames.Add(item.UserName);
            }

            ViewBag.UserNames = new SelectList(ListOfUserNames);

            return View();
        }

        [HttpPost]
        public ActionResult AssignRole(string Role, string userName)
        {
            try
            {
                var UserId = db.Users.Where(u => u.UserName == userName).FirstOrDefault().Id;   

               //Dajem Useru Ulogu - Za ovo jednu liniju mi je trebalo nekih 7 sati

                userManager.AddToRole(UserId, Role);      

                ViewBag.AssignRole = "Uspjeh!";
                return RedirectToAction("Predbiljezba", "Pocetak");
            }
            catch (Exception e)
            {
                ViewBag.Error = "Dogodila se greška: " + e.Message;
                return View("Default","Error");

            }
                                       
        }      
        public ActionResult PromjeniPredavaca(int id)
        {

            List<Zaposlenik> PopisZaposlenika = db.Zaposlenici.ToList();
            TempData["IdSeminara"] = id;
            return View(PopisZaposlenika);
        }


        //Ovo sam stavio u AdminController jer mi nije imalo smisla da zaposlenici imaju tu ovlast
        public ActionResult PromjenjenPredavac(int id)
        {
            int IdSeminara = (int)TempData["IdSeminara"];

            Seminar seminar = new Seminar();

            seminar = db.Seminari.Where(x => x.IdSeminar == IdSeminara).FirstOrDefault();
            seminar.Predavac = id;

            db.Entry(seminar).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Seminari","Pocetak");
        }
    }
}