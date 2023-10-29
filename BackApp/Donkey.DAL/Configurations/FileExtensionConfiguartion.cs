using Donkey.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Donkey.DAL.Configurations
{
	internal class FileExtensionConfiguartion : IEntityTypeConfiguration<FileExtension>
	{
		public void Configure(EntityTypeBuilder<FileExtension> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(e => e.Name).HasMaxLength(100);

			builder.HasMany(x => x.FileInfos)
				.WithOne(x => x.Extension)
				.HasForeignKey(x => x.ExtensionId);
		}
	}
}
