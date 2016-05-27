using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    public class NytProxyController : Controller
    {
        // GET: api/values
        [HttpGet]
        public async Task<dynamic> Get(string section)
        {
            if (string.IsNullOrEmpty(section)) section = "world";
            var url = $"http://api.nytimes.com/svc/topstories/v1/{section}.json?api-key=70444bc7247c7b99f726453419c61740:5:74385476";
            
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    return data;
                }
                else
                {
                    return this.BadRequest(new { error = "An error occured with your request!" });
                }
                
            }
        }
    }
}
