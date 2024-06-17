using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LAB_2_LEDUYCUONG_HE163193.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public SchedulesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Route("schedules")]
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5265/api/Schedules");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var schedules = JsonConvert.DeserializeObject<List<dynamic>>(content);
                return View(schedules);
            }

            return View("Error");
        }
    }
}
