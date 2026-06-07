using KE03_INTDEV_SE_2_Base.ViewModels;

namespace KE03_INTDEV_SE_2_Base.Services
{
    public interface IExternalOrderService
    {
        Task<List<OrderListItemViewModel>> GetOrdersAsync();

        Task<OrderDetailsViewModel?> GetOrderByIdAsync(int id);
    }
}
