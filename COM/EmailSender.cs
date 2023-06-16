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

        public EmailSender()
        {
            this.smtpServer = "smtp.qq.com";
            this.smtpPort =587;
            this.senderEmail = "3069448871@qq.com";
            this.senderPassword = "frjfhvhzfarsdgej";
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
            }
            catch (Exception ex)
            {
            }
        }
    }
}
