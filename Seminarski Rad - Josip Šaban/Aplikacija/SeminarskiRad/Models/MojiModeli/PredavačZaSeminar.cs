using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SeminarskiRad.Models.MojiModeli
{
    public class PredavačZaSeminar
    {
        [Key]
        public int Id { get; set; }
        
        public int IdZaposlenika { get; set; }
        [ForeignKey ("IdZaposlenika")]
        public virtual Zaposlenik Zaposlenik { get; set; }

        public int IdSeminara { get; set; }
        [ForeignKey ("IdSeminara")]
        public virtual Seminar Seminar { get; set; }

    }
}