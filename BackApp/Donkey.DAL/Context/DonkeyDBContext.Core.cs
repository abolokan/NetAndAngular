using Microsoft.EntityFrameworkCore;

namespace Donkey.DAL.Context
{
	public sealed partial class DonkeyDBContext: DbContext
	{		
		public DonkeyDBContext(DbContextOptions<DonkeyDBContext> options) : base(options)
		{			
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// applying Configurations 
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(DonkeyDBContext).Assembly);
		}
	}
}
