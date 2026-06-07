namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string AddressLine { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public int TotalItems { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = string.Empty;

        public string TrackAndTraceCode { get; set; } = string.Empty;

        public DateTime ExpectedDeliveryDate { get; set; }

        public List<string> AvailableStatuses { get; set; } = new List<string>();

        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
    }
}
