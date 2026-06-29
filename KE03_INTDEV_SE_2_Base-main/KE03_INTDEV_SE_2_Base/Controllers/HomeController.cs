using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
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
        private readonly IComplaintService _complaintService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductRepository productRepository,
            IExternalOrderService orderService,
            IComplaintService complaintService)
        {
            _logger = logger;
            _productRepository = productRepository;
            _orderService = orderService;
            _complaintService = complaintService;
        }

        public async Task<IActionResult> Index()
        {
            const int lowStockLimit = 7;
            
            // Products with low stock
            List<Product> lowStockProducts = _productRepository.GetAllProducts()
                .Where(product => product.Stock <= lowStockLimit)
                .OrderBy(product => product.Stock)
                .ToList();

            // All current orders
            List<OrderListItemViewModel> orders = await _orderService.GetOrdersAsync();

            // Assume order id matches product id
            bool lowStockOrderExists = orders.Any(order =>
                lowStockProducts.Any(product => product.Id == order.Id));

            List<OrderListItemViewModel> openOrders = orders
            .Where(order => order.Status != "Afgeleverd")

            // Orders containing low stock products first
            .OrderByDescending(order => order.ContainsLowStockProduct)

            // Then sort by status priority
            .ThenBy(order =>
                order.Status == "Ontvangen" ? 0 :
                order.Status == "Klaar voor verzenden" ? 1 :
                order.Status == "Kan worden opgehaald" ? 2 :
                order.Status == "Wordt bezorgd" ? 3 :
                4)

            // Then oldest orders first
            .ThenBy(order => order.OrderDate)

            .Take(5)
            .ToList();

            ViewBag.OpenOrders = openOrders;

            List<ComplaintListItemViewModel> openComplaints = await _complaintService.GetComplaintsAsync();

            openComplaints = openComplaints
                .OrderBy(c => c.Status == "Open" ? 0 :
                              c.Status == "In behandeling" ? 1 :
                              c.Status == "Wacht op klant" ? 2 : 3)
                .ThenBy(c => c.ComplaintDate)
                .Take(5)
                .ToList();

            ViewBag.OpenComplaints = openComplaints;

            ViewBag.LowStockOrderExists = lowStockOrderExists;

            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                WelcomeText = "Welkom",
                DateText = DateTime.Now.ToString("dddd d MMMM yyyy"),
                LowStockProducts = lowStockProducts
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