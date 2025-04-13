namespace Wypozyczalnia.ViewModels
{
    public class WypozyczenieViewModel
    {
        public int IdWypozyczenia { get; set; }
        public int IdKlienta { get; set; }
        public int IdSamochodu { get; set; }
        public DateTime DataRozpoczecia { get; set; }
        public DateTime DataZakonczenia { get; set; }
        public int Kwota { get; set; }
        public string KlientFullName { get; set; }
        public string SamochodFullName { get; set; }
    }
}