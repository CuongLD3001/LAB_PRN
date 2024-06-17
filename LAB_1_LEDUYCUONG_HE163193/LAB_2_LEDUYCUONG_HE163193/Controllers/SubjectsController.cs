using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LAB_2_LEDUYCUONG_HE163193.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public SubjectsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Route("subjects")]
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5265/api/Subjects");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var subjects = JsonConvert.DeserializeObject<List<dynamic>>(content);
                return View(subjects);
            }

            return View("Error");
        }
    }
}