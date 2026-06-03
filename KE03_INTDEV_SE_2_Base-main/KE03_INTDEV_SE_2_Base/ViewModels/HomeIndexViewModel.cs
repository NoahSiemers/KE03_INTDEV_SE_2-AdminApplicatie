namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class HomeIndexViewModel
    {
        public string WelcomeText { get; set; } = string.Empty;

        public string DateText { get; set; } = string.Empty;

        public ZenQuoteViewModel Quote { get; set; } = new();
    }
}
