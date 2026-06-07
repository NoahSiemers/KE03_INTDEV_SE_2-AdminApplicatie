using KE03_INTDEV_SE_2_Base.ViewModels;

namespace KE03_INTDEV_SE_2_Base.Services
{
    public class DummyComplaintService : IComplaintService
    {
        private static readonly Dictionary<int, string> ComplaintStatuses = new();

        private static readonly List<string> AvailableStatuses = new()
        {
            "Open",
            "In behandeling",
            "Wacht op klant",
            "Gesloten"
        };

        public async Task<List<ComplaintListItemViewModel>> GetComplaintsAsync()
        {
            await Task.Delay(1);

            return Enumerable.Range(1, 20)
                .Select(CreateComplaint)
                .ToList();
        }

        public async Task<ComplaintDetailsViewModel?> GetComplaintByIdAsync(int id)
        {
            await Task.Delay(1);

            return CreateComplaintDetails(id);
        }

        public Task UpdateComplaintStatusAsync(int id, string status)
        {
            if (!AvailableStatuses.Contains(status))
            {
                return Task.CompletedTask;
            }

            ComplaintStatuses[id] = status;

            return Task.CompletedTask;
        }

        public List<string> GetAvailableStatuses()
        {
            return AvailableStatuses;
        }

        private ComplaintListItemViewModel CreateComplaint(int id)
        {
            return new ComplaintListItemViewModel
            {
                Id = id,
                CustomerName = $"Gebruiker {id}",
                CustomerEmail = $"gebruiker{id}@example.nl",
                ComplaintDate = CreateDemoDate(id),
                Subject = $"Klacht {id}",
                Category = GetCategory(id),
                Priority = GetPriority(id),
                Status = GetComplaintStatus(id),
                AssignedTo = "Supportteam"
            };
        }

        private ComplaintDetailsViewModel CreateComplaintDetails(int id)
        {
            return new ComplaintDetailsViewModel
            {
                Id = id,
                CustomerName = $"Gebruiker {id}",
                CustomerEmail = $"gebruiker{id}@example.nl",
                ComplaintDate = CreateDemoDate(id),
                Subject = $"Klacht {id}",
                Category = GetCategory(id),
                Priority = GetPriority(id),
                Status = GetComplaintStatus(id),
                AssignedTo = "Supportteam",
                Channel = "E-mail",
                City = "Amsterdam",
                Summary = $"Samenvatting klacht {id}",
                Description = $"Beschrijving klacht {id}",
                LastUpdated = DateTime.Now.AddDays(-1),
                ResolutionDeadline = DateTime.Now.AddDays(5),
                AvailableStatuses = AvailableStatuses
            };
        }

        private string GetComplaintStatus(int complaintId)
        {
            if (ComplaintStatuses.ContainsKey(complaintId))
            {
                return ComplaintStatuses[complaintId];
            }

            return AvailableStatuses[(complaintId - 1) % AvailableStatuses.Count];
        }

        private string GetCategory(int id)
        {
            string[] categories =
            {
                "Levering",
                "Product",
                "Retour",
                "Facturatie"
            };

            return categories[id % categories.Length];
        }

        private string GetPriority(int id)
        {
            string[] priorities =
            {
                "Laag",
                "Normaal",
                "Hoog",
                "Urgent"
            };

            return priorities[id % priorities.Length];
        }

        private DateTime CreateDemoDate(int id)
        {
            return new DateTime(2026, 5, 1).AddDays(id);
        }
    }
}