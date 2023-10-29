using Donkey.Common;

namespace Donkey.ViewModels.FIleInfo
{
	public class FileInfoFilterModel : BaseFilter
	{
		public FileInfoFilter Filter { get; set; }
	}

	public class FileInfoFilter
	{
		public int? ExtensionId { get; set; }
	}
}
