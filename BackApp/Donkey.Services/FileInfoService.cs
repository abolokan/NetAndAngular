using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donkey.Common;
using Donkey.DAL.Context;
using Donkey.DomainModels;
using Donkey.Interfaces;
using Donkey.ViewModels.FIleInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Donkey.Services
{
	public class FileInfoService : IFileInfoService
	{
		private readonly IFileStorageService _fileStorageService;
		private readonly DonkeyDBContext _donkeyDBContext;
		private readonly IMapper _mapper;

		public FileInfoService(IFileStorageService fileStorageService, DonkeyDBContext donkeyDBContext, IMapper mapper)
		{
			_donkeyDBContext = donkeyDBContext;
			_fileStorageService = fileStorageService;
			_mapper = mapper;
		}

		public async Task<QueryResult<FileInfoListViewModel>> GetList(FileInfoFilterModel baseFilter)
		{
			IQueryable<FileInfo> query = _donkeyDBContext.FileInfos;

			if (baseFilter?.Filter?.ExtensionId != null)
			{
				query = query.Where(x => x.ExtensionId == baseFilter.Filter.ExtensionId);
			}

			var response = new QueryResult<FileInfoListViewModel>();

			if (baseFilter.WithCount)
			{
				response.Count = query.Count();
			}

			query = SetOrder(query, baseFilter.Order);

			if (baseFilter.Skip.HasValue)
			{
				query = query.Skip(baseFilter.Skip.Value);
			}

			if (baseFilter.Take.HasValue)
			{
				query = query.Take(baseFilter.Take.Value);
			}

			response.Items = await Task.FromResult(query.ProjectTo<FileInfoListViewModel>(_mapper.ConfigurationProvider).ToArray());

			return response;
		}


		public async Task<int?> SaveFile(IFormFile file)
		{
			var uploadetFile = await _fileStorageService.UploadFile(file);

			var newFile = new FileInfo
			{
				Name = file.Name,
				Size = file.Length,
				UploadedDate = DateTime.UtcNow,
				FilePath = uploadetFile.filePath
			};


			if (!string.IsNullOrEmpty(uploadetFile.extension))
			{
				var extDb = _donkeyDBContext.FileExtensions.FirstOrDefault(x => x.Name.Equals(uploadetFile.extension));

				if (extDb != null)
				{
					newFile.ExtensionId = extDb.Id;
				}
				else
				{
					newFile.Extension = new FileExtension
					{
						Name = uploadetFile.extension
					};
				}
			}

			await _donkeyDBContext.AddAsync(newFile);
			await _donkeyDBContext.SaveChangesAsync();

			return newFile.ExtensionId;
		}

		public async Task<FileStreamResult> Download(int fileId)
		{
			var file = _donkeyDBContext.FileInfos.FirstOrDefault(x => x.Id == fileId);
			return await _fileStorageService.DownloadAsync(file.FilePath);
		}

		public async Task<FileExtensionListViewModel[]> GetExtensions()
		{
			return await Task.FromResult(_donkeyDBContext.FileExtensions.ProjectTo<FileExtensionListViewModel>(_mapper.ConfigurationProvider).ToArray());
		}

		private IQueryable<FileInfo> SetOrder(IQueryable<FileInfo> query, List<Order> orderList)
		{
			if (orderList == null || !orderList.Any())
			{
				return query;
			}

			PropertyInfo[] _entityProperties = typeof(FileInfo).GetProperties();

			var entityParameter = Expression.Parameter(typeof(FileInfo));

			orderList.ForEach(orderDto =>
			{
				var entityProperty = _entityProperties.FirstOrDefault(e => e.Name.ToLower() == orderDto.Field.ToLower());

				if (entityProperty == null)
				{
					return;
				}

				var expression = Expression.Property(entityParameter, entityProperty);

				var orderExpression =
					Expression.Lambda<Func<FileInfo, object>>(Expression.Convert(expression, typeof(object)), entityParameter);

				query = orderDto.Desc == true ? query.OrderByDescending(orderExpression) : query.OrderBy(orderExpression);
			});

			return query;
		}
	}
}
