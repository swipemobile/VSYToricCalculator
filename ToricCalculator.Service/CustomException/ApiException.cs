using System;
using System.Collections.Generic;
using System.Text;

namespace ToricCalculator.Service.CustomException
{
	public class ApiException : Exception
	{
		public ApiException(string message, Exception ex) : base(message, ex)
		{
			
		}
		public ApiException(string message) : base(message)
		{

		}
	}
}
