using System.ComponentModel.DataAnnotations;

namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class ProductCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public string Supplier { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        public string? MainImageUrl { get; set; }

        public string? SubImageUrl { get; set; }

        public string Description { get; set; } = string.Empty;

        public string? SpecificationName { get; set; }

        public string? SpecificationValue { get; set; }

        public List<string> Categories { get; set; } = new();
    }
}
