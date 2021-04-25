using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ToricCalculator.Service.Abstract;
using ToricCalculator.Service.Model;
using ToricCalculator.Service.ResponseModel;

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
