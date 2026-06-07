using Microsoft.AspNetCore.Mvc.Rendering;

namespace KE03_INTDEV_SE_2_Base.ViewModels
{
	public class StaffViewModel
	{
		public int StaffId { get; set; }

		public string FirstName { get; set; } = string.Empty;

		public string LastName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public int FunctionId { get; set; }

		public bool Active { get; set; }

		public List<SelectListItem> Functions { get; set; } = [];
	}
}
