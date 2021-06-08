using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToricCalculator.Models
{
	public class PdfModel
	{
		public string GuidId { get; set; }
		public string ReferenceNo { get; set; }
		public string SurgeonName { get; set; }
		public string IOLType { get; set; }
		public string Clinic { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Country { get; set; }
		public string IOLSphericalEquivalent { get; set; }
		public string FlatAxis { get; set; }
		public string SteepK { get; set; }
		public string SteepAxis { get; set; }
		public string SurgicallyInducedAstigmatism { get; set; }
		public string IncisionLocation { get; set; }
		public string FlatK { get; set; }
		public string Recipient { get; set; }
		public string MailContent { get; set; }
		public string EyeSelection { get; set; }
		public string Culture { get; set; }
	}
}