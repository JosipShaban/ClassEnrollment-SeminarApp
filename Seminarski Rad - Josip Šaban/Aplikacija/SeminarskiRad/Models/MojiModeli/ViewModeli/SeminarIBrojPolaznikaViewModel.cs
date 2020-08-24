using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace SeminarskiRad.Models.MojiModeli
{
    public class SeminarIBrojPolaznikaViewModel
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //Popis Seminara
        public IEnumerable<Seminar> SeminariUViewModelu { get; set; }
        //Popis brojeva polaznika seminara
        public IEnumerable<BrojPolaznika> BrojPolaznikaUViewModelu { get; set; }
        //Predavaci seminara
        public IEnumerable<Zaposlenik> PredavaciUViewModelu { get; set; }

       public SeminarIBrojPolaznikaViewModel()
       {
            this.SeminariUViewModelu = db.Seminari.ToList();                 
            
            List<BrojPolaznika> popisBrojevaPolaznika = new List<BrojPolaznika>();

            //Broj polaznika je definiran u konstruktoru klase, tako da je Id dovoljan
            foreach (Seminar item in this.SeminariUViewModelu)
            {
                popisBrojevaPolaznika.Add(new BrojPolaznika() { IdSeminara = item.IdSeminar, BrojPolaznikaUKlasi = db.Predbiljezbe.Where(u => u.IdSeminar == item.IdSeminar && u.Status == 1).Count() });
            }
        

            this.BrojPolaznikaUViewModelu = popisBrojevaPolaznika;

            this.PredavaciUViewModelu = db.Zaposlenici.ToList();  
       }
    }
}