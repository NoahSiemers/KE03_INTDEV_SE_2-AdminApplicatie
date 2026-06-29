using KE03_INTDEV_SE_2_Base.ViewModels;

namespace KE03_INTDEV_SE_2_Base.Services
{
    public interface IExternalOrderService
    {
        Task<List<OrderListItemViewModel>> GetOrdersAsync();

        Task<OrderDetailsViewModel?> GetOrderByIdAsync(int id);

        Task UpdateOrderStatusAsync(int id, string status);

        Task UpdatePackedStateAsync(int orderId, string productName, bool packed);

        List<string> GetAvailableStatuses();
    }
}