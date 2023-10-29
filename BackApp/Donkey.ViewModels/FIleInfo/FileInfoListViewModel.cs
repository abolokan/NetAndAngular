using System;

namespace Donkey.ViewModels.FIleInfo
{
	public class FileInfoListViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public long Size { get; set; }

		public DateTime? UploadedDate { get; set; }

		public int? ExtensionId { get; set; }
	}
}
