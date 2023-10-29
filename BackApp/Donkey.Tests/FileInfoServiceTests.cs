using AutoMapper;
using Donkey.DAL.Context;
using Donkey.DomainModels;
using Donkey.Interfaces;
using Donkey.Services;
using Donkey.Services.AutomapperConfigure;
using Donkey.Tests.Context;
using Donkey.ViewModels.FIleInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;
using Xunit;

namespace Donkey.Tests
{
	public class FileInfoServiceTests : DbContextTestsBase
	{
		private readonly AutoMocker _autoMocker;
		private readonly IMapper _mapper;

		public FileInfoServiceTests() : base()
		{
			_autoMocker = new AutoMocker();
			_mapper = new MapperConfiguration(cfg => new MapperConfigure(cfg)).CreateMapper();
		}

		[Fact]
		public async void SaveFileTest_ShouldAddOneFileInfo()
		{
			// Arrange				
			var returnValue = (extension: ".txt", count: "f1.txt");
			_autoMocker.Setup<IFileStorageService, Task<(string extension, string filePath)>>(x => x.UploadFile(It.IsAny<IFormFile>())).ReturnsAsync(returnValue);			
			_autoMocker.Use<DonkeyDBContext>(DbContext);

			var expected = DbContext.FileInfos.Local.Count + 1;
			var fileInfoService = _autoMocker.CreateInstance<FileInfoService>();			

			// Act
			await fileInfoService.SaveFile(new FormFile(null, 0, 5000, "newFile.txt", "newFile.txt"));		

			// Assert
			Assert.Equal(expected, DbContext.FileInfos.Local.Count);
		}

		[Fact]
		public void MappingTest__ShouldBeEqual()
		{
			// Arrange	
			string expectedFileName = "File.txt";			
			long expectedFileSize = 123456;
			var file = new FileInfo { Name = expectedFileName, Size = expectedFileSize };

			// Act
			var fileInfoListViewModel = _mapper.Map<FileInfoListViewModel>(file);			

			// Assert
			Assert.True(expectedFileName == fileInfoListViewModel.Name && expectedFileSize == fileInfoListViewModel.Size);
		}
	}
}
