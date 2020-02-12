using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HighfieldTest.Models;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HighfieldTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ViewResult> Index()
        {
            var users = await GetDataAsync();
            HomeViewModel viewModel = new HomeViewModel()
            {
                users = users,
                ages = users.Select(u => new Age {userId = u.id, originalAge = CalculateAge(u.dob,DateTime.Now), agePlusTwenty = CalculateAge(u.dob, DateTime.Now) + 20 }),
                topColours = (users.GroupBy(u => u.favouriteColour).Select(t => new TopColour { colour = t.Key, count = t.Select(u => u.id).Distinct().Count() })).OrderByDescending(x => x.count).ThenBy(x => x.colour)
            };
            //string reponse = await SubmitData(viewModel);
            return View(viewModel);
        }
        public int CalculateAge(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            {
                age--;
            }

            return age;
        }
        private static async Task<List<User>>  GetDataAsync()
        {
            var jsonstring = client.GetStreamAsync("https://recruitment.highfieldqualifications.com/api/test");
            var users = await JsonSerializer.DeserializeAsync<List<User>>(await jsonstring);
            return users;
        }
        [HttpPost]
        public async Task<string> SubmitData()
        {
            var users = await GetDataAsync();
            HomeViewModel viewModel = new HomeViewModel()
            {
                users = users,
                ages = users.Select(u => new Age { userId = u.id, originalAge = CalculateAge(u.dob, DateTime.Now), agePlusTwenty = CalculateAge(u.dob, DateTime.Now) + 20 }),
                topColours = (users.GroupBy(u => u.favouriteColour).Select(t => new TopColour { colour = t.Key, count = t.Select(u => u.id).Distinct().Count() })).OrderByDescending(x => x.count).ThenBy(x => x.colour)
            };
            var dataAsString = JsonConvert.SerializeObject(viewModel);
            var content = new StringContent(dataAsString);
            client.DefaultRequestHeaders.Accept.Clear();
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("https://recruitment.highfieldqualifications.com/api/test", content);
            var contents = response.Result.Content.ReadAsStringAsync();
            return contents.Result;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
