using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class ProductIndexViewModel
    {
        public List<Product> Products { get; set; } = new();

        public List<Product> LowStockProducts { get; set; } = new();

        public string? Search { get; set; }

        public string? Category { get; set; }

        public string SortBy { get; set; } = "id";

        public string SortDirection { get; set; } = "asc";

        public bool LowStockOnly { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public List<string> Categories { get; set; } = new();
    }
}