using System.Diagnostics;
using System.Text.Json;
using KE03_INTDEV_SE_2_Base.Models;
using KE03_INTDEV_SE_2_Base.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                WelcomeText = "Welkom",
                DateText = DateTime.Now.ToString("dddd d MMMM yyyy"),
                Quote = await GetZenQuoteAsync()
            };

            return View(viewModel);
        }

        private async Task<ZenQuoteViewModel> GetZenQuoteAsync()
        {
            try
            {
                string url = "https://zenquotes.io/api/random";

                string json = await _httpClient.GetStringAsync(url);

                var quotes = JsonSerializer.Deserialize<List<ZenQuoteApiResponse>>(json);

                var quote = quotes?.FirstOrDefault();

                if (quote == null)
                {
                    return new ZenQuoteViewModel
                    {
                        Quote = "Geen quote beschikbaar.",
                        Author = ""
                    };
                }

                return new ZenQuoteViewModel
                {
                    Quote = quote.q,
                    Author = quote.a
                };
            }
            catch
            {
                return new ZenQuoteViewModel
                {
                    Quote = "Geen quote beschikbaar.",
                    Author = ""
                };
            }
        }

        private class ZenQuoteApiResponse
        {
            public string q { get; set; } = string.Empty;

            public string a { get; set; } = string.Empty;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}