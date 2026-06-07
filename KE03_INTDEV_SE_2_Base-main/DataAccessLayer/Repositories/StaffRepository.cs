using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
	public class StaffRepository : IStaffRepository
	{
		private readonly MatrixIncDbContext _context;

		public StaffRepository(MatrixIncDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Staff> GetAllStaff()
		{
			return _context.Staff.Include(s => s.Function).ToList();
		}

		public Staff? GetStaffById(int id)
		{
			return _context.Staff.Include(s => s.Function).FirstOrDefault(s => s.StaffId == id);
		}

		public void AddStaff(Staff staff)
		{
			_context.Staff.Add(staff);
			_context.SaveChanges();
		}

		public void UpdateStaff(Staff staff)
		{
			_context.Staff.Update(staff);
			_context.SaveChanges();
		}
	}
}
