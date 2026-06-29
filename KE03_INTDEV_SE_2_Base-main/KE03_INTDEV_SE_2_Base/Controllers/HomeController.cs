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
            const int lowStockLimit = 9;
            
            // Products with low stock
            List<Product> lowStockProducts = _productRepository.GetAllProducts()
                .Where(product => product.Stock <= lowStockLimit)
                .OrderBy(product => product.Stock)
                .ToList();

            // All current orders
            List<OrderListItemViewModel> orders = await _orderService.GetOrdersAsync();

            List<OrderListItemViewModel> allOpenOrders = orders
                .Where(order => order.Status != "Afgeleverd")
                .ToList();

            List<OrderListItemViewModel> openOrders = allOpenOrders
                .OrderBy(o =>
                    o.Status == "Ontvangen" ? 0 :
                    o.Status == "Klaar voor verzenden" ? 1 :
                    o.Status == "Kan worden opgehaald" ? 2 :
                    o.Status == "Wordt bezorgd" ? 3 :
                    4)
                .ThenBy(o => o.OrderDate)
                .Take(5)
                .ToList();

            ViewBag.OpenOrders = openOrders;

            List<ComplaintListItemViewModel> allComplaints =
                await _complaintService.GetComplaintsAsync();

            // ONLY complaints whose status is exactly "Open"
            List<ComplaintListItemViewModel> openComplaintsAll = allComplaints
                .Where(c => c.Status.Equals("Open", StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.ComplaintDate)
                .ToList();

            int openComplaintCount = openComplaintsAll.Count;

            ViewBag.OpenComplaintCount = openComplaintCount;

            ViewBag.OpenComplaints = openComplaintsAll
                .Take(5)
                .ToList();

            List<Product> allProducts = _productRepository
                .GetAllProducts()
                .ToList();

            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                WelcomeText = "Welkom",
                DateText = DateTime.Now.ToString("dddd d MMMM yyyy"),
                LowStockProducts = lowStockProducts
            };

            // Complaint alert
            if (openComplaintCount >= 10)
            {
                ViewBag.ComplaintAlertColor = "red";
            }
            else if (openComplaintCount >= 1)
            {
                ViewBag.ComplaintAlertColor = "yellow";
            }
            else
            {
                ViewBag.ComplaintAlertColor = "blue";
            }

            // Order alert
            int receivedOrders = orders.Count(o =>
                o.Status.Equals("Ontvangen", StringComparison.OrdinalIgnoreCase));

            ViewBag.ReceivedOrders = receivedOrders;

            if (receivedOrders >= 10)
            {
                ViewBag.OrderAlertColor = "red";
            }
            else if (receivedOrders >= 1)
            {
                ViewBag.OrderAlertColor = "yellow";
            }
            else
            {
                ViewBag.OrderAlertColor = "blue";
            }

            // Product alert
            int criticalProducts =
            allProducts.Count(p => p.Stock <= 4);

            int warningProducts =
                allProducts.Count(p => p.Stock >= 5 && p.Stock <= 9);

            int healthyProducts =
                allProducts.Count(p => p.Stock >= 10);

            if (criticalProducts > 0)
            {
                ViewBag.ProductAlertColor = "red";
                ViewBag.ProductAlertText = $"{criticalProducts} producten hebben een groot tekort aan voorraad.";
            }
            else if (warningProducts > 0)
            {
                ViewBag.ProductAlertColor = "yellow";
                ViewBag.ProductAlertText = $"{warningProducts} producten hebben een lage voorraad.";
            }
            else
            {
                ViewBag.ProductAlertColor = "blue";
                ViewBag.ProductAlertText = "Alle producten hebben voldoende voorraad.";
            }

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