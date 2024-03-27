using IOT.Application.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Contract.Email
{
	public interface IEmailSender
	{
		Task<bool> SendEmail(EmailMessage email);
	}
}
