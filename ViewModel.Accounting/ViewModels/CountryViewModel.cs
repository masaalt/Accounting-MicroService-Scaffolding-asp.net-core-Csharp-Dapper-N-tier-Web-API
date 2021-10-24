using System;

namespace ViewModel.Accounting.ViewModels
{
    public class CountryViewModel
    {
        public short CountryId { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string DefaultCode { get; set; }
        public int Population { get; set; }
        public decimal Area { get; set; }
        public decimal Distance { get; set; }
        public decimal LenWid { get; set; }
        public string Capital { get; set; }
        public DateTime Time { get; set; }
    }
}
