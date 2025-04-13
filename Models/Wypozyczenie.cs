using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wypozyczalnia.Models
{
    public class Wypozyczenie
    {
        [Key]
        public int IdWypozyczenia { get; set; }

        [ForeignKey("Klient")]

        public int IdKlienta { get; set; }

        [ForeignKey("Samochod")]

        public int IdSamochodu { get; set; }

        public DateTime DataRozpoczecia { get; set; }

        public DateTime DataZakonczenia { get; set; }

        public int Kwota { get; set; }

        public virtual Klient Klient { get; set; }
        public virtual Samochod Samochod { get; set; }
    }
}