using Donkey.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donkey.Tests.Context
{
	public class DbContextTestsBase: IDisposable
	{
		private static volatile bool _isInitialized;

		protected readonly DonkeyDBContext DbContext;

		protected DbContextTestsBase()
		{
			var optionsBuilder = new DbContextOptionsBuilder<DonkeyDBContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());			

			DbContext = new DonkeyDBContext(optionsBuilder.Options);
			DbContext.Database.EnsureCreated();

			if (_isInitialized)
			{
				return;
			}

			_isInitialized = true;
		}

		public void Dispose()
		{
			DbContext.Database.EnsureDeleted();
			DbContext.Dispose();
		}
	}
}
