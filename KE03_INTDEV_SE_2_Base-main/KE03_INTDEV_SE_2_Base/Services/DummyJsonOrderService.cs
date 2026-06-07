using KE03_INTDEV_SE_2_Base.Dtos;
using KE03_INTDEV_SE_2_Base.ViewModels;
using System.Net.Http.Json;

namespace KE03_INTDEV_SE_2_Base.Services
{
    public class DummyJsonOrderService : IExternalOrderService
    {
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

        private OrderListItemViewModel CreateOrderListItemViewModel(DummyJsonCartDto cart)
        {
            return new OrderListItemViewModel
            {
                Id = cart.Id,
                CustomerName = $"Gebruiker {cart.UserId}",
                OrderDate = CreateDemoDate(cart.Id),
                City = "Niet beschikbaar",
                TotalItems = cart.TotalQuantity,
                TotalPrice = cart.Total
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

            return new OrderDetailsViewModel
            {
                Id = cart.Id,
                CustomerName = $"Gebruiker {cart.UserId}",
                OrderDate = CreateDemoDate(cart.Id),
                FullName = $"Gebruiker {cart.UserId}",
                AddressLine = "Niet beschikbaar via externe API",
                PostalCode = "-",
                City = "Niet beschikbaar",
                TotalItems = cart.TotalQuantity,
                TotalPrice = cart.Total,
                Items = items
            };
        }

        private DateTime CreateDemoDate(int orderId)
        {
            return new DateTime(2026, 5, 1).AddDays(orderId);
        }
    }
}
