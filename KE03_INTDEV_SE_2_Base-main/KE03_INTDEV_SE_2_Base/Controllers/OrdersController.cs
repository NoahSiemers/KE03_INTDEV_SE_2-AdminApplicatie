using KE03_INTDEV_SE_2_Base.Services;
using KE03_INTDEV_SE_2_Base.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class OrdersController : Controller
    {
        private const int PageSize = 10;

        private readonly IExternalOrderService _orderService;

        public OrdersController(IExternalOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(
            string? search,
            string sortBy = "datum",
            string sortDirection = "desc",
            int page = 1)
        {
            List<OrderListItemViewModel> allOrders = await _orderService.GetOrdersAsync();

            IEnumerable<OrderListItemViewModel> filteredOrders = FilterOrders(allOrders, search);

            filteredOrders = SortOrders(filteredOrders, sortBy, sortDirection);

            int totalOrders = filteredOrders.Count();
            int totalPages = (int)Math.Ceiling(totalOrders / (double)PageSize);

            if (page < 1)
            {
                page = 1;
            }

            if (totalPages > 0 && page > totalPages)
            {
                page = totalPages;
            }

            List<OrderListItemViewModel> pagedOrders = filteredOrders
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            OrderIndexViewModel viewModel = new OrderIndexViewModel
            {
                Orders = pagedOrders,
                Search = search,
                SortBy = sortBy,
                SortDirection = sortDirection,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalOrders = totalOrders,
                TotalRevenue = filteredOrders.Sum(order => order.TotalPrice)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            OrderDetailsViewModel? viewModel = await _orderService.GetOrderByIdAsync(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            await _orderService.UpdateOrderStatusAsync(id, status);

            return RedirectToAction(nameof(Details), new { id });
        }

        private IEnumerable<OrderListItemViewModel> FilterOrders(
            IEnumerable<OrderListItemViewModel> orders,
            string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return orders;
            }

            string searchValue = search.Trim();

            return orders.Where(order =>
                ContainsText(order.Id.ToString(), searchValue) ||
                ContainsText(order.CustomerName, searchValue) ||
                ContainsText(order.City, searchValue) ||
                ContainsText(order.Status, searchValue));
        }

        private IEnumerable<OrderListItemViewModel> SortOrders(
            IEnumerable<OrderListItemViewModel> orders,
            string sortBy,
            string sortDirection)
        {
            bool descending = sortDirection == "desc";

            return sortBy.ToLower() switch
            {
                "order" => descending
                    ? orders.OrderByDescending(order => order.Id)
                    : orders.OrderBy(order => order.Id),

                "klant" => descending
                    ? orders.OrderByDescending(order => order.CustomerName)
                    : orders.OrderBy(order => order.CustomerName),

                "stad" => descending
                    ? orders.OrderByDescending(order => order.City)
                    : orders.OrderBy(order => order.City),

                "status" => descending
                    ? orders.OrderByDescending(order => order.Status)
                    : orders.OrderBy(order => order.Status),

                "totaal" => descending
                    ? orders.OrderByDescending(order => order.TotalPrice)
                    : orders.OrderBy(order => order.TotalPrice),

                _ => descending
                    ? orders.OrderByDescending(order => order.OrderDate)
                    : orders.OrderBy(order => order.OrderDate)
            };
        }

        private bool ContainsText(string value, string searchValue)
        {
            return value.Contains(searchValue, StringComparison.OrdinalIgnoreCase);
        }
    }
}
