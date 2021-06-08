using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ToricCalculator.Service.Model
{

	public class Message
	{
		public List<MailboxAddress> To { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
		public string SurgeonName { get; set; }
		public string ReferenceName { get; set; }
		public string IOL { get; set; }
		public string EyeSelection { get; set; }
		public string Alignment { get; set; }
		public string ResidualAstigmatism { get; set; }
		public string GuidId { get; set; }
		public Message(IEnumerable<string> to, string subject, string content, string guidId, string referenceName, string surgeonName, string iol, string alignment, string residualAstigmatism, string eyeSelection)
		{
			To = new List<MailboxAddress>();
			To.AddRange(to.Select(s => new MailboxAddress(s)));
			Subject = subject;
			Content = content;
			GuidId = guidId;
			ReferenceName = referenceName;
			SurgeonName = surgeonName;
			IOL = iol;
			Alignment = alignment;
			ResidualAstigmatism = residualAstigmatism;
			EyeSelection = eyeSelection;

		}
	}
}
