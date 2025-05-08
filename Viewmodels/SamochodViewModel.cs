namespace Wypozyczalnia.ViewModels
{
    public class SamochodViewModel
    {
        public int IdSamochodu { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int RokProdukcji { get; set; }
        public string Kolor { get; set; }
        public bool Dostepnosc { get; set; }
        public int PojSilnika { get; set; }
        public int MocSilnika { get; set; }
        public int CenaZaWypozyczenie { get; set; }
        public string FullName => $"{Marka} {Model}";
    }
}