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
using ToricCalculator.Service.ResponseModel;
using System.Resources;
namespace ToricCalculator.Service.Concrate
{
	public class LanguageTranslationManager : ILanguageManager
	{
		private readonly ICacheManager _cacheManager;
		private readonly AppSettings _appSettings;
		public LanguageTranslationManager(ICacheManager cacheManager, AppSettings appSettings)
		{
			_cacheManager = cacheManager;
			_appSettings = appSettings;
		}

		
		public void AddOrUpdateResource(string key, string value)
		{
			//var resx = new List<DictionaryEntry>();

			//using (var reader = new System.Resources.ResXResourceReader(@"C:\\Users\\Lenovo\\Desktop\\Bihter\\ToricCalculator\\ToricCalculator\\Resources\\Views.Home.Form.tr-TR.resx"))
			//{
			//	resx = reader.Cast<DictionaryEntry>().ToList();
			//	var existingResource = resx.Where(r => r.Key.ToString() == key).FirstOrDefault();
			//	if (existingResource.Key == null && existingResource.Value == null) // NEW!
			//	{
			//		resx.Add(new DictionaryEntry() { Key = key, Value = value });
			//	}
			//	else // MODIFIED RESOURCE!
			//	{
			//		var modifiedResx = new DictionaryEntry()
			//		{ Key = existingResource.Key, Value = value };
			//		resx.Remove(existingResource);  // REMOVING RESOURCE!
			//		resx.Add(modifiedResx);  // AND THEN ADDING RESOURCE!
			//	}
			//}
			//using (var writer = new ResXResourceWriter(@"C:\\Users\\Lenovo\\Desktop\\Bihter\\ToricCalculator\\ToricCalculator\\Resources\\Views.Home.Form.tr-TR.resx"))
			//{
			//	resx.ForEach(r =>
			//	{
			//		// Again Adding all resource to generate with final items
			//		writer.AddResource(r.Key.ToString(), r.Value.ToString());
			//	});
			//	writer.Generate();
			//}
		}


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
		public List<LanguageTransModel> GetLanguageTrans()
		{
			List<LanguageTransModel> languageTrans = new List<LanguageTransModel>();
			using (IDbConnection con = new SqlConnection(_appSettings.ConnectionString))
			{
				if (con.State == ConnectionState.Closed) con.Open();
				{
					DynamicParameters parameters = new DynamicParameters();
					languageTrans = con.Query<LanguageTransModel>("sp_getLanguageTrans", parameters, commandType: CommandType.StoredProcedure).ToList();
				}
			}
			return languageTrans;
		}
		

		public List<LanguageTransModel> GetLanguageTranslationByKey(string key)
		{
			//var cachedKey = _cacheManager.Get<List<string>>(key);
			//if (cachedKey != null)
			//	return cachedKey;
			var dbQuery = GetLanguageTrans().Where(w=>w.Key == key).ToList();
			
			//var dbQuery = new Dictionary<string, string>();

			_cacheManager.Add(key, dbQuery, 60);

			return dbQuery;
		}
		public Dictionary<string,string> GetLocalizedValue(string key)
		{
			Dictionary<string, string> localizedValue = new Dictionary<string, string>();
			var list = GetLanguageTranslationByKey(key);
			foreach (var item in list)
			{
				localizedValue.Add(item.Culture, item.Value);
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
