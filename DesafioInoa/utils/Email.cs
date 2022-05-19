using System.Configuration;
using System.Net.Mail;

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

			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(settings["from"]);
			mail.To.Add(settings["to"]);
			mail.Subject = subject;
			mail.Body = message;

			SmtpClient smtp = generateSmtpClient();

			smtp.Send(mail);
        }

		private SmtpClient generateSmtpClient()
        {
			var settings = ConfigurationManager.AppSettings;

			SmtpClient smtp = new SmtpClient(settings["smtpClient"]);
			smtp.Port = int.Parse(settings["smtpPort"]);
			smtp.Credentials = new System.Net.NetworkCredential(settings["from"], settings["password"]);
			smtp.EnableSsl = true;

			return smtp;
        }

		public void notifyToSell(float currentPrice)
        {
			string subject = "Alert! Time to sell!";
			string message = "The ticker" + this.ticker + "has riched the selling point " + this.sellPrice + "! It's price is " + currentPrice;

			sendMail(subject, message);
        }

		public void notifyToBuy(float currentPrice)
        {
			string subject = "Alert! Time to buy!";
			string message = "The ticker" + this.ticker + "has riched the buying point " + this.buyPrice + "! It's price is " + currentPrice;

			sendMail(subject, message);
		}
	}
}

