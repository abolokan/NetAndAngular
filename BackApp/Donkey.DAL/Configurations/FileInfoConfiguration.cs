using Donkey.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Donkey.DAL.Configurations
{
	internal class FileInfoConfiguration : IEntityTypeConfiguration<FileInfo>
	{
		public void Configure(EntityTypeBuilder<FileInfo> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(e => e.Name).HasMaxLength(100);
		}
	}
}
