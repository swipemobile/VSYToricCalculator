using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Resources;
using System.Diagnostics;
using ToricCalculator.Models;
using ToricCalculator.Service.Abstract;

namespace ToricCalculator.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly IStringLocalizer<HomeController> _localizer;
		private readonly ICalculateManager _calculateManager;


		public HomeController(ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment, IStringLocalizer<HomeController> localizer, ICalculateManager calculateManager)
		{
			_logger = logger;
			_hostingEnvironment = hostingEnvironment;
			_localizer = localizer;
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
		public IActionResult Index3(string key, string culture)
		{
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
			var a =_calculateManager.GetFormScreen();
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
				return View(model);
			}
			return RedirectToAction("Terms");

		}

		public void writter()
		{
			ResourceWriter rw = new ResourceWriter("Resources\\a.resx");
			rw.AddResource("Name", "Test");
			rw.AddResource("Ver", 1.0);
			rw.AddResource("Author", "www.java2s.com");
			rw.Generate();
			rw.Close();

			//ResourceManager rm = new ResourceManager("Resources\\a.resx", Assembly.GetExecutingAssembly());
			////String strWebsite = rm.GetString("Website",CultureInfo.CurrentCulture);   
			//String strName = rm.GetString("FirstName");
			//Console.WriteLine(strName);
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
		//public void Screenshot()
		//{
		//	using var bitmap = new Bitmap(1920, 1080);
		//	using (var g = Graphics.FromImage(bitmap))
		//	{
		//		g.CopyFromScreen(0, 0, 0, 0,
		//		bitmap.Size, CopyPixelOperation.SourceCopy);
		//	}
		//	//bitmap.Save("filename.jpg", ImageFormat.Jpeg);

		//}
		//public IActionResult ConvertPdf()
		//{
		//	HtmlToPdfConverter converter = new HtmlToPdfConverter();

		//	WebKitConverterSettings settings = new WebKitConverterSettings();
		//	settings.WebKitPath = Path.Combine(_hostingEnvironment.ContentRootPath, "QtBinariesWindows");
		//	converter.ConverterSettings = settings;

		//	PdfDocument document = converter.Convert("https://toriccalculator.azurewebsites.net/Home/Terms");

		//	MemoryStream ms = new MemoryStream();
		//	document.Save(ms);
		//	document.Close(true);

		//	ms.Position = 0;
		//	FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");
		//	fileStreamResult.FileDownloadName = "test2.pdf";
		//	return fileStreamResult;
		//}
		//public IActionResult ExportToPdf()
		//{
		//	HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.WebKit);
		//	WebKitConverterSettings settings = new WebKitConverterSettings();
		//	settings.WebKitPath = Path.Combine(_hostingEnvironment.ContentRootPath, "QtBinariesWindows");
		//	htmlConverter.ConverterSettings = settings;
		//	PdfDocument document = htmlConverter.Convert("https://www.google.com");
		//	MemoryStream stream = new MemoryStream();
		//	document.Save(stream);
		//	return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "Output.pdf");
		//}

		//	var astigm = document.getElementById("inducedAstigmatism").value; // (Cerrahiye Bağlı Gelişen Astigmatizma(*))
		//	var steepAxis = document.getElementById("axisY").value; //steep Axis (Dik Eksen)
		//	var astigIncis = 0.4;
		//	var incisAxis = document.getElementById("kesiID").value; //kesi yeri, incis location

		//	var cx = astigm * Math.cos(steepAxis * 2) + astigIncis * Math.cos(incisAxis * 2);
		//	var cy = astigm * Math.sin(steepAxis * 2) + astigIncis * Math.sin(incisAxis * 2);
		//	var al = Math.sqrt((Math.pow(cx, 2) + Math.pow(cy, 2)));
		//	var q  = 45 - 0.5 * Math.atan(cx / cy);


		//	var inducedAxis = incisAxis > 90 ? incisAxis - 90 : parseFloat(incisAxis) + 90;
		//	var cx2 = parseFloat(astigm) * parseFloat(Math.cos(parseFloat(steepAxis) / parseFloat(28, 6478897566))) + parseFloat(parseFloat(astigIncis) * parseFloat(Math.cos(inducedAxis / parseFloat(28, 6478897566))));
		//	var cy2 = parseFloat(astigm) * parseFloat(Math.sin(steepAxis / parseFloat(28, 6478897566))) + parseFloat(astigIncis) * parseFloat(Math.sin(inducedAxis / parseFloat(28, 6478897566) ));
		//	var fi = Math.atan(cx2 / cy2) * parseFloat(57,2957795131);
		//	var fi2 = cy2 < 0 ? fi - 180 : fi;

		//	var crossedAstigm = Math.sqrt(Math.pow(cx, 2) + Math.pow(cy, 2));
		//	var crossedAxis = 45 - 0.5 * fi2;
		//}



		//		mwh customera davet gönderdi,
		//customer üye oldu yani REGISTER'dan üye oldu dim,

		/* margin-top: 56%; */
		/*margin: 20% 0% 0% 1%;
			float: left;
			background-color: #ffffff;
			padding: 0%;*/
		/* margin: 20% 0% 0% 1%; */
		/*border-radius: 0px 10px 10px 0px;*/
		/* padding: 3.6%; */
		/*padding: 4%;*/


		//function formules()
		//{
		//	debugger;

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
