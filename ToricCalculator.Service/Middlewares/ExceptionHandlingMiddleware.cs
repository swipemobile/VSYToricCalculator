using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToricCalculator.Service.CustomException;
using ToricCalculator.Service.ResponseModel;

namespace ToricCalculator.Service.Middlewares
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionHandlingMiddleware> logger;
		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			this.next = next;
			this.logger = logger;
		}
		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await next.Invoke(httpContext).ConfigureAwait(false);
			}
			catch (ApiException ex)
			{
				logger.LogError(ex, "Request Error");
				httpContext.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
				httpContext.Response.ContentType = "application/json";
				var response = new ServiceResponse<string>(); // Var olan hatanın key karşılığı yada çevirisi
				Dictionary<string, string> localizedValue = new Dictionary<string, string>();
				localizedValue.Add("tr-TR", "Kayıt Bulunamadı");
				localizedValue.Add("en-US", "Not Found");
				var jsonLoc = new JsonLocaliazator() { Key = "NotFound", LocalizedValue = localizedValue};
				response.Localizations.Add(jsonLoc);
				response.SetException(ex);
				var json = JsonConvert.SerializeObject(response);
				await httpContext.Response.WriteAsync(json);
				// Api exception tipinde geliyor ise çevirisi yapılmış olan gönderdilebilir yada hatanın çevirisi buradan yapılabilir.
				
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Request Error");
				httpContext.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
				httpContext.Response.ContentType = "application/json";
				var response = new ServiceResponse<string>(); // Var olan hatanın key karşılığı yada çevirisi

				response.SetException(ex);
				
				var json = JsonConvert.SerializeObject(response);
				await httpContext.Response.WriteAsync(json);

			}
		}
	}

}
