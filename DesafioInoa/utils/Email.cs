using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace DesafioInoa.utils
{
	public class EmailHandler
	{
		
		private readonly float sellPrice;
		private readonly float buyPrice;
		private readonly string ticker;

		public EmailHandler(string ticker, float sellPrice, float buyPrice)
		{
			this.sellPrice = sellPrice;
			this.buyPrice = buyPrice;
			this.ticker = ticker;
		}

		private void sendMail(string subject, string message)
        {
			Console.WriteLine(message);

			var settings = ConfigurationManager.AppSettings;

			var fromAddress = new MailAddress(settings["from"]);
			var toAddress = new MailAddress(settings["to"]);

			SmtpClient smtp = new SmtpClient
			{
				Host = settings["smtpClient"],
				Port = Int32.Parse(settings["smtpPort"]),
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, settings["password"])

			};

			using (var mailMessage = new MailMessage(fromAddress, toAddress)
			{
				Subject = subject,
				Body = message
			})
            {
				smtp.Send(mailMessage);

            };

			Console.WriteLine("Email Sent!");
        }

		public void notifyToSell(float currentPrice)
        {
			string subject = "Alert! Time to sell!";
			string message = "The ticker " + this.ticker + " has riched the selling point " + this.sellPrice + "! It's price is " + currentPrice;

			sendMail(subject, message);
        }

		public void notifyToBuy(float currentPrice)
        {
			string subject = "Alert! Time to buy!";
			string message = "The ticker " + this.ticker + " has riched the buying point " + this.buyPrice + "! It's price is " + currentPrice;

			sendMail(subject, message);
		}
	}
}

