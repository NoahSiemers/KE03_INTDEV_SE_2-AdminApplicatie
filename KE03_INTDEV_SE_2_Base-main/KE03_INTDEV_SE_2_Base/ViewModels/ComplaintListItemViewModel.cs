namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class ComplaintListItemViewModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;

        public DateTime ComplaintDate { get; set; }

        public string Subject { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Priority { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int? OrderId { get; set; }

        public string Channel { get; set; } = string.Empty;

        public string AssignedTo { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime LastUpdated { get; set; }

        public DateTime ResolutionDeadline { get; set; }
    }
}