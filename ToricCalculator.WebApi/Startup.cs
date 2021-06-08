using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToricCalculator.Models;
using ToricCalculator.Service.Abstract;
using ToricCalculator.Service.Concrate;
using ToricCalculator.Service.Model;

namespace ToricCalculator.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToricCalculator.WebApi", Version = "v1" });
			});

			services.AddSingleton<ICacheManager, MemoryCacheManager>();
			services.AddScoped<ILanguageManager, LanguageTranslationManager>();
			services.AddScoped<ICalculateManager, CalculateManager>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddMemoryCache();
			var connectionString = Configuration.GetSection("ConnectionString").Value;
			services.AddSingleton(new AppSettings() { ConnectionString = connectionString });

			var emailConfig = Configuration
  .GetSection("EmailConfiguration")
  .Get<EmailConfiguration>();
			services.AddSingleton(emailConfig);
			services.Configure<FormOptions>(o =>
			{
				o.ValueLengthLimit = int.MaxValue;
				o.MultipartBodyLengthLimit = int.MaxValue;
				o.MemoryBufferThreshold = int.MaxValue;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//if (env.IsDevelopment())
			//{
			//	app.UseDeveloperExceptionPage();
			//	app.UseSwagger();
			//	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToricCalculator.WebApi v1"));
			//}
			
			app.UseDeveloperExceptionPage();

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToricCalculator.WebApi v1"));
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
