namespace Wypozyczalnia.ViewModels
{
    public class TopWypozyczaneSamochodyViewModel
    {
        public int IdSamochodu { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int LiczbaWypozyczen { get; set; }
        public string FullName => $"{Marka} {Model}";
    }
}