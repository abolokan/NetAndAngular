using Donkey.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Donkey.DAL.Context
{
	public sealed partial class DonkeyDBContext : DbContext
	{
		public DbSet<FileInfo> FileInfos { get; set; }
		public DbSet<FileExtension> FileExtensions { get; set; }
	}
}
