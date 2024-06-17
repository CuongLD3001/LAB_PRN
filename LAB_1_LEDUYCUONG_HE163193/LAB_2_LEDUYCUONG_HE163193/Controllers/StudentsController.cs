using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LAB_2_LEDUYCUONG_HE163193.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public StudentsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Route("students")]
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5265/api/Students");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var students = JsonConvert.DeserializeObject<List<dynamic>>(content);
                return View(students);
            }

            return View("Error");
        }
    }
}

