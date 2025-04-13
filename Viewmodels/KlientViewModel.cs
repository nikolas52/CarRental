namespace Wypozyczalnia.ViewModels
{
    public class KlientViewModel
    {
        public int IdKlienta { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string FullName => $"{Imie} {Nazwisko}";
    }
}