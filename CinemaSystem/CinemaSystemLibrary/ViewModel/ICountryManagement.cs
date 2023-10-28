using CinemaSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSystemLibrary.ViewModel
{
    public interface ICountryManagement
    {
        void AddCountry(string countryCode, string countryName);
       void UpdateCountry(string countryCode, string countryName);
        void DeleteCountry(string countryCode);
        List<Country> GetAllCountries();
    }
}
