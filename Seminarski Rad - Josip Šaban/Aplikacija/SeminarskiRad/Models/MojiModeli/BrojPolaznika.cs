using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeminarskiRad.Models.MojiModeli
{
    public class BrojPolaznika
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public int IdSeminara { get; set; }
        public int BrojPolaznikaUKlasi { get; set; }

       public BrojPolaznika()
        {
            this.BrojPolaznikaUKlasi = db.Predbiljezbe.Where(u => u.IdSeminar == IdSeminara).Count();
        }
    }
}