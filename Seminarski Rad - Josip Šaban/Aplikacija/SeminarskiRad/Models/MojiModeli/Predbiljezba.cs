using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace SeminarskiRad.Models.MojiModeli
{
    public class Predbiljezba
    {
        [Key]
        public int IdPredbiljezba { get; set; }
        [Required(ErrorMessage = "Datum je obvezan")]
        [DataType(DataType.DateTime,ErrorMessage ="Ovo nije valjani datum")]
        public DateTime Datum { get; set; }
        [Required(ErrorMessage = "Ime je obvezno")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obvezno")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Adresa je obvezna")]
        public string Adresa { get; set; }
        [Required(ErrorMessage = "Korisnik je obvezan")]
        [EmailAddress(ErrorMessage ="Ovo nije valjana email adresa")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Broj telefona je obvezan")]
        public string Telefon { get; set; } 
        [Required(ErrorMessage ="Morate odrediti status"),Range(0,2,ErrorMessage ="Vrijednost mora biti: 0(neobrađen),1(prihvaćen) ili 2(odbijen)")]
        public int Status { get; set; }


        [Display(Name = "Seminar")]
        public int IdSeminar { get; set; }
        [ForeignKey("IdSeminar")]
        public virtual Seminar Seminar { get; set; }


        public Predbiljezba()
        {
            this.Datum = DateTime.Now;
            this.Status = 0;
        }
    }
  
}