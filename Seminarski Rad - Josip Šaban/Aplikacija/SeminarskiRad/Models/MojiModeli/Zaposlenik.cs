using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeminarskiRad.Models.MojiModeli
{
    public class Zaposlenik
    {
        [Key]
        public int IdZaposlenik { get; set; }

        [Required(ErrorMessage = "Ime je obvezno")]
        [StringLength(30, ErrorMessage = "Ime mora biti manje od 30 slova")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obvezno")]
        [StringLength(40, ErrorMessage = "Prezime mora biti manje od 40 slova")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Korisničko ime je obvezno")]
        [StringLength(40, ErrorMessage = "Korisničko ime mora biti manje od 40 slova")]
        [Display(Name ="Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Šifra je obvezna")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}