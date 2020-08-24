using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SeminarskiRad.Models.MojiModeli
{
    public class Seminar
    {
        [Key]
        public int IdSeminar { get; set; }
        [Required(ErrorMessage = "Naziv je obvezan"), StringLength(50, ErrorMessage = "Naziv može imati najviše 50 slova")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Opis je obvezan")]
        public string Opis { get; set; }
        [Required(ErrorMessage = "Datum je obvezan")]
        [DataType(DataType.DateTime,ErrorMessage ="Nije dobar datum")]
        public DateTime Datum { get; set; }
        [Required(ErrorMessage = "Status seminara mora biti definiran")]
        public bool Popunjen { get; set; }

        [Display(Name = "Predavač")]
        public int Predavac { get; set; }
        [ForeignKey("Predavac")]
        public virtual Zaposlenik Zaposlenik { get; set; }

        public Seminar()
        {
            this.Popunjen = false;
            this.Datum = DateTime.Now;
        }
    }


}