using CinemaSystemLibrary.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSystemLibrary.DataAccess
{
    public class CountryManagement: ICountryManagement
    {
        private static CountryManagement instance = null;
        private static readonly object instanceLock = new object();
        private readonly CinemaSystemContext context;
        public CountryManagement()
        {
            context= new CinemaSystemContext();
        }
        public static CountryManagement Instance
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CountryManagement();
                    }
                    return instance;
                }
            }
        }

        public void AddCountry(string countryCode, string countryName)
        {
            var existingCountry = context.Countries.FirstOrDefault(c => c.CountryCode == countryCode);
              if (existingCountry == null)
              {
            Country c = new Country
                {
                    CountryCode = countryCode,
                    CountryName = countryName
                };
                context.Countries.Add(c);
                context.SaveChanges();
           }
            else
            {
                throw new Exception("Country has alread exist");
            }
        }

        public void UpdateCountry(string countryCode, string countryName) {
            Country existingCountry = context.Countries.FirstOrDefault(c => c.CountryCode == countryCode);
            if (existingCountry == null)
            {
                throw new Exception("Not found country to update");
            }
            else
            {
                existingCountry.CountryName = countryName;
                existingCountry.CountryCode = countryCode;
                context.SaveChanges();
            }

        }

        public void DeleteCountry(string countryCode)
        {
            Country existingCountry = context.Countries.FirstOrDefault(c => c.CountryCode == countryCode);
            if (existingCountry != null)
            {
                context.Countries.Remove(existingCountry);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Contry not found to delete");
            }
        }

        public List<Country> GetAllCountries()
        {
            return context.Countries.ToList();
        }

    }
}
