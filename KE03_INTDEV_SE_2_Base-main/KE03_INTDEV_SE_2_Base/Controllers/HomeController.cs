using DataAccessLayer.Interfaces;
using KE03_INTDEV_SE_2_Base.Models;
using KE03_INTDEV_SE_2_Base.Services;
using KE03_INTDEV_SE_2_Base.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IExternalOrderService _orderService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductRepository productRepository,
            IExternalOrderService orderService)
        {
            _logger = logger;
            _productRepository = productRepository;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            const int lowStockLimit = 7;

            List<OrderListItemViewModel> orders = await _orderService.GetOrdersAsync();

            List<OrderListItemViewModel> openOrders = orders
                .Where(order => order.Status != "Afgeleverd")
                .Take(5)
                .ToList();

            ViewBag.OpenOrders = openOrders;

            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                WelcomeText = "Welkom",
                DateText = DateTime.Now.ToString("dddd d MMMM yyyy"),
                LowStockProducts = _productRepository.GetAllProducts()
                    .Where(product => product.Stock <= lowStockLimit)
                    .OrderBy(product => product.Stock)
                    .ToList()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ErrorViewModel viewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
