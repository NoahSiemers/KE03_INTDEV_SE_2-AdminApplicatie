namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class OrderListItemViewModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public string City { get; set; } = string.Empty;

        public int TotalItems { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
