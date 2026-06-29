public class OrderItemViewModel
{
    public string ProductName { get; set; } = "";

    public int Amount { get; set; }

    public decimal PriceAtOrder { get; set; }

    public decimal Subtotal { get; set; }

    public bool IsPacked { get; set; }
}