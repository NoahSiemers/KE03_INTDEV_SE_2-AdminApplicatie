using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
	public class FunctionRepository : IFunctionRepository
	{
		private readonly MatrixIncDbContext _context;

		public FunctionRepository(MatrixIncDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Function> GetAllFunctions()
		{
			return _context.Functions.ToList();
		}
	}
}
