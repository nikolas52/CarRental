using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Imiê jest wymagane")]
            [StringLength(25, ErrorMessage = "Imiê nie mo¿e mieæ wiêcej ni¿ 25 znaków")]
            public string Imie { get; set; }

            [Required(ErrorMessage = "Nazwisko jest wymagane")]
            [StringLength(25, ErrorMessage = "Nazwisko nie mo¿e mieæ wiêcej ni¿ 25 znaków")]
            public string Nazwisko { get; set; }

            [Required(ErrorMessage = "Email jest wymagany")]
            [EmailAddress(ErrorMessage = "Nieprawid³owy adres email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Has³o jest wymagane")]
            [StringLength(100, ErrorMessage = "Has³o musi mieæ od {2} do {1} znaków", MinimumLength = 4)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "PotwierdŸ has³o")]
            [Compare("Password", ErrorMessage = "Has³a nie s¹ takie same")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Imie = Input.Imie,
                    Nazwisko = Input.Nazwisko
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}