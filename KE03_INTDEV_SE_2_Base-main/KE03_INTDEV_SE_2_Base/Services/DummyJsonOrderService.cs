using KE03_INTDEV_SE_2_Base.Dtos;
using KE03_INTDEV_SE_2_Base.ViewModels;
using System.Net.Http.Json;

namespace KE03_INTDEV_SE_2_Base.Services
{
    public class DummyJsonOrderService : IExternalOrderService
    {
        private static readonly Dictionary<int, string> OrderStatuses = new Dictionary<int, string>();

        private static readonly List<string> AvailableStatuses = new List<string>
        {
            "Ontvangen",
            "Klaar voor verzenden",
            "Kan worden opgehaald",
            "Wordt bezorgd",
            "Afgeleverd"
        };

        private readonly HttpClient _httpClient;

        public DummyJsonOrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrderListItemViewModel>> GetOrdersAsync()
        {
            DummyJsonCartResponseDto? response = await _httpClient
                .GetFromJsonAsync<DummyJsonCartResponseDto>("carts");

            if (response == null)
            {
                return new List<OrderListItemViewModel>();
            }

            List<OrderListItemViewModel> orders = response.Carts
                .Select(CreateOrderListItemViewModel)
                .ToList();

            return orders;
        }

        public async Task<OrderDetailsViewModel?> GetOrderByIdAsync(int id)
        {
            DummyJsonCartDto? cart = await _httpClient
                .GetFromJsonAsync<DummyJsonCartDto>($"carts/{id}");

            if (cart == null)
            {
                return null;
            }

            return CreateOrderDetailsViewModel(cart);
        }

        public Task UpdateOrderStatusAsync(int id, string status)
        {
            if (!AvailableStatuses.Contains(status))
            {
                return Task.CompletedTask;
            }

            OrderStatuses[id] = status;

            return Task.CompletedTask;
        }

        public List<string> GetAvailableStatuses()
        {
            return AvailableStatuses;
        }

        private OrderListItemViewModel CreateOrderListItemViewModel(DummyJsonCartDto cart)
        {
            return new OrderListItemViewModel
            {
                Id = cart.Id,
                CustomerName = $"Gebruiker {cart.UserId}",
                OrderDate = CreateDemoDate(cart.Id),
                City = "Niet beschikbaar",
                TotalItems = cart.TotalQuantity,
                TotalPrice = cart.Total,
                Status = GetOrderStatus(cart.Id)
            };
        }

        private OrderDetailsViewModel CreateOrderDetailsViewModel(DummyJsonCartDto cart)
        {
            List<OrderItemViewModel> items = cart.Products
                .Select(product => new OrderItemViewModel
                {
                    ProductName = product.Title,
                    Amount = product.Quantity,
                    PriceAtOrder = product.Price,
                    Subtotal = product.Total
                })
                .ToList();

            DateTime orderDate = CreateDemoDate(cart.Id);

            return new OrderDetailsViewModel
            {
                Id = cart.Id,
                CustomerName = $"Gebruiker {cart.UserId}",
                OrderDate = orderDate,
                FullName = $"Gebruiker {cart.UserId}",
                AddressLine = "Niet beschikbaar via externe API",
                PostalCode = "-",
                City = "Niet beschikbaar",
                TotalItems = cart.TotalQuantity,
                TotalPrice = cart.Total,
                Status = GetOrderStatus(cart.Id),
                TrackAndTraceCode = CreateTrackAndTraceCode(cart.Id, cart.UserId),
                ExpectedDeliveryDate = orderDate.AddDays(3 + cart.Id % 3),
                AvailableStatuses = AvailableStatuses,
                Items = items
            };
        }

        private string GetOrderStatus(int orderId)
        {
            if (OrderStatuses.ContainsKey(orderId))
            {
                return OrderStatuses[orderId];
            }

            int statusIndex = (orderId - 1) % AvailableStatuses.Count;

            return AvailableStatuses[statusIndex];
        }

        private string CreateTrackAndTraceCode(int orderId, int userId)
        {
            return $"MX-{userId:D3}-{orderId:D5}";
        }

        private DateTime CreateDemoDate(int orderId)
        {
            return new DateTime(2026, 5, 1).AddDays(orderId);
        }
    }
}
