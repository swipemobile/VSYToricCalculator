using System;
using System.Collections.Generic;
using System.Text;

namespace ToricCalculator.Service.ResponseModel
{
	public class JsonLocaliazator
	{
		public string Key { get; set; }

		//public Dictionary<string, string> LocalizedValue = new Dictionary<string, string>();
		public Dictionary<string, string> LocalizedValue { get; set; }

	}
}
/*
  [
{
"Key": "Hello",
"LocalizedValue" : {
"tr-TR": "Merhaba",
"en-US": "Hello"
}, 
 {
"Key": "Language",
"LocalizedValue" : {
"tr-TR": "Dil",
"en-US": "Language"
}
]

 var hello = langList.Find("Hello").LocalizedValue.Find("tr-TR")
 */
