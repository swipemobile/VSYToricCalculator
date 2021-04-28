using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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
		private readonly ILanguageManager _languageManager;
		public HomeController(ICalculateManager calculateManager, ILanguageManager languageManager)
		{
			_calculateManager = calculateManager;
			_languageManager = languageManager;
		}
		//[HttpGet]
		//public ServiceResponse<FormSecrenModel> GetLanguageSupport()
		//{

		//	return _calculateManager.GetFormScreen();
		//}

		[HttpPost("SetResources")]
		public string SetResources()
		{
			string message = "";
			try
			{
				var translates = GetLanguageTrans();
				foreach (var item in translates)
				{
					AddOrUpdateResource(item.Key, item.Value, item.Culture);
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
			return message;
		}

		[HttpPost("GetLanguageTrans")]
		public List<LanguageTransModel> GetLanguageTrans()
		{
			return _languageManager.GetLanguageTrans();
		}
		[HttpPost("AddOrUpdateResource")]
		public void AddOrUpdateResource(string key, string value, string culture)
		{
			var resx = new List<DictionaryEntry>();

			using (var reader = new System.Resources.ResXResourceReader(@"C:\\Users\\Lenovo\\Desktop\\Bihter\\ToricCalculator\\ToricCalculator\\Resources\\Views.Home.Form."+culture+".resx"))
			{
				resx = reader.Cast<DictionaryEntry>().ToList();
				var existingResource = resx.Where(r => r.Key.ToString() == key).FirstOrDefault();
				if (existingResource.Key == null && existingResource.Value == null) // NEW!
				{
					resx.Add(new DictionaryEntry() { Key = key, Value = value });
				}
				else 
				{
					var modifiedResx = new DictionaryEntry()
					{ Key = existingResource.Key, Value = value };
					resx.Remove(existingResource); 
					resx.Add(modifiedResx); 
				}
			}
			using (var writer = new System.Resources.ResXResourceWriter(@"C:\\Users\\Lenovo\\Desktop\\Bihter\\ToricCalculator\\ToricCalculator\\Resources\\Views.Home.Form." + culture + ".resx"))
			{
				resx.ForEach(r =>
				{
					// Again Adding all resource to generate with final items
					writer.AddResource(r.Key.ToString(), r.Value.ToString());
				});
				writer.Generate();
			}
		}
	}
}
