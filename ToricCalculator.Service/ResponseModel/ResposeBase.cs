using System;
using System.Collections.Generic;
using System.Text;

namespace ToricCalculator.Service.ResponseModel
{
	public class ResposeBase
	{
		public ResposeBase()
		{
			Success = true;
			Localizations = new List<JsonLocaliazator>();
		}
		public bool Success { get; set; }
		public string Message { get; set; }
		public List<JsonLocaliazator> Localizations { get; set; }
		public void SetException(Exception ex)
		{
			Success = false;
			Message = ex.Message;
		}
	}


}
