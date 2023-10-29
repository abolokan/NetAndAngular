using System.Collections.Generic;

namespace Donkey.DomainModels
{
	public class FileExtension : BaseEntity
	{
		public string Name { get; set; }

		public List<FileInfo> FileInfos { get; set; }
	}
}
