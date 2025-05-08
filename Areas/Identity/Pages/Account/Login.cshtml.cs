using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Email jest wymagany")]
            [EmailAddress(ErrorMessage = "Nieprawid這wy adres email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Has這 jest wymagane")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Zapami皻aj mnie")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Nieprawid這wy email lub has這.");
                    return Page();
                }
            }
            return Page();
        }
    }

}