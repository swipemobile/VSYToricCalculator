using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ToricCalculator.Service.Abstract;
using ToricCalculator.Service.Model;
using ToricCalculator.Cms.Models;
using ToricCalculator.Models;
using HiQPdf;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using System.Resources;
using ToricCalculator.Service.ResponseModel;
using System.Web;

namespace ToricCalculator.Service.Concrate
{
	public class LanguageTranslationManager : ILanguageManager
	{
		private readonly ICacheManager _cacheManager;
		private readonly AppSettings _appSettings;
		private readonly IEmailService _emailService;
		public LanguageTranslationManager(ICacheManager cacheManager, AppSettings appSettings, IEmailService emailService )
		{
			_cacheManager = cacheManager;
			_appSettings = appSettings;
			_emailService = emailService;
		}

		public List<LanguageModel> GetLanguageTrans(string culture)
		{
			List<LanguageModel> languageTrans = new List<LanguageModel>();
			using (IDbConnection con = new SqlConnection(_appSettings.ConnectionString))
			{
				if (con.State == ConnectionState.Closed) con.Open();
				{
					DynamicParameters parameters = new DynamicParameters();
					parameters.Add("@culture",culture);
					languageTrans = con.Query<LanguageModel>("sp_getLanguageTrans", parameters, commandType: CommandType.StoredProcedure).ToList();
				}
			}
			return languageTrans;
		}

		public StateModel UpdateLanguageTrans(LanguageKeys keys)
		{
			StateModel response = new StateModel();
			using (IDbConnection con = new SqlConnection(_appSettings.ConnectionString))
			{
				if (con.State == ConnectionState.Closed) con.Open();
				{
					DynamicParameters parameters = new DynamicParameters();
					parameters.Add("@Clinic", keys.Clinic != null ? keys.Clinic : String.Empty);
					parameters.Add("@Country", keys.Country != null ? keys.Country : String.Empty);
					parameters.Add("@Culture", keys.Culture != null ? keys.Culture : String.Empty);
					parameters.Add("@Email", keys.Email != null ? keys.Email : String.Empty);
					parameters.Add("@FlatAxis", keys.FlatAxis != null ? keys.FlatAxis : String.Empty);
					parameters.Add("@FlatK", keys.FlatK != null ? keys.FlatK : String.Empty);
					parameters.Add("@IncisionLocation", keys.IncisionLocation != null ? keys.IncisionLocation : String.Empty);
					parameters.Add("@IOLSphericalEquivalent", keys.IOLSphericalEquivalent != null ? keys.IOLSphericalEquivalent : String.Empty);
					parameters.Add("@IOLType", keys.IOLType != null ? keys.IOLType : String.Empty);
					parameters.Add("@Phone", keys.Phone != null ? keys.Phone : String.Empty);
					parameters.Add("@ReferenceNo", keys.ReferenceNo != null ? keys.ReferenceNo : String.Empty);
					parameters.Add("@SteepAxis", keys.SteepAxis != null ? keys.SteepAxis : String.Empty);
					parameters.Add("@SteepK", keys.SteepK != null ? keys.SteepK : String.Empty);
					parameters.Add("@SurgeonName", keys.SurgeonName != null ? keys.SurgeonName : String.Empty);
					parameters.Add("@SurgicallyInducedAstigmatism", keys.SurgicallyInducedAstigmatism != null ? keys.SurgicallyInducedAstigmatism : String.Empty);
					parameters.Add("@Clean", keys.Clean != null ? keys.Clean : String.Empty);

					parameters.Add("@Cancel", keys.Cancel != null ? keys.Cancel : String.Empty);
					parameters.Add("@DescSurgicallyInducedAstigmatism", keys.DescSurgicallyInducedAstigmatism != null ? keys.DescSurgicallyInducedAstigmatism : String.Empty);
					parameters.Add("@LeftEye", keys.LeftEye != null ? keys.LeftEye : String.Empty);
					parameters.Add("@NotificationSuccess", keys.NotificationSuccess != null ? keys.NotificationSuccess : String.Empty);
					parameters.Add("@Ok", keys.Ok != null ? keys.Ok : String.Empty);
					parameters.Add("@Results", keys.Results != null ? keys.Results : String.Empty);
					parameters.Add("@RightEye", keys.RightEye != null ? keys.RightEye : String.Empty);
					parameters.Add("@Send", keys.Send != null ? keys.Send : String.Empty);
					parameters.Add("@TitleEyeInformations", keys.TitleEyeInformations != null ? keys.TitleEyeInformations : String.Empty);
					parameters.Add("@TitleSurgeonInformations", keys.TitleSurgeonInformations != null ? keys.TitleSurgeonInformations : String.Empty);
					parameters.Add("@WarningK1Bigger", keys.WarningK1Bigger != null ? keys.WarningK1Bigger : String.Empty);
					parameters.Add("@WarningK2Bigger", keys.WarningK2Bigger != null ? keys.WarningK2Bigger : String.Empty);
					parameters.Add("@Calculate", keys.Calculate != null ? keys.Calculate : String.Empty);

					response = con.Query<StateModel>("sp_updateLanguageTrans", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
				}
			}
			return response;
		}

		public StateModel SetPdfInfo(PdfModel model)
		{
			StateModel result = new StateModel();
			using (IDbConnection con = new SqlConnection(_appSettings.ConnectionString))
			{
				if (con.State == ConnectionState.Closed) con.Open();
				{
					DynamicParameters parameters = new DynamicParameters();
					if (model.IOLType == "1")
					{
						parameters.Add("IOLType", "Trifocal Toric IOL Acriva Trinova");
					}
					else
					{
						parameters.Add("IOLType", "Monofocal Toric IOL Acriva BB T UDM 611");
					}
					parameters.Add("pdfId", model.GuidId);
					parameters.Add("ReferenceNo", model.ReferenceNo);
					parameters.Add("SurgeonName", model.SurgeonName);
					parameters.Add("Email", model.Email);
					parameters.Add("Phone", model.Phone);
					parameters.Add("Clinic", model.Clinic);
					parameters.Add("Country", model.Country);

					result = con.Query<StateModel>("sp_setPdfInfos", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
				}
			}
			return result;
		}
	

		public void WritePdf(PdfModel model)
		{
			HtmlToPdf htmlToPdfConverter = new HtmlToPdf()
			{
				BrowserWidth = 1200,
				HtmlLoadedTimeout = 600,

			};

			htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
			htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;
			htmlToPdfConverter.Document.Margins = new PdfMargins(5);

			byte[] pdfBuffer = null;
			//var url = "https://htp.azurewebsites.net/Reports/hoseBySize?id=44";
			string url = "https://toriccalculator.azurewebsites.net/home/formpdf?email=" + model.Email +
				"&SurgeonName=" + model.SurgeonName +
				"&Phone=" + model.Phone +
				"&Clinic=" + model.Clinic +
				"&ReferenceNo=" + model.ReferenceNo +
				"&FlatK=" + model.FlatK +
				"&FlatAxis=" + model.FlatAxis +
				"&SteepAxis=" + model.SteepAxis +
				"&SteepK=" + model.SteepK +
				"&IOLType=" + model.IOLType +
				"&IOLSphericalEquivalent=" + model.IOLSphericalEquivalent +
				"&SurgicallyInducedAstigmatism=" + model.SurgicallyInducedAstigmatism +
				"&IncisionLocation=" + model.IncisionLocation +
				"&Country=" + model.Country +
				"&EyeSelection=" + model.EyeSelection
				;

			var url2 = "https://toriccalculator.azurewebsites.net/home/formpdf?email=erkatbihter@gmaaik.com&SurgeonName=Surgeon%20Bihter&Phone=8273238&Clinic=Clinic%201&ReferenceNo=Naz&FlatK=40&FlatAxis=0&SteepAxis=90&SteepK=45&IOLType=2&IOLSphericalEquivalent=44.50&SurgicallyInducedAstigmatism=1&IncisionLocation=0&Country=UK&EyeSelection=OD";
			




			//using (WebClient myWebClient = new WebClient())
			//{
			//	string htmlContent = myWebClient.DownloadString(url);
			//	pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlContent, null);
			//	//pdfBuffer = Encoding.UTF8.GetBytes(htmlContent); ;
			//	string pdfName = "PDF/" + model.GuidId.ToString() + ".pdf";
			//	System.IO.File.WriteAllBytes(pdfName, pdfBuffer);
			//}

			var message32 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
				"VSY hata email", "try öncesi", "", "not1", "", "", "allignment", "residualAstigm.", "");
			_emailService.SendEmail3(message32);

			try
			{
				 htmlToPdfConverter.ConvertUrlToMemory(url);

				var message37 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
				"VSY hata email", "buffer created", "", "not1", "", "", "allignment", "residualAstigm.", "");
				_emailService.SendEmail3(message37);

				string pdfName = "PDF/" + model.GuidId.ToString() + ".pdf";
				System.IO.File.WriteAllBytes(pdfName, pdfBuffer);

				var message3 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
				"VSY hata email", "file created", "", "not1", "", "", "allignment", "residualAstigm.", "");
				_emailService.SendEmail3(message3);
			}
			catch (Exception ex)
			{

				var message3 = new Message(new string[] { "bihter.erkat@smartapps.com.tr" },
				"VSY hata email", ex.Message.ToString(), "", "not1","", "", "allignment", "residualAstigm.", "");
				_emailService.SendEmail3(message3);
			}
			
		}

		//public async void WritePdf2(PdfModel model)
		//{
		//	HtmlToPdf htmlToPdfConverter = new HtmlToPdf()
		//	{
		//		BrowserWidth = 1200,
		//		HtmlLoadedTimeout = 120,
		//	};

		//	htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
		//	htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;
		//	htmlToPdfConverter.Document.Margins = new PdfMargins(5);

		//	byte[] pdfBuffer = null;
		//	string url = "https://localhost:44356/home/formpdf?email=" + model.Email +
		//		"&SurgeonName=" + model.SurgeonName +
		//		"&Phone=" + model.Phone +
		//		"&Clinic=" + model.Clinic +
		//		"&ReferenceNo=" + model.ReferenceNo +
		//		"&FlatK=" + model.FlatK +
		//		"&FlatAxis=" + model.FlatAxis +
		//		"&SteepAxis=" + model.SteepAxis +
		//		"&SteepK=" + model.SteepK +
		//		"&IOLType=" + model.IOLType +
		//		"&IOLSphericalEquivalent=" + model.IOLSphericalEquivalent +
		//		"&SurgicallyInducedAstigmatism=" + model.SurgicallyInducedAstigmatism +
		//		"&IncisionLocation=" + model.IncisionLocation +
		//		"&Country=" + model.Country +
		//		"&EyeSelection=" + model.EyeSelection
		//		;
		//	await Task.Delay(500);

		//	var apdfBuffer =  htmlToPdfConverter.ConvertUrlToMemory(url);

		//	string pdfName = "PDF/" + model.GuidId.ToString() + ".pdf";
		//	 File.WriteAllBytes(pdfName, pdfBuffer);
		//}

		//public void PrintPdf(PdfModel model)
		//{
		//	//HtmlToPdf htmlToPdfConverter = new HtmlToPdf()
		//	//{
		//	//	BrowserWidth = 1200,
		//	//	HtmlLoadedTimeout = 120,
		//	//};

		//	//htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
		//	//htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;
		//	//htmlToPdfConverter.Document.Margins = new PdfMargins(5);

		//	//byte[] pdfBuffer = null;
		//	//string url = "https://localhost:44356/home/formpdf?email=" + model.Email +
		//	//	"&SurgeonName=" + model.SurgeonName +
		//	//	"&Phone=" + model.Phone +
		//	//	"&Clinic=" + model.Clinic +
		//	//	"&ReferenceNo=" + model.ReferenceNo +
		//	//	"&FlatK=" + model.FlatK +
		//	//	"&FlatAxis=" + model.FlatAxis +
		//	//	"&SteepAxis=" + model.SteepAxis +
		//	//	"&SteepK=" + model.SteepK +
		//	//	"&IOLType=" + model.IOLType +
		//	//	"&IOLSphericalEquivalent=" + model.IOLSphericalEquivalent +
		//	//	"&SurgicallyInducedAstigmatism=" + model.SurgicallyInducedAstigmatism +
		//	//	"&IncisionLocation=" + model.IncisionLocation;

		//	//pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);
		//	string pdfName = "PDF/9ec8a1d8-5307-463b-bf86-cc1fbf8d06af" + ".pdf";
		//	//System.IO.File.WriteAllBytes(pdfName, pdfBuffer);


		//	Process p = new Process();
		//	p.StartInfo = new ProcessStartInfo()
		//	{
		//		CreateNoWindow = false,
		//		Verb = pdfName,
		//		FileName = "pdftest" //put the correct path here
		//	};
		//	p.Start();
		//}




		public string GetLanguageTranslationByKeyAndCulture(string key, string culture)
		{
			var cachedKey = $"{key}_{culture}";
			var cachedValue = _cacheManager.Get<string>(cachedKey);
			if (cachedValue != null)
				return cachedValue;

			string dbQuery = "";

			_cacheManager.Add(cachedKey, dbQuery, 60);

			return dbQuery;
		}


		public List<LanguageTransModel> GetLanguageTranslationByKey(string key)
		{
			//var cachedKey = _cacheManager.Get<List<string>>(key);
			//if (cachedKey != null)
			//	return cachedKey;
			//var dbQuery = GetLanguageTrans("tr-TR").Where(w => w.Key == key).ToList();
			List<LanguageTransModel> model = new List<LanguageTransModel>();

			//var dbQuery = new Dictionary<string, string>();

			//_cacheManager.Add(key, dbQuery, 60);

			return model;
		}

		public Dictionary<string, string> GetLocalizedValue(string key)
		{
			Dictionary<string, string> localizedValue = new Dictionary<string, string>();
			var list = GetLanguageTranslationByKey(key);
			foreach (var item in list)
			{
				//localizedValue.Add(item.Culture, item.Value);
				//localizedValue.Add("tr-TR", item.Key);
			}

			return localizedValue;
		}

		public List<JsonLocaliazator> GetJsonLocaliazators(string[] keys)
		{
			var localiazators = new List<JsonLocaliazator>();
			foreach (var key in keys)
			{
				var jsonLoc = new JsonLocaliazator() { Key = key, LocalizedValue = GetLocalizedValue(key) };
				localiazators.Add(jsonLoc);
			}
			return localiazators;
		}
	}
}
