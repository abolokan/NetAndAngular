using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Donkey.Interfaces
{
	public interface IFileStorageService
	{
		Task<(string extension, string filePath)> UploadFile(IFormFile file);
		Task<FileStreamResult> DownloadAsync(string filePath);
	}
}
