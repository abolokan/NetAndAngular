using Donkey.Common;
using Donkey.DAL.Context;
using Donkey.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackApp
{
	public class Startup
	{
		readonly string _myAllowSpecificOrigins = "myAllowSpecificOrigins";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			string[] corsList = new string[] { "http://localhost:40017" };

			services.AddCors(options => options.AddPolicy(_myAllowSpecificOrigins,
					builder =>
					{
						builder
							.AllowAnyHeader()
							.AllowCredentials()
							.AllowAnyMethod()
							.WithOrigins(corsList);
					}));

			services.AddControllers();

			services.DefaultConfigureServices<DonkeyDBContext>(Configuration);

			services.Configure<FormOptions>(o => {
				o.ValueLengthLimit = int.MaxValue;
				o.MultipartBodyLengthLimit = int.MaxValue;
				o.MemoryBufferThreshold = int.MaxValue;
			});

			// configure strongly typed settings objects			
			services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}			

			app.UseRouting();

			app.UseCors(_myAllowSpecificOrigins);

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
