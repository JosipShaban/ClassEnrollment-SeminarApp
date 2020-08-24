using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeminarskiRad.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Korisničko ime je obvezno")]
        [StringLength(40, ErrorMessage = "Korisničko ime mora biti manje od 40 slova")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Korisničko ime je obvezno")]
        [StringLength(40, ErrorMessage = "Korisničko ime mora biti manje od 40 slova")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Korisničko ime je obvezno")]
        [StringLength(40, ErrorMessage = "Korisničko ime mora biti manje od 40 slova")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Korisničko ime je obvezno")]
        [StringLength(40, ErrorMessage = "Korisničko ime mora biti manje od 40 slova")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required]     
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi loziku")]
        [Compare("Password", ErrorMessage = "Lozinka i potvrda lozinke nisu iste")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Ime je obvezno")]
        [StringLength(30, ErrorMessage = "Ime mora biti manje od 30 slova")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obvezno")]
        [StringLength(40, ErrorMessage = "Prezime mora biti manje od 40 slova")]
        public string Prezime { get; set; }
        public string Email { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Korisničko ime je obvezno")]
        [StringLength(40, ErrorMessage = "Korisničko ime mora biti manje od 40 slova")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Korisničko ime je obvezno")]
        [StringLength(40, ErrorMessage = "Korisničko ime mora biti manje od 40 slova")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }
    }
}
