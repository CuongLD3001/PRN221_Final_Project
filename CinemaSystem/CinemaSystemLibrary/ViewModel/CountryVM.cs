using CinemaSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSystemLibrary.ViewModel
{
    public class CountryVM : ICountryManagement
    {
        public void AddCountry(string countryCode, string countryName)
        {
            CountryManagement.Instance.AddCountry(countryCode, countryName);
        }

        public void DeleteCountry(string countryCode)
        {
            CountryManagement.Instance.DeleteCountry(countryCode);
        }

        public List<Country> GetAllCountries()
        {
           return CountryManagement.Instance.GetAllCountries();
        }

        public void UpdateCountry(string countryCode, string countryName)
        {
            CountryManagement.Instance.UpdateCountry(countryCode, countryName);
        }
    }
}
