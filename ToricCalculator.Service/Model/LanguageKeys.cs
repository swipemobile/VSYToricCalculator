using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToricCalculator.Cms.Models
{
	public class LanguageKeys
	{
		public string Culture { get; set; }
		public Cultures Language { get; set; }
		public string ReferenceNo { get; set; }
		public string IOLType { get; set; }
		public string SurgeonName { get; set; }
		public string Clinic { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Country { get; set; }
		public string IOLSphericalEquivalent { get; set; }
		public string FlatK { get; set; }
		public string FlatAxis { get; set; }
		public string SteepK { get; set; }
		public string SteepAxis { get; set; }
		public string SurgicallyInducedAstigmatism { get; set; }
		public string IncisionLocation { get; set; }
		public string Cancel { get; set; }
		public string Clean { get; set; }
		public string DescSurgicallyInducedAstigmatism { get; set; }
		public string LeftEye { get; set; }
		public string NotificationSuccess { get; set; }
		public string Ok { get; set; }
		public string Results { get; set; }
		public string RightEye { get; set; }
		public string Send { get; set; }
		public string TitleEyeInformations { get; set; }
		public string TitleSurgeonInformations { get; set; }
		public string WarningK1Bigger { get; set; }
		public string WarningK2Bigger { get; set; }
		public string Calculate { get; set; }


	}
	public enum Cultures
	{
		İngilizce = 1,
		Türkçe = 2,
		Fransızca = 3,
		İspanyolca = 4,
		Almanca = 5
	}

}
