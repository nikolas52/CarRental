using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wypozyczalnia.Models
{
    public class Klient
    {
        public Klient()
        {
            Wypozyczenia = new List<Wypozyczenie>(); 
        }

        [Key]
        public int IdKlienta { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public string Email { get; set; }

        public string Telefon { get; set; }

        public string Adres { get; set; }

        public virtual ICollection<Wypozyczenie> Wypozyczenia { get; set; }

        public string FullName => $"{Imie} {Nazwisko}"; 

       
    }
}