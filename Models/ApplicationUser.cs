using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Wypozyczalnia.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(25)]
        public string Imie { get; set; }

        [Required]
        [MaxLength(25)]
        public string Nazwisko { get; set; }
    }
}