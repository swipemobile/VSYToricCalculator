using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToricCalculator.Service.Abstract;
using ToricCalculator.Service.Model;
using ToricCalculator.Service.ResponseModel;
using ToricCalculator.WebApi.Models;

namespace ToricCalculator.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HomeController : ControllerBase
	{
		//private readonly ICalculateManager _calculateManager;
		private readonly ILanguageManager _languageManager;
		//private readonly IEmailService _emailService;
		public HomeController( ILanguageManager languageManager)
		{
			//_calculateManager = calculateManager;
			_languageManager = languageManager;
			//_emailService = emailService;
		}

		[HttpGet("GetLanguageTrans")]
		public IActionResult GetLanguageTrans(string culture = "en-US")
		{
			var result = _languageManager.GetLanguageTrans(culture);
			return Ok(result);
		}
		//[HttpGet]
		//public ServiceResponse<FormSecrenModel> GetLanguageSupport()
		//{

		//	return _calculateManager.GetFormScreen();
		//}
		[HttpPost("Calculate")]
		public CalculateModel Calculate()
		{
			CalculateModel result = new CalculateModel() {
				Result = 45
			};
			return result;
		}

		//[HttpPost("SetResources")]
		//public string SetResources()
		//{
		//	string message = "";
		//	try
		//	{
				
		//		var translates = GetLanguageTrans();
		//		foreach (var item in translates)
		//		{
		//			//AddOrUpdateResource(item.Key, item.Value, item.Culture);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		message = ex.Message;
		//	}
		//	return message;
		//}
		//public void AddOrUpdateResource(string key, string value, string culture)
		//{
		//	var a = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
		//	var message5 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
		//			"path full name", a.FullName, "not8", "not1", "", "", "allignment", "residualAstigm.", "");
		//	_emailService.SendEmail3(message5);

		//	//var pathWeb = Directory.GetParent(Directory.GetCurrentDirectory()).Parent + "\\ToricCalculator\\ToricCalculator\\Resources\\Views.Home.Form." + culture + ".resx";

		//	var pathWeb = Directory.GetParent(Directory.GetCurrentDirectory()).Parent + "\\site\\wwwroot\\wwwroot\\Resources\\SharedResource.+" + "tr-TR" + ".resx";
		//	var resx = new List<DictionaryEntry>();

		//	using (var reader = new System.Resources.ResXResourceReader(pathWeb))
		//	{
		//		resx = reader.Cast<DictionaryEntry>().ToList();
		//		var existingResource = resx.Where(r => r.Key.ToString() == key).FirstOrDefault();
		//		if (existingResource.Key == null && existingResource.Value == null) // NEW!
		//		{
		//			resx.Add(new DictionaryEntry() { Key = key, Value = value });
		//		}
		//		else
		//		{
		//			var modifiedResx = new DictionaryEntry()
		//			{ Key = existingResource.Key, Value = value };
		//			resx.Remove(existingResource);
		//			resx.Add(modifiedResx);
		//		}
		//	}
		//	using (var writer = new System.Resources.ResXResourceWriter(pathWeb))
		//	{
		//		resx.ForEach(r =>
		//		{
		//			writer.AddResource(r.Key.ToString(), r.Value.ToString());
		//		});
		//		writer.Generate();
		//	}
		//}

		//[HttpPost("GetLanguageTrans")]
		//public List<LanguageTransModel> GetLanguageTrans()
		//{
		//	return _languageManager.GetLanguageTrans();
		//}
		//[HttpPost("AddOrUpdateResource")]
		//public void AddOrUpdateResource(string key, string value, string culture)
		//{
		//	var resx = new List<DictionaryEntry>();
		//	//GetCurrentDirectory();
		//	//ControllerContext.HttpContext.("~/_xslt/example.xslt");
		//	using (var reader = new System.Resources.ResXResourceReader(@"C:\\Users\\Lenovo\\Desktop\\Bihter\\ToricCalculator\\ToricCalculator\\Resources\\Views.Home.Form."+culture+".resx"))
		//	{
		//		resx = reader.Cast<DictionaryEntry>().ToList();
		//		var existingResource = resx.Where(r => r.Key.ToString() == key).FirstOrDefault();
		//		if (existingResource.Key == null && existingResource.Value == null) // NEW!
		//		{
		//			resx.Add(new DictionaryEntry() { Key = key, Value = value });
		//		}
		//		else 
		//		{
		//			var modifiedResx = new DictionaryEntry()
		//			{ Key = existingResource.Key, Value = value };
		//			resx.Remove(existingResource); 
		//			resx.Add(modifiedResx); 
		//		}
		//	}
		//	using (var writer = new System.Resources.ResXResourceWriter(@"C:\\Users\\Lenovo\\Desktop\\Bihter\\ToricCalculator\\ToricCalculator\\Resources\\Views.Home.Form." + culture + ".resx"))
		//	{
		//		resx.ForEach(r =>
		//		{
		//			// Again Adding all resource to generate with final items
		//			writer.AddResource(r.Key.ToString(), r.Value.ToString());
		//		});
		//		writer.Generate();
		//	}
		//}
	}
}
