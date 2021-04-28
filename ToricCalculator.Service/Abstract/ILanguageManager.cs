using System;
using System.Collections.Generic;
using System.Text;
using ToricCalculator.Service.Model;
using ToricCalculator.Service.ResponseModel;

namespace ToricCalculator.Service.Abstract
{
	public interface ILanguageManager
	{
		string GetLanguageTranslationByKeyAndCulture(string key, string culture);
		List<LanguageTransModel> GetLanguageTranslationByKey(string key);
		Dictionary<string, string> GetLocalizedValue(string key);
		List<JsonLocaliazator> GetJsonLocaliazators(string[] keys);
		List<LanguageTransModel> GetLanguageTrans();
		void AddOrUpdateResource(string key, string value);
	}
}
