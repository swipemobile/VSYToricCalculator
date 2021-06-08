using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToricCalculator.Service.Model;

namespace ToricCalculator.Service.Abstract
{
    public interface IEmailService
    {
        void SendEmail(Message message);
        Task SenEmail2(Message message);
        void SendEmail3(Message message);
    }
}
