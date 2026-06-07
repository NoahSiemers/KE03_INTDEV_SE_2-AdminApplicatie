using KE03_INTDEV_SE_2_Base.ViewModels;

namespace KE03_INTDEV_SE_2_Base.Services
{
    public interface IComplaintService
    {
        Task<List<ComplaintListItemViewModel>> GetComplaintsAsync();

        Task<ComplaintDetailsViewModel?> GetComplaintByIdAsync(int id);

        Task UpdateComplaintStatusAsync(int id, string status);

        List<string> GetAvailableStatuses();
    }
}