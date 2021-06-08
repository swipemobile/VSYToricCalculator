using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace PDFDownloader.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public void selectpdf()
		{
			string url = "https://toriccalculator.azurewebsites.net/home/formpdf?email=erkatbihter@gmaaik.com&SurgeonName=Surgeon%20Bihter&Phone=8273238&Clinic=Clinic%201&ReferenceNo=Naz&FlatK=40&FlatAxis=0&SteepAxis=90&SteepK=45&IOLType=2&IOLSphericalEquivalent=44.50&SurgicallyInducedAstigmatism=1&IncisionLocation=0&Country=UK&EyeSelection=OD";


			HtmlToPdf converter = new HtmlToPdf();
			/*converter.Options.PdfPageSize = PdfPageSize.A4;
			converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
			converter.Options.WebPageWidth = 1024;*/

			converter.Options.MinPageLoadTime = 2;

			converter.Options.MaxPageLoadTime = 30;

			//converter.Options.WebPageHeight = 500;

			
			PdfDocument doc = converter.ConvertUrl(url);
			
			//PdfDocument doc2 = converter.ConvertHtmlString("<b>Selam Turgut</b><i>bugun nasılsın</i><a href='www.google.com'>google</a>");

			// save pdf document
			doc.Save("PdfTestNaz.pdf");
			
			// close pdf document
			doc.Close();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}