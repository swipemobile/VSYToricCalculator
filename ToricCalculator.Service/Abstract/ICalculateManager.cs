using System;
using System.Collections.Generic;
using System.Text;
using ToricCalculator.Service.Model;
using ToricCalculator.Service.ResponseModel;

namespace ToricCalculator.Service.Abstract
{
	public interface ICalculateManager
	{
		ServiceResponse<FormSecrenModel> GetFormScreen();
	}
}
