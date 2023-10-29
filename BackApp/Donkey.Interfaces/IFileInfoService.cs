using Donkey.Common;
using Donkey.ViewModels.FIleInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Donkey.Interfaces
{
	public interface IFileInfoService
	{
		Task<int?> SaveFile(IFormFile file);

		Task<QueryResult<FileInfoListViewModel>> GetList(FileInfoFilterModel baseFilter);

		Task<FileStreamResult> Download(int fileId);

		Task<FileExtensionListViewModel[]> GetExtensions();
	}
}
