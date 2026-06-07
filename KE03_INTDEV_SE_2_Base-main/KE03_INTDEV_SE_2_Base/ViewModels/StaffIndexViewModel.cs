using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_2_Base.ViewModels
{
	public class StaffIndexViewModel
	{
		public List<Staff> StaffMembers { get; set; } = [];

		public string? Search { get; set; }
	}
}
