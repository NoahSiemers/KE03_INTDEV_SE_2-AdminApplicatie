namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class OrderItemViewModel
    {
        public string ProductName { get; set; } = string.Empty;

        public int Amount { get; set; }

        public decimal PriceAtOrder { get; set; }

        public decimal Subtotal { get; set; }
    }
}
