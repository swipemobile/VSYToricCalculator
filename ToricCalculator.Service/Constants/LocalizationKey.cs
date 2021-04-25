using System;
using System.Collections.Generic;
using System.Text;

namespace ToricCalculator.Service.Constants
{
	public class LocalizationKey
	{
		public const string ReferenceNo = "ReferenceNo";
		public const string Phone = "Phone";


		public static string[] FormScreenKeys => new string[] { ReferenceNo, Phone };
		public static string[] FormScreen2Keys => new string[] { ReferenceNo };
	}
}
