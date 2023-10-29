using Donkey.Common;
using Donkey.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace Donkey.Services
{
	public class FileStorageService : IFileStorageService
	{
		private readonly IFileSystem _fileSystem;
		private readonly string _fileStoragePath;

		public FileStorageService(IFileSystem fileSystem, IOptions<AppSettings> settings)
		{
			_fileSystem = fileSystem;
			_fileStoragePath = settings.Value.FileStoragePath;
		}

		/// <summary>
		/// Key: extension
		/// Value: filePath
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public async Task<(string extension, string filePath)> UploadFile(IFormFile file)
		{
			if (!_fileSystem.Directory.Exists(_fileStoragePath))
			{
				_fileSystem.Directory.CreateDirectory(_fileStoragePath);
			}
			
			var extension = _fileSystem.Path.GetExtension(file.Name);
			var filePath = _fileSystem.Path.Combine(_fileStoragePath, $"{Guid.NewGuid()}.${extension}");

			var result = (extension: extension, filePath: filePath);

			using (var fs = _fileSystem.File.OpenWrite(filePath))
			{
				await file.CopyToAsync(fs);
				fs.Flush();
			}

			return result;
		}

		public async Task<FileStreamResult> DownloadAsync(string filePath)
		{

			var fileName = _fileSystem.Path.GetFileName(filePath);
			var memory = new MemoryStream();

			using (var stream = new FileStream(filePath, FileMode.Open))
			{
				await stream.CopyToAsync(memory);
			}
			memory.Position = 0;

			var result = new FileStreamResult(memory, GetMimeType(fileName));
			result.FileDownloadName = fileName;
			return result;
		}

		private string GetMimeType(string fileName)
			=> string.IsNullOrEmpty(fileName)
				? null
				: MimeTypeMap.GetMimeType(_fileSystem.Path.GetExtension(fileName));
	}
}
