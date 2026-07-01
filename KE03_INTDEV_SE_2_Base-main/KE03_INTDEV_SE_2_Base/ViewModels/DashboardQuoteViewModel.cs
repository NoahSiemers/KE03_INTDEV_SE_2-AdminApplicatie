namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class DashboardQuoteViewModel
    {
        public string Quote { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public bool IsFallback { get; set; }
    }
}