﻿using LockWarden.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockWarden.DataAccess.Interfaces.IRepositories
{
	public interface ILoginRepository : IGenericRepository<Login>
	{
		public Task<bool> AddAllAsync(List<Login> entities);
		public Task<bool> DeleteOldAsync();
		public Task<bool> DeleteAllAsync(int userId);

	}
}
