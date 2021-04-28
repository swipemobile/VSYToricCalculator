//using Localization.SqlLocalizer.DbStringLocalizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ToricCalculator.Service.Abstract;
using ToricCalculator.Service.Concrate;
using ToricCalculator.Service.Model;

namespace ToricCalculator
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
			services.AddControllersWithViews();
			services.AddHttpContextAccessor();
			services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
			services.AddLocalization(options =>
			{
				options.ResourcesPath = "Resources";
			});
			services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix);
			services.AddSingleton<ICacheManager, MemoryCacheManager>();
			services.AddScoped<ILanguageManager, LanguageTranslationManager>();
			services.AddScoped<ICalculateManager, CalculateManager>();
			services.AddMemoryCache();

			var connectionString = Configuration.GetSection("ConnectionString").Value;
			services.AddSingleton(new AppSettings() { ConnectionString = connectionString });
			// init database for localization
			//var sqlConnectionString = Configuration["DbStringLocalizer:ConnectionString"];

			//services.AddDbContext<LocalizationModelContext>(options =>
			//	options.UseSqlite(
			//		sqlConnectionString,
			//		b => b.MigrationsAssembly("ImportExportLocalization")
			//	),
			//	ServiceLifetime.Singleton,
			//	ServiceLifetime.Singleton
			//);

			// Requires that LocalizationModelContext is defined
			//services.AddSqlLocalization(options => options.UseTypeFullNames = true);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			IList<CultureInfo> supportedCultures = new List<CultureInfo>
			{
				new CultureInfo("en-US"),
				new CultureInfo("tr-TR"),
			};

			//app.UseRequestLocalization(new RequestLocalizationOptions
			//{
			//	DefaultRequestCulture = new RequestCulture("en-US"),
			//	SupportedCultures = supportedCultures,
			//	SupportedUICultures = supportedCultures
			//});



			var localizationOptions = new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en-US"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			};
			// Find the cookie provider with LINQ
			

			app.UseRequestLocalization(localizationOptions);

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index3}/{id?}");

				endpoints.MapControllerRoute(
				 name: "culture",
				 pattern: "{controller=Home}/{action=Form}/{key}/{culture}");
			});


			//app.UseMvc(routes =>
			//{
			//	routes.MapRoute(
			//		name: "default",
			//		template: "{controller=Home}/{action=Index}/{id?}");
			//});




		}
	}
}
