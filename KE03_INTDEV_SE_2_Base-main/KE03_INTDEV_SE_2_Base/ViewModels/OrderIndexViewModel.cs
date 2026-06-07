namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class OrderIndexViewModel
    {
        public List<OrderListItemViewModel> Orders { get; set; } = new List<OrderListItemViewModel>();

        public string? Search { get; set; }

        public string SortBy { get; set; } = "datum";

        public string SortDirection { get; set; } = "desc";

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalOrders { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
