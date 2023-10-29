using AutoMapper;
using Donkey.Interfaces;
using Donkey.Services.AutomapperConfigure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;

namespace Donkey.Services.Extensions
{
	public static class DefaultConfigureServicesExtension
	{
		/// <summary>
		/// Apply implemetation
		/// </summary>
		/// <typeparam name="Context"></typeparam>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		public static void DefaultConfigureServices<Context>(this IServiceCollection services, IConfiguration configuration) where Context : DbContext
		{
			// EF Context
			services.AddDbContext<Context>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			services.AddTransient<Context, Context>();

			// Services
			services.AddScoped<IFileSystem, FileSystem>();
			services.AddScoped<IFileInfoService, FileInfoService>();
			services.AddScoped<IFileStorageService, FileStorageService>();			
			
			// Automapper profile
			services.AddSingleton(new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfigure(cfg))).CreateMapper());
		}
	}
}
