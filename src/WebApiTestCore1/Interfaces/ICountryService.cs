using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTestCore1.Models;

namespace WebApiTestCore1.Interfaces
{
    public interface ICountryService
    {
        List<Country> GetAll();
        Country Get(string Code);
        void Add(Country Country);
        void Update(Country Country);
        void Delete(string Code); 
    }
}
