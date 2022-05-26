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
		private MailAddress fromAddress;
		private MailAddress toAddress;
		private string password;
		private string smtpClient;
		private int smtpPort;

		public EmailHandler(string ticker, float sellPrice, float buyPrice)
		{
			this.sellPrice = sellPrice;
			this.buyPrice = buyPrice;
			this.ticker = ticker;

			validateConfig();
		}

		 private void sendMail(string subject, string message)
        {
			Console.WriteLine(message);


			SmtpClient smtp = new()
            {
				Host = this.smtpClient,
				Port = this.smtpPort,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(this.fromAddress.Address, this.password)

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

		private void validateConfig()
        {
			var settings = ConfigurationManager.AppSettings;

			if (
				settings["from"] is null || settings["from"].Length == 0 ||
				settings["to"] is null || settings["to"].Length == 0 ||
				settings["password"] is null || settings["password"].Length == 0 ||
				settings["smtpClient"] is null || settings["smtpClient"].Length == 0 ||
				settings["smtpPort"] is null || settings["smtpPort"].Length == 0
				) throw new ArgumentException("Missing parameters in settings file!");


			this.fromAddress = new MailAddress(settings["from"]);
			this.toAddress = new MailAddress(settings["to"]);
			this.password = settings["password"];
			this.smtpClient = settings["smtpClient"];
			this.smtpPort = Int32.Parse(settings["smtpPort"]);
        }
	}
}

