using System;

namespace Donkey.DomainModels
{
	public class FileInfo: BaseEntity
	{	
		public string Name { get; set; }
		public long Size { get; set; }
		public DateTime? UploadedDate { get; set; }
		public string FilePath { get; set; }

		public int? ExtensionId { get; set; }
		public FileExtension Extension { get; set; }
	}
}
