using Donkey.Common;
using Donkey.Interfaces;
using Donkey.ViewModels.FIleInfo;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BackApp.API.Controllers
{
	[Route("api/files")]
	[ApiController]
	public class FileController : Controller
	{
		private readonly IFileInfoService _fileInfoService;
		public FileController(IFileInfoService fileInfoService)
		{
			_fileInfoService = fileInfoService;
		}

		[HttpPost]
		public async Task<QueryResult<FileInfoListViewModel>> GetList(FileInfoFilterModel filter)
		{
			return await _fileInfoService.GetList(filter);
		}


		[HttpPost("upload"), DisableRequestSizeLimit]
		public async Task<int?> Upload()
		{
			var formCollection = await Request.ReadFormAsync();
			var file = formCollection.Files.First();

			return await _fileInfoService.SaveFile(file);
		}

		[HttpGet("download/{fileId}")]
		public async Task<FileStreamResult> Download(int fileId)
		{
			return await _fileInfoService.Download(fileId);
		}

		[HttpGet("extensions")]
		public async Task<FileExtensionListViewModel[]> GetExtensions()
		{
			return await _fileInfoService.GetExtensions();
		}
	}
}
