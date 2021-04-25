using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToricCalculator.Service.Abstract;
using ToricCalculator.Service.Model;
using ToricCalculator.Service.ResponseModel;

namespace ToricCalculator.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HomeController : ControllerBase
	{
		private readonly ICalculateManager _calculateManager;
		public HomeController(ICalculateManager calculateManager)
		{
			_calculateManager = calculateManager;
		}
		[HttpGet]
		public ServiceResponse<FormSecrenModel> GetLanguageSupport()
		{
			return _calculateManager.GetFormScreen();
		}
	}
}
