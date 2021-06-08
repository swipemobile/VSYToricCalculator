using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using ToricCalculator.Models;
using ToricCalculator.Service.Abstract;
using Microsoft.AspNetCore.Http;
using ToricCalculator.Service.Model;
using System.Threading.Tasks;
using ToricCalculator.Service;
using System.Collections.Generic;
using System.Linq;
using ToricCalculator.Cms.Models;
using System.IO;
using SelectPdf;
using System.Net;
using GemBox.Document;

namespace ToricCalculator.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ILanguageManager _languageManager;
		private readonly IEmailService _emailService;
		private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
		private readonly ICalculateManager _calculateManager;



		public HomeController(ILogger<HomeController> logger, ILanguageManager languageManager, IEmailService emailService, IStringLocalizer<SharedResource> sharedLocalizer, ICalculateManager calculateManager)
		{
			_logger = logger;
			_languageManager = languageManager;
			_emailService = emailService;
			_sharedLocalizer = sharedLocalizer;
			_calculateManager = calculateManager;
		}

		public IActionResult Index()
		{			
			return View();
		}

		public IActionResult Index2()
		{
			return View();
		}
		public static void testc()
		{
			var a = new Stopwatch();
			a.Start();

			a.Stop();
			var test = a.Elapsed;
		}
		public IActionResult Index3(string key, string culture)
		{
			//var a = _calculateManager.GetFormScreen();
			ViewBag.vsyLanguage = Request.Cookies["vsyLanguage"];
			return View();
		}

		public IActionResult Index4()
		{
			return View();
		}
		public IActionResult Index5()
		{
			return View();
		}
		public IActionResult Index6()
		{
			return View();
		}

		public IActionResult Terms(string language, bool? approve)
		{
			if (language != null)
			{
				ViewBag.vsyLanguage = language;
			}
			else if (Request.Cookies["vsyLanguage"] != null)
			{
				ViewBag.vsyLanguage = Request.Cookies["vsyLanguage"];
			}
			if (approve != null)
			{
				ViewBag.vsyApprove = approve;
			}
			else
			{
				ViewBag.vsyApprove = Request.Cookies["vsyApprove"];
			}
			return View();
		}
		public IActionResult Form(GeneralModel model)
		{
			var languageTrans = _languageManager.GetLanguageTrans(model.Culture);
			LanguageKeys model2 = new LanguageKeys()
			{
				Culture = model.Culture,
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
				WarningK2Bigger = languageTrans.Where(w => w.Key == "WarningK2Bigger").FirstOrDefault().Value,
				Calculate = languageTrans.Where(w=>w.Key=="Calculate").FirstOrDefault().Value
			};
			ViewBag.vsyLanguage = Request.Cookies["vsyLanguage"];
			ViewBag.vsyModel = Request.Cookies["vsyModel"];
			ViewBag.vsyApprove = Request.Cookies["vsyApprove"];
			model.Culture = Request.Cookies["vsyLanguage"];
			if (String.IsNullOrEmpty(model.Key))
			{
				return RedirectToAction("Terms");
			}
			else if (Request.Cookies["vsyKey"] == model.Key)
			{
				return View(model2);
			}
			return RedirectToAction("Terms");
		}

		public IActionResult FormPdf(PdfModel model)
		{
			ViewBag.vsyLanguage = Request.Cookies["vsyLanguage"];
			model.Culture = ViewBag.vsyLanguage;
			ViewBag.vsyModel = Request.Cookies["vsyModel"];
			ViewBag.vsyApprove = Request.Cookies["vsyApprove"];
			return View(model);
		}
		public void selectpdf()
		{
			string url = "https://toriccalculator.azurewebsites.net/home/formpdf?email=erkatbihter@gmaaik.com&SurgeonName=Surgeon%20Bihter&Phone=8273238&Clinic=Clinic%201&ReferenceNo=Naz&FlatK=40&FlatAxis=0&SteepAxis=90&SteepK=45&IOLType=2&IOLSphericalEquivalent=44.50&SurgicallyInducedAstigmatism=1&IncisionLocation=0&Country=UK&EyeSelection=OD";


			string s = "";

			using (WebClient client = new WebClient())
			{
				s = client.DownloadString(url);
			}


			HtmlToPdf converter = new HtmlToPdf();
			/*converter.Options.PdfPageSize = PdfPageSize.A4;
			converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
			converter.Options.WebPageWidth = 1024;*/

			converter.Options.MinPageLoadTime = 2;
			converter.Options.MaxPageLoadTime = 30;


			Message message13 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
						"VSY hata mesajı", "convert url once" , "not1", "","","allignment", "residualAstigm.", "","");
			_emailService.SendEmail3(message13);
			PdfDocument doc = converter.ConvertUrl(url);
			Message message113 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
						"VSY hata mesajı", "convert url sonra", "not1", "", "", "allignment", "residualAstigm.", "", "");
			_emailService.SendEmail3(message113);
			//PdfDocument doc2 = converter.ConvertHtmlString("<b>Selam Turgut</b><i>bugun nasılsın</i><a href='www.google.com'>google</a>");

			// save pdf document
			doc.Save("PdfTestNaz.pdf");
			Message message1113 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
						"VSY hata mesajı", "pdf saved", "not1", "", "", "allignment", "residualAstigm.", "", "");
			_emailService.SendEmail3(message1113);
			// close pdf document
			doc.Close();






			//ComponentInfo.SetLicense("FREE-LIMITED-KEY");

			//DocumentModel document = DocumentModel.Load(url, LoadOptions.HtmlDefault);

			//// When reading any HTML content a single Section element is created.
			//// We can use that Section element to specify various page options.
			//Section section = document.Sections[0];
			//PageSetup pageSetup = section.PageSetup;
			//PageMargins pageMargins = pageSetup.PageMargins;
			//pageMargins.Top = pageMargins.Bottom = pageMargins.Left = pageMargins.Right = 0;

			//// Save output PDF file.
			//document.Save("Output.pdf");
		}


		public void WritePdf2()
		{
			try
			{
				var url2 = "https://toriccalculator.azurewebsites.net/home/formpdf?email=erkatbihter@gmaaik.com&SurgeonName=Surgeon%20Bihter&Phone=8273238&Clinic=Clinic%201&ReferenceNo=Naz&FlatK=40&FlatAxis=0&SteepAxis=90&SteepK=45&IOLType=2&IOLSphericalEquivalent=44.50&SurgicallyInducedAstigmatism=1&IncisionLocation=0&Country=UK&EyeSelection=OD";
			var chromePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
				var output = Path.Combine(Environment.CurrentDirectory, @"pdf2.pdf");
			
				using (var p = new Process())
			{
				p.StartInfo.FileName = chromePath;
				p.StartInfo.Arguments = $"--headless --disable-gpu --print-to-pdf={output} {url2}";
				p.Start();
				p.WaitForExit();
			}
			}
			catch (Exception ex )
			{
				var message4 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
			"VSY hata email",ex.Message.ToString(), "", "not1", "", "", "allignment", "residualAstigm.", "");
				_emailService.SendEmail3(message4);

			}
			

		}
		public IActionResult SendPdf(PdfModel pdf)
		{
			var guid = Request.Cookies["vsyGuidId"];
			//var guid = "1a14d489-900f-4d88-89e7-b008e779c6e3";
			pdf.GuidId = guid;
			//var message5 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
			//	"VSY hata mesajı", guid, pdf.GuidId, "not1", pdf.SurgeonName, pdf.IOLType, "allignment", "residualAstigm.", pdf.EyeSelection);
			//_emailService.SendEmail3(message5);
			//var message3 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
			//	"VSY hata mesajı","not", pdf.GuidId,"not1", pdf.SurgeonName, pdf.IOLType, "allignment", "residualAstigm.", pdf.EyeSelection);
			//_emailService.SendEmail3(message3);
			try
			{
				selectpdf();
				//_languageManager.WritePdf(pdf);
				//_languageManager.SetPdfInfo(pdf);
				//var message = new Message(new string[] { pdf.Recipient },
				//"VSY Toric Calculator", pdf.MailContent, pdf.GuidId, pdf.ReferenceNo, pdf.SurgeonName, pdf.IOLType, "allignment", "residualAstigm.", pdf.EyeSelection);
				//_emailService.SendEmail(message);



			}
			catch (Exception ex)
			{
				var message3 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
				"VSY hata mesajı", ex.Message.ToString() +Environment.NewLine + ex.StackTrace, pdf.GuidId, "not1", pdf.SurgeonName, pdf.IOLType, "allignment", "residualAstigm.", pdf.EyeSelection);
				_emailService.SendEmail3(message3 );

			}

			return Json(new { Success=true});
		}
		public async void SendPdf2(PdfModel pdf)
		{
			var guid = Request.Cookies["vsyGuidId"];
			pdf.GuidId = guid;
			await Writte(pdf);
			
		}
		public async Task Writte(PdfModel pdf)
		{
			await selam(pdf);
			var message = new Message(new string[] { pdf.Recipient },
				 "VSY Toric Calculator", pdf.MailContent, pdf.GuidId, pdf.ReferenceNo, pdf.SurgeonName, pdf.IOLType, "allignment", "residualAstigm.", pdf.EyeSelection);
			 _emailService.SenEmail2(message);
		}
		public async Task selam(PdfModel pdf)
		{
			//_languageManager.WritePdf2(pdf);

		}

		public IActionResult Form2(GeneralModel model)
		{
			ViewBag.vsyLanguage = Request.Cookies["vsyLanguage"];
			ViewBag.vsyModel = Request.Cookies["vsyModel"];
			ViewBag.vsyApprove = Request.Cookies["vsyApprove"];
			model.Culture = Request.Cookies["vsyLanguage"];

			return View(model);
		}

		public void GetResult(float astigm, float steepAxis, float astigIncis, float incisAxis)
		{
			var cx = astigm * Math.Cos(steepAxis * 2) + astigIncis * Math.Cos(incisAxis * 2);
			var cy = astigm * Math.Sin(steepAxis * 2) + astigIncis * Math.Sin(incisAxis * 2);
			var al = Math.Sqrt((Math.Pow(cx, 2) + Math.Pow(cy, 2)));
			var q = 45 - 0.5 * Math.Atan(cx / cy);


			var inducedAxis = incisAxis > 90 ? incisAxis - 90 : incisAxis + 90;
			var cx2 = astigm * (Math.Cos(steepAxis) / Convert.ToInt64(28.6478897566)) + astigIncis * (Math.Cos(inducedAxis / Convert.ToInt64(28.6478897566)));
			var cy2 = astigm * Math.Sin(steepAxis / Convert.ToInt64(28.6478897566)) + astigIncis * Math.Sin(inducedAxis / Convert.ToInt64(28.6478897566));
			var fi = Math.Atan(cx2 / cy2) * Convert.ToInt64(57.2957795131);
			var fi2 = cy2 < 0 ? fi - 180 : fi;

			var crossedAstigm = Math.Sqrt(Math.Pow(cx, 2) + Math.Pow(cy, 2));
			var crossedAxis = 45 - 0.5 * fi2;
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
