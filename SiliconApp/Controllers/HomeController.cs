using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiliconWebApp.ViewModels;
using System.Text;
namespace SiliconApp.Controllers
{
    public class HomeController(HttpClient http) : Controller
    {
        private readonly HttpClient _http = http;

        //Authorize kan du sätta om du vill säkra upp autentisering och tillgång till olika sidor. 
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        //AllowAnonymous är bra om vi har satt en authorize på något, då säger den här att den är tillgänglig för alla andra. 
        [Route("/error")]
        public IActionResult Error404()
        {
            return View();
        }

        [Route("/denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Subscribe()
        {
            ViewData["Status"] = false;
            return View(new SubscriberViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscriberViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(viewModel);
                    using var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _http.PostAsync("https://localhost:7296/api/subscribers", content);

                    //var url = $"https://localhost:7296/api/Subscribers?email={viewModel.Email}";
                    //var request = new HttpRequestMessage(HttpMethod.Post, url);
                    //var response = await http.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        ViewData["Status"] = "Subscribed";

                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        ViewData["Status"] = "AlreadyExists";
                    }
                }
                catch
                {
                    ViewData["Status"] = "ConnectionFailed";
                }
            }
            else
            {
                ViewData["Status"] = "Invalid";
            }
            return View(viewModel);
        }
    }
}
