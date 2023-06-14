using System;
using System.Net;
using System.Net.Mail;

namespace ThisIsMyWar.COM
{
    public class EmailSender
    {
        //发送邮件
        private string smtpServer;
        private int smtpPort;
        private string senderEmail;
        private string senderPassword;

        public EmailSender(string smtpServer, int smtpPort, string senderEmail, string senderPassword)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.senderEmail = senderEmail;
            this.senderPassword = senderPassword;
        }

        public void SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                    using (MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail))
                    {
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;

                        smtpClient.Send(mailMessage);
                    }
                }

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error message: " + ex.Message);
            }
        }
    }
}
