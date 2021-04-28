using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToricCalculator.Service.Abstract;
using ToricCalculator.Service.Constants;
using ToricCalculator.Service.CustomException;
using ToricCalculator.Service.Model;
using ToricCalculator.Service.ResponseModel;

namespace ToricCalculator.Service.Concrate
{
	public class CalculateManager : ICalculateManager
	{
		private readonly ILanguageManager _languageManager;
		public CalculateManager(ILanguageManager languageManager)
		{
			_languageManager = languageManager;
		}

		public ServiceResponse<FormSecrenModel> GetFormScreen()
		{
			//throw new ApiException("Hata mesajı");

			Dictionary<string, string> localizedValue = new Dictionary<string, string>();
			localizedValue.Add("tr-TR", "Kayıt Bulunamadı");
			localizedValue.Add("en-US", "Not Found");
			var jsonLoc = new JsonLocaliazator() { Key = "NotFound", LocalizedValue = localizedValue };

			FormSecrenModel formSecrenModel = new FormSecrenModel();
			var response = new ServiceResponse<FormSecrenModel>()
			{
				Value= formSecrenModel
			};
			response.Localizations.AddRange(_languageManager.GetJsonLocaliazators(LocalizationKey.FormScreenKeys));
			return response;
		}

		

	}
}
