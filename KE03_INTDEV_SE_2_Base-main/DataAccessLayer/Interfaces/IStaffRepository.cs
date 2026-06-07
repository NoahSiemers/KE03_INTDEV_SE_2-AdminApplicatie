using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{

	public interface IStaffRepository
	{
		IEnumerable<Staff> GetAllStaff();

		Staff? GetStaffById(int id);

		void AddStaff(Staff staff);

		void UpdateStaff(Staff staff);
	}
}