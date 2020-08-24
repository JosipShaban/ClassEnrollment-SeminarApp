using Microsoft.Ajax.Utilities;
using SeminarskiRad.Models;
using SeminarskiRad.Models.MojiModeli;
using SeminarskiRad.Models.MojiModeli.ViewModeli;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Odbc;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace SeminarskiRad.Controllers.MojiKontroleri
{
    [Authorize]
    public class PocetakController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region POPIS SEMINARA I UNOS PODATAKA -- ANON

        // Home
        [AllowAnonymous]
        public ActionResult Predbiljezba()
        {
            //Popis Seminara koji nisu popunjeni

            List<Seminar> PopisAktivnihSeminara = new List<Seminar>();
            PopisAktivnihSeminara = db.Seminari.Where(u => u.Popunjen == false).ToList();


            //Popis Predavača

            List<Zaposlenik> Predavaci = new List<Zaposlenik>();
            Predavaci = db.Zaposlenici.ToList();
            ViewBag.ImenaPredavaca = Predavaci;

            return View(PopisAktivnihSeminara);
        }
        [HttpPost]
        public ActionResult Predbiljezba(string Pretraga)
        {
                              
            //Popis Seminara koji nisu popunjeni

            List<Seminar> PopisAktivnihSeminara = new List<Seminar>();
            PopisAktivnihSeminara = db.Seminari.Where(u => u.Popunjen == false).ToList();


            //Seminari se pretražuju po datumu. Ako je datum prazan prikaže ih sve

            var DohvaceniSeminari = (from seminar in db.Seminari
                                     select seminar);
            
            if (!Pretraga.IsNullOrWhiteSpace())
            {
                if(Pretraga.IsDateTime())
                {
                  DateTime PretrazenDatum = DateTime.Parse(Pretraga);
                    DohvaceniSeminari = DohvaceniSeminari.Where(m => m.Datum.Year == PretrazenDatum.Year && m.Datum.Month == PretrazenDatum.Month && m.Datum.Day == PretrazenDatum.Day);
                }
                else
                {
                    DohvaceniSeminari = DohvaceniSeminari.Where(m => m.Naziv.Contains(Pretraga) || m.Opis.Contains(Pretraga) || m.Zaposlenik.Ime.Contains(Pretraga));
                }
            }
          
            

            //Popis Predavača

            List<Zaposlenik> Predavaci = new List<Zaposlenik>();
            Predavaci = db.Zaposlenici.ToList();
            ViewBag.ImenaPredavaca = Predavaci;

            return View(DohvaceniSeminari);
        }


        //Upis podataka Anona
        [AllowAnonymous]
        public ActionResult UnosOsobnihPodataka(int id)
        {
            //Naslov View-a

            ViewBag.ImeSeminara = db.Seminari.Where(x => x.IdSeminar == id).FirstOrDefault().Naziv;
           
            TempData["IdSeminara"] = id;

            return View(new Predbiljezba());

        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UnosOsobnihPodataka(Predbiljezba predbiljezba)
        {
            predbiljezba.Datum = DateTime.Now;
            predbiljezba.IdSeminar = (int)TempData["IdSeminara"];

            if (ModelState.IsValid)
            {
                db.Predbiljezbe.Add(predbiljezba);
                db.SaveChanges();
            }
            return RedirectToAction("Predbiljezba");
        }
#endregion

#region CRUD PREDBILJEŽBE 

        
        public ActionResult Predbiljezbe()
        {
            List<Predbiljezba> PopisNeobradjenihPredbiljezbi = new List<Predbiljezba>();

            PopisNeobradjenihPredbiljezbi = db.Predbiljezbe.Where(p => p.Status == 0).ToList();
            
            return View(PopisNeobradjenihPredbiljezbi);
        }

        [HttpPost]
        public ActionResult Predbiljezbe(string Pretraga, int Filter)
        {
            List<Seminar> PopisSeminara = new List<Seminar>();
            PopisSeminara = db.Seminari.ToList();

            // Filtriram seminare po statusu

            List<Predbiljezba> PopisPredbiljezbiPoFiltru = new List<Predbiljezba>();
            PopisPredbiljezbiPoFiltru = db.Predbiljezbe.Where(p => p.Status == Filter).ToList();


            var DohvacenePretrage = (from predbiljezba in db.Predbiljezbe select predbiljezba);

            if (!Pretraga.IsNullOrWhiteSpace())
            {
                if (Pretraga.IsDateTime())
                {
                    DateTime PretrazenDatum = DateTime.Parse(Pretraga);
                    DohvacenePretrage = DohvacenePretrage.Where(m => m.Datum.Year == PretrazenDatum.Year && m.Datum.Month == PretrazenDatum.Month && m.Datum.Day == PretrazenDatum.Day);
                }
                else
                {
                    DohvacenePretrage = DohvacenePretrage.Where(m => m.Adresa.Contains(Pretraga)
                    || m.Email.Contains(Pretraga) || m.Ime.Contains(Pretraga) || m.Prezime.Contains(Pretraga)
                    || m.Telefon.Contains(Pretraga) || m.Seminar.Naziv.Contains(Pretraga));
                }
            }
            

            //Sastavljam konačan popis spajanjem dva popisa

            List<Predbiljezba> KonacanPopis = new List<Predbiljezba>();

            foreach (Predbiljezba predbiljezba in PopisPredbiljezbiPoFiltru)
            {
                foreach (Predbiljezba predbiljezba1 in DohvacenePretrage)
                {
                    if (predbiljezba == predbiljezba1)
                    {
                        KonacanPopis.Add(predbiljezba);
                    }
                }
            }

            return View(KonacanPopis);
        }

        public ActionResult UrediPredbiljezbu(int id)
        {
            //Dobavljam predbilježbu
            Predbiljezba predbiljezba = new Predbiljezba();
            predbiljezba = db.Predbiljezbe.Where(u => u.IdPredbiljezba == id).FirstOrDefault();

            //Popis Seminara
            List<Seminar> seminari = new List<Seminar>();
            seminari = db.Seminari.ToList();

           // Riječnik za popis seminara
            var PopisSeminara = new Dictionary<int,string >
            { };

                foreach (Seminar item in seminari)
            {
                PopisSeminara.Add( item.IdSeminar,item.Naziv);            
            }

            ViewBag.PopisSeminara = new SelectList(PopisSeminara, "Key", "Value");
                 
            return View(predbiljezba);
        }

        [HttpPost]
        public ActionResult UrediPredbiljezbu(Predbiljezba predbiljezba)
        {
                            
            //Nema smisla da je datum stariji od ove godine, osigurava od iskakanja Error-a

            if (predbiljezba.Datum.CompareTo(DateTime.Parse("2020.01.01")) < 0)
            {
                predbiljezba.Datum = DateTime.Now;
            }


            if (ModelState.IsValid)
            {
                db.Entry(predbiljezba).State = EntityState.Modified;            
                db.SaveChanges();
            }
            return RedirectToAction("Predbiljezbe");
        }
#endregion

#region SEMINARI I CRUD OPERACIJE -- USER

        //Userov uvid u seminare
        public ActionResult Seminari()
        {
            //Koristim ViewModel - u kontruktoru se nalazi još informacija

            SeminarIBrojPolaznikaViewModel seminarIBrojPolaznikaViewModel = new SeminarIBrojPolaznikaViewModel();

            return View(seminarIBrojPolaznikaViewModel);
        }

        [HttpPost]
        public ActionResult Seminari(string Pretraga)
        {

            //Pretraga po nazivu
            if (!Pretraga.IsEmpty())
            {
                if (Pretraga.IsDateTime())
                {
                    DateTime PretragaPoDatumu = DateTime.Parse(Pretraga);
                    SeminarIBrojPolaznikaViewModel seminarIBrojPolaznikaViewModel = new SeminarIBrojPolaznikaViewModel()
                    {
                        SeminariUViewModelu = db.Seminari.Where(x => x.Datum.Year == PretragaPoDatumu.Year &&
                        x.Datum.Month == PretragaPoDatumu.Month && x.Datum.Day == PretragaPoDatumu.Day)                     
                    };
                    return View(seminarIBrojPolaznikaViewModel);
                }
                else
                {
                    SeminarIBrojPolaznikaViewModel seminarIBrojPolaznikaViewModel = new SeminarIBrojPolaznikaViewModel()
                    {

                        SeminariUViewModelu = db.Seminari.Where(x => x.Naziv.ToLower().Contains(Pretraga.ToLower()) ||
                        x.Opis.Contains(Pretraga) || x.Zaposlenik.Ime.Contains(Pretraga) || x.Zaposlenik.Prezime.Contains(Pretraga))
                    };
                    return View(seminarIBrojPolaznikaViewModel);
                }             
            }
            else
            {
                SeminarIBrojPolaznikaViewModel seminarIBrojPolaznikaViewModel = new SeminarIBrojPolaznikaViewModel();
                return View(seminarIBrojPolaznikaViewModel);
            }

        }

        public ActionResult UrediSeminar(int id)
        {
            Seminar seminar = new Seminar();
            seminar = db.Seminari.Where(u => u.IdSeminar == id).FirstOrDefault();

            //Koristim Viewbagove za ne-propery informacije - Predavač i Broj Polaznika

            ViewBag.BrojPolaznika = db.Predbiljezbe.Where(u => u.IdSeminar == id).Count();
            ViewBag.ImePredavaca = db.Zaposlenici.Where(u => u.IdZaposlenik == seminar.Predavac).FirstOrDefault().Ime + " "
                + db.Zaposlenici.Where(u => u.IdZaposlenik == seminar.Predavac).FirstOrDefault().Prezime;

            return View(seminar);
        }
        [HttpPost]
        public ActionResult UrediSeminar(Seminar seminar)
        {
            //Ista zaštita kao i prije - pogledati UrediPredbilježbu

            if (seminar.Datum.CompareTo(DateTime.Parse("2020.01.01")) < 0)
            {
                seminar.Datum = DateTime.Now;
            }


            if (ModelState.IsValid)
            {
                db.Entry(seminar).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Seminari");
        }

        public ActionResult IzbrisiSeminar(int id)
        {
            Seminar seminar = new Seminar();

            seminar = db.Seminari.Where(u => u.IdSeminar == id).FirstOrDefault();
            db.Seminari.Remove(seminar);
            db.SaveChanges();

            return RedirectToAction("Seminari");
        }

        public ActionResult StvoriSeminar()
        {
            //Koristim ViewModel - Pogledati konstruktor

            return View(new StvoriSeminar());
        }

        [HttpPost]
        public ActionResult StvoriSeminar(StvoriSeminar stvoriSeminar)
        {
            //Ponovno ista zaštita - Pogledati UrediPredbiljezbu


            if (stvoriSeminar.Seminar.Datum.CompareTo(DateTime.Parse("2020.01.01")) < 0)
            {
                stvoriSeminar.Seminar.Datum = DateTime.Now;
            }

            //Rastavljam podatke iz ViewModela u modele za bazu
            if (ModelState.IsValid)
            {
                Seminar seminar = new Seminar();
                seminar = stvoriSeminar.Seminar;
                db.Seminari.Add(seminar);
                db.SaveChanges();
               return RedirectToAction("Seminari");
            }
            else
            {
                ViewBag.StvoriSeminar = "Nešto je pošlo u krivu";
                return View(stvoriSeminar);
            }
#endregion
        }
    }
}