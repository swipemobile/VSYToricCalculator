using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToricCalculator.Cms.Models;
using ToricCalculator.Models;
using ToricCalculator.Service.Model;
using ToricCalculator.Service.ResponseModel;

namespace ToricCalculator.Service.Abstract
{
	public interface ILanguageManager
	{
		List<LanguageModel> GetLanguageTrans(string culture);
		StateModel UpdateLanguageTrans(LanguageKeys keys);
		void WritePdf(PdfModel model);
		StateModel SetPdfInfo(PdfModel model);
		List<JsonLocaliazator> GetJsonLocaliazators(string[] keys);
		Dictionary<string, string> GetLocalizedValue(string key);
		List<LanguageTransModel> GetLanguageTranslationByKey(string key);
		string GetLanguageTranslationByKeyAndCulture(string key, string culture);
		//void PrintPdf(PdfModel model);
		//void WritePdf2(PdfModel model);
	}
}
