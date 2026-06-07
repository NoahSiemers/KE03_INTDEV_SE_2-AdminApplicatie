namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class ComplaintIndexViewModel
    {
        public List<ComplaintListItemViewModel> Complaints { get; set; } = new List<ComplaintListItemViewModel>();

        public string? Search { get; set; }

        public string StatusFilter { get; set; } = "all";

        public string CategoryFilter { get; set; } = "all";

        public string SortBy { get; set; } = "datum";

        public string SortDirection { get; set; } = "desc";

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalComplaints { get; set; }

        public int OpenComplaints { get; set; }

        public int UrgentComplaints { get; set; }

        public List<string> StatusOptions { get; set; } = new List<string>();

        public List<string> CategoryOptions { get; set; } = new List<string>();

        public List<string> PriorityOptions { get; set; } = new List<string>();

        public List<string> AssigneeOptions { get; set; } = new List<string>();
    }
}