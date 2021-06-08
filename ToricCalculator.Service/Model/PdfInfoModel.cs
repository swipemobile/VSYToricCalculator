using System;
using System.Collections.Generic;
using System.Text;

namespace ToricCalculator.Service.Model
{
	public class PdfInfoModel
	{
		public int? Id { get; set; }
		public string PdfId { get; set; }
		public string SurgeonName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Clinic { get; set; }
		public string Country { get; set; }
		public string IOLType { get; set; }
	}
}
