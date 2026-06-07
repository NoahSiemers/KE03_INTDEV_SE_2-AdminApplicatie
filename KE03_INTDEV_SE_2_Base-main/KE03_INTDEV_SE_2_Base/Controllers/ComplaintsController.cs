using KE03_INTDEV_SE_2_Base.Services;
using KE03_INTDEV_SE_2_Base.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class ComplaintsController : Controller
    {
        private const int PageSize = 8;

        private readonly IComplaintService _complaintService;

        public ComplaintsController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        public async Task<IActionResult> Index(
            string? search,
            string statusFilter = "all",
            string sortBy = "datum",
            string sortDirection = "desc",
            int page = 1)
        {
            List<ComplaintListItemViewModel> complaints =
                await _complaintService.GetComplaintsAsync();

            IEnumerable<ComplaintListItemViewModel> filteredComplaints = complaints;

            // Search complaints
            if (!string.IsNullOrWhiteSpace(search))
            {
                string searchValue = search.Trim();

                filteredComplaints = filteredComplaints.Where(c =>
                    c.Id.ToString().Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    c.CustomerName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    c.CustomerEmail.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    c.Subject.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    c.Status.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
            }

            // Status filter
            if (!string.Equals(statusFilter, "all", StringComparison.OrdinalIgnoreCase))
            {
                filteredComplaints = filteredComplaints.Where(c =>
                    c.Status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase));
            }

            // Sorting
            bool descending = sortDirection == "desc";

            filteredComplaints = sortBy.ToLower() switch
            {
                "klant" => descending
                    ? filteredComplaints.OrderByDescending(c => c.CustomerName)
                    : filteredComplaints.OrderBy(c => c.CustomerName),

                "status" => descending
                    ? filteredComplaints.OrderByDescending(c => c.Status)
                    : filteredComplaints.OrderBy(c => c.Status),

                _ => descending
                    ? filteredComplaints.OrderByDescending(c => c.ComplaintDate)
                    : filteredComplaints.OrderBy(c => c.ComplaintDate)
            };

            int totalComplaints = filteredComplaints.Count();
            int totalPages = (int)Math.Ceiling(totalComplaints / (double)PageSize);

            if (page < 1)
            {
                page = 1;
            }

            if (totalPages > 0 && page > totalPages)
            {
                page = totalPages;
            }

            List<ComplaintListItemViewModel> pagedComplaints = filteredComplaints
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ComplaintIndexViewModel viewModel = new ComplaintIndexViewModel
            {
                Complaints = pagedComplaints,
                Search = search,
                StatusFilter = statusFilter,
                SortBy = sortBy,
                SortDirection = sortDirection,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalComplaints = totalComplaints,
                OpenComplaints = complaints.Count(c => c.Status != "Gesloten"),
                UrgentComplaints = complaints.Count(c => c.Priority == "Urgent"),
                StatusOptions = _complaintService.GetAvailableStatuses()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            ComplaintDetailsViewModel? complaint =
                await _complaintService.GetComplaintByIdAsync(id);

            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            await _complaintService.UpdateComplaintStatusAsync(id, status);

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}