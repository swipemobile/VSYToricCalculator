using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using ToricCalculator.Cms.Models;
using ToricCalculator.Service.Abstract;
using ToricCalculator.Service.Model;

namespace ToricCalculator.Cms.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ILanguageManager _languageManager;
		private readonly IEmailService _emailService;

		public HomeController(ILogger<HomeController> logger, ILanguageManager languageManager, IEmailService emailService)
		{
			_logger = logger;
			_languageManager = languageManager;
			_emailService = emailService;
		}

		public IActionResult Index(string culture = "en-US")
		{
			var languageTrans = _languageManager.GetLanguageTrans(culture);
			LanguageKeys model = new LanguageKeys()
			{
				Culture = culture,
				Clinic = languageTrans.Where(w => w.Key == "Clinic").FirstOrDefault().Value,
				Country = languageTrans.Where(w => w.Key == "Country").FirstOrDefault().Value,
				Email = languageTrans.Where(w => w.Key == "Email").FirstOrDefault().Value,
				FlatAxis = languageTrans.Where(w => w.Key == "FlatAxis").FirstOrDefault().Value,
				FlatK = languageTrans.Where(w => w.Key == "FlatK").FirstOrDefault().Value,
				IncisionLocation = languageTrans.Where(w => w.Key == "IncisionLocation").FirstOrDefault().Value,
				IOLSphericalEquivalent = languageTrans.Where(w => w.Key == "IOLSphericalEquivalent").FirstOrDefault().Value,
				IOLType = languageTrans.Where(w => w.Key == "IOLType").FirstOrDefault().Value,
				Phone = languageTrans.Where(w => w.Key == "Phone").FirstOrDefault().Value,
				ReferenceNo = languageTrans.Where(w => w.Key == "ReferenceNo").FirstOrDefault().Value,
				SteepAxis = languageTrans.Where(w => w.Key == "SteepAxis").FirstOrDefault().Value,
				SteepK = languageTrans.Where(w => w.Key == "SteepK").FirstOrDefault().Value,
				SurgeonName = languageTrans.Where(w => w.Key == "SurgeonName").FirstOrDefault().Value,
				SurgicallyInducedAstigmatism = languageTrans.Where(w => w.Key == "SurgicallyInducedAstigmatism").FirstOrDefault().Value,
				Cancel = languageTrans.Where(w => w.Key == "Cancel").FirstOrDefault().Value,
				Results = languageTrans.Where(w => w.Key == "Results").FirstOrDefault().Value,
				DescSurgicallyInducedAstigmatism = languageTrans.Where(w => w.Key == "DescSurgicallyInducedAstigmatism").FirstOrDefault().Value,
				Clean = languageTrans.Where(w => w.Key == "Clean").FirstOrDefault().Value,
				LeftEye = languageTrans.Where(w => w.Key == "LeftEye").FirstOrDefault().Value,
				NotificationSuccess = languageTrans.Where(w => w.Key == "NotificationSuccess").FirstOrDefault().Value,
				Ok = languageTrans.Where(w => w.Key == "Ok").FirstOrDefault().Value,
				RightEye = languageTrans.Where(w => w.Key == "RightEye").FirstOrDefault().Value,
				Send = languageTrans.Where(w => w.Key == "Send").FirstOrDefault().Value,
				TitleEyeInformations = languageTrans.Where(w => w.Key == "TitleEyeInformations").FirstOrDefault().Value,
				TitleSurgeonInformations = languageTrans.Where(w => w.Key == "TitleSurgeonInformations").FirstOrDefault().Value,
				WarningK1Bigger = languageTrans.Where(w => w.Key == "WarningK1Bigger").FirstOrDefault().Value,
				WarningK2Bigger = languageTrans.Where(w => w.Key == "WarningK2Bigger").FirstOrDefault().Value
			};

			return View(model);
		}

		public IActionResult LanguageSettings(string culture = "en-US")
		{
			return View(_languageManager.GetLanguageTrans(culture));
		}
		[HttpPost]
		public IActionResult LanguageSettings(LanguageKeys model)
		{
			try
			{
				var result = _languageManager.UpdateLanguageTrans(model);
				//TODO: Yanlışla toast.
				if (result.State)
				{
					var languageTranses = _languageManager.GetLanguageTrans(model.Culture);
					//foreach (var item in languageTranses.Where(w => w.Culture == model.Culture))
					//{
					//	AddOrUpdateResource(item.Key, item.Value, item.Culture);
					//}

					return View(_languageManager.GetLanguageTrans(model.Culture));
				}
			}
			catch (System.Exception ex)
			{
				var message5 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
					"VSY hata mesajı", ex.Message.ToString(), "not8", "not1", "", "", "allignment", "residualAstigm.", "");
				_emailService.SendEmail3(message5);
			}
			
			return View(_languageManager.GetLanguageTrans(model.Culture));
		}

		[HttpPost]
		public IActionResult Index(LanguageKeys model)
		{
			var result = _languageManager.UpdateLanguageTrans(model);

			string URL = "http://www.example.com/recepticle.aspx";
			try
			{
				// Get HTML data   
				WebClient client = new WebClient();
				Stream data = client.OpenRead(URL);
				StreamReader reader = new StreamReader(data);
				string str = "";
				str = reader.ReadLine();
				while (str != null)
				{
					str = reader.ReadLine();
				}
				data.Close();
			}
			catch (WebException exp)
			{

			}
			//TODO: Yanlışla toast.
			if (result.State)
			{
				
				//var languageTranses = _languageManager.GetLanguageTrans(model.Culture);
				//foreach (var item in languageTranses.Where(w=>w.Culture == model.Culture))
				//{
				//	AddOrUpdateResource(item.Key, item.Value, item.Culture);
				//}

				return View(model);
			}
			return View(model);
		}

		public void AddOrUpdateResource(string key, string value, string culture)
		{
			var a = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
			var message5 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
					"path full name", a.FullName, "not8", "not1", "", "", "allignment", "residualAstigm.", "");
			_emailService.SendEmail3(message5);

			//var pathWeb = Directory.GetParent(Directory.GetCurrentDirectory()).Parent + "\\ToricCalculator\\ToricCalculator.Service\\Resources\\SharedResource." + culture + ".resx";

			var pathWeb = Directory.GetParent(Directory.GetCurrentDirectory()).Parent + "\\site\\wwwroot\\Resources\\SharedResource."+culture+".resx";
			var resx = new List<DictionaryEntry>();

			using (var reader = new System.Resources.ResXResourceReader(pathWeb))
			{
				resx = reader.Cast<DictionaryEntry>().ToList();
				var existingResource = resx.Where(r => r.Key.ToString() == key).FirstOrDefault();
				if (existingResource.Key == null && existingResource.Value == null) 
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
			using (var writer = new System.Resources.ResXResourceWriter(pathWeb))
			{
				resx.ForEach(r =>
				{
					writer.AddResource(r.Key.ToString(), r.Value.ToString());
				});
				writer.Generate();
			}
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
