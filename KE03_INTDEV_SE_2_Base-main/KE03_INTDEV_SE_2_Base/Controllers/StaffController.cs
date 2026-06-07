using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using KE03_INTDEV_SE_2_Base.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace KE03_INTDEV_SE_2_Base.Controllers;

public class StaffController : Controller
{
	private readonly IStaffRepository _staffRepository;
	private readonly IFunctionRepository _functionRepository;

	public StaffController(IStaffRepository staffRepository, IFunctionRepository functionRepository)
	{
		_staffRepository = staffRepository;
		_functionRepository = functionRepository;
	}

	public IActionResult Index(string? search)
	{
		var allStaff = _staffRepository.GetAllStaff().ToList();

		IEnumerable<Staff> filteredStaff = allStaff;

		if (!string.IsNullOrWhiteSpace(search))
		{
			filteredStaff = filteredStaff.Where(s =>
				s.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
				s.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
				s.Email.Contains(search, StringComparison.OrdinalIgnoreCase));
		}

		var viewModel = new StaffIndexViewModel
		{
			StaffMembers = filteredStaff.ToList(),
			Search = search
		};

		return View(viewModel);
	}

	public IActionResult Create()
	{
		var viewModel = new StaffViewModel
		{
			Functions = _functionRepository.GetAllFunctions()
				.Select(f => new SelectListItem { Value = f.FunctionId.ToString(), Text = f.Name })
				.ToList()
		};
		return View(viewModel);
	}

	[HttpPost]
	public IActionResult Create(StaffViewModel viewModel)
	{
		if (!ModelState.IsValid)
		{
			viewModel.Functions = _functionRepository.GetAllFunctions()
				.Select(f => new SelectListItem { Value = f.FunctionId.ToString(), Text = f.Name })
				.ToList();
			return View(viewModel);
		}

		var staff = new Staff
		{
			FirstName = viewModel.FirstName,
			LastName = viewModel.LastName,
			Email = viewModel.Email,
			FunctionId = viewModel.FunctionId,
			Active = viewModel.Active
		};

		_staffRepository.AddStaff(staff);

		return RedirectToAction(nameof(Index));
	}

	public IActionResult Edit(int id)
	{
		var staff = _staffRepository.GetStaffById(id);
		if (staff == null) return NotFound();

		var viewModel = new StaffViewModel
		{
			StaffId = staff.StaffId,
			FirstName = staff.FirstName,
			LastName = staff.LastName,
			Email = staff.Email,
			FunctionId = staff.FunctionId,
			Active = staff.Active,
			Functions = _functionRepository.GetAllFunctions()
				.Select(f => new SelectListItem { Value = f.FunctionId.ToString(), Text = f.Name })
				.ToList()
		};

		return View(viewModel);
	}

	[HttpPost]
	public IActionResult Edit(StaffViewModel viewModel)
	{
		if (!ModelState.IsValid)
		{
			viewModel.Functions = _functionRepository.GetAllFunctions()
				.Select(f => new SelectListItem { Value = f.FunctionId.ToString(), Text = f.Name })
				.ToList();
			return View(viewModel);
		}

		var staff = _staffRepository.GetStaffById(viewModel.StaffId);

		if (staff == null) return NotFound();

		staff.FirstName = viewModel.FirstName;
		staff.LastName = viewModel.LastName;
		staff.Email = viewModel.Email;
		staff.FunctionId = viewModel.FunctionId;
		staff.Active = viewModel.Active;

		_staffRepository.UpdateStaff(staff);

		return RedirectToAction(nameof(Index));
	}
}