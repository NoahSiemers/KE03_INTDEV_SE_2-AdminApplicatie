using KE03_INTDEV_SE_2_Base.Dtos;
using KE03_INTDEV_SE_2_Base.ViewModels;
using System.Net.Http.Json;

namespace KE03_INTDEV_SE_2_Base.Services
{
    public class DummyJsonQuoteService : IQuoteService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DummyJsonQuoteService> _logger;

        public DummyJsonQuoteService(
            HttpClient httpClient,
            ILogger<DummyJsonQuoteService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<DashboardQuoteViewModel> GetRandomQuoteAsync()
        {
            try
            {
                DummyJsonQuoteDto? quote =
                    await _httpClient.GetFromJsonAsync<DummyJsonQuoteDto>("quotes/random");

                if (quote == null || string.IsNullOrWhiteSpace(quote.Quote))
                {
                    return CreateFallbackQuote();
                }

                return new DashboardQuoteViewModel
                {
                    Quote = quote.Quote,
                    Author = quote.Author,
                    IsFallback = false
                };
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception, "Quote API could not be loaded.");

                return CreateFallbackQuote();
            }
        }

        private DashboardQuoteViewModel CreateFallbackQuote()
        {
            return new DashboardQuoteViewModel
            {
                Quote = "Welkom terug bij Matrix Inc.",
                Author = "Matrix Inc.",
                IsFallback = true
            };
        }
    }
}