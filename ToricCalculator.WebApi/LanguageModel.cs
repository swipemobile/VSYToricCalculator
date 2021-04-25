using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToricCalculator.WebApi
{
	public class LanguageModel
	{
		public List<JsonLocalization> Localizations { get; set; }
	}
	public class JsonLocalization
	{
		public string Key { get; set; }
		public Dictionary<string, string> LocalizedValue = new Dictionary<string, string>();
	}

}
