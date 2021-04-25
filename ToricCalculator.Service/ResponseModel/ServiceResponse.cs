using System;
using System.Collections.Generic;
using System.Text;

namespace ToricCalculator.Service.ResponseModel
{
	public class ServiceResponse<T> : ResposeBase
	{
		public T Value { get; set; }
	}
}
