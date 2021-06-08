using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Globalization;
using ToricCalculator.Models;
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
			services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader();
			}));

			services.AddControllersWithViews();
			services.AddHttpContextAccessor();
			services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
			services.AddSingleton<ICacheManager, MemoryCacheManager>();
			services.AddScoped<ILanguageManager, LanguageTranslationManager>();
			services.AddScoped<ICalculateManager, CalculateManager>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix);
			services.AddMemoryCache();

			services.AddLocalization(options =>
			{
				options.ResourcesPath = "Resources";

			});
			services.Configure<IISServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});

			var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
			services.AddSingleton(emailConfig);

			services.Configure<FormOptions>(o => {
				o.ValueLengthLimit = int.MaxValue;
				o.MultipartBodyLengthLimit = int.MaxValue;
				o.MemoryBufferThreshold = int.MaxValue;
			});

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
			app.UseCors("MyPolicy");

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
				new CultureInfo("fr-CA"),
				new CultureInfo("es-ES"),
				new CultureInfo("de-DE"),
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
