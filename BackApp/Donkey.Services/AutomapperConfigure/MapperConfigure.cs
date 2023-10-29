using AutoMapper;
using Donkey.DomainModels;
using Donkey.ViewModels.FIleInfo;

namespace Donkey.Services.AutomapperConfigure
{
	public class MapperConfigure : Profile
	{
		public MapperConfigure(IMapperConfigurationExpression cfg)
		{
			cfg.CreateMap<FileInfo, FileInfoListViewModel>();
			cfg.CreateMap<FileExtension, FileExtensionListViewModel>();
		}
	}
}
