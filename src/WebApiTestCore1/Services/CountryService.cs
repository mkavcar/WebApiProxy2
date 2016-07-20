using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTestCore1.Interfaces;
using WebApiTestCore1.Models;

namespace WebApiTestCore1.Services
{
    public class CountryService : ICountryService
    {
        private const string cachekey = "CountryList";
        private readonly IDistributedCache cache;

        private void SetCache(List<Country> list)
        {
            if (list != null)
                cache.Set(cachekey, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(list)));
        }

        public CountryService(IDistributedCache Cache)
        {
            cache = Cache;
        }

        public List<Country> GetAll()
        {
            var data = cache.Get(cachekey);
            if (data != null)
            {
                return JsonConvert.DeserializeObject<List<Country>>(Encoding.ASCII.GetString(data));
            }

            return null;
        }

        public Country Get(string Code)
        {
            var list = GetAll();
            return list?.Where(w => w.Code == Code?.ToUpper()).FirstOrDefault();
        }

        public void Add(Country Country)
        {
            var list = GetAll();
            list?.Add(Country);
        }

        public void Update(Country Country)
        {
            var item = Get(Country.Code);
            if (item != null)
                item.Name = Country.Name;
        }

        public void Delete(string Code)
        {
            var list = GetAll();
            var item = Get(Code);
            if (item != null)
                list?.Remove(item);
        }
    }
}
