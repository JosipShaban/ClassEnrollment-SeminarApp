using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeminarskiRad.Models.MojiModeli.ViewModeli
{
    public class StvoriSeminar
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //Novi seminar
        public Seminar Seminar { get; set; }
        //Popis predavača
        public IEnumerable<SelectListItem> Predavaci { get; set; }

        public StvoriSeminar()
        {
            this.Seminar = new Seminar();
            this.Predavaci = db.Zaposlenici.Select(x => new SelectListItem
            {
                Text = x.Ime + " " + x.Prezime,
                Value = x.IdZaposlenik.ToString()
            });
        }
    }
}