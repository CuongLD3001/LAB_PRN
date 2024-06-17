using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LAB_2_LEDUYCUONG_HE163193.Controllers
{
    public class TeachersController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public TeachersController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Route("teachers")]
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5265/api/Teachers");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var teachers = JsonConvert.DeserializeObject<List<dynamic>>(content);
                return View(teachers);
            }

            return View("Error");
        }
    }
}