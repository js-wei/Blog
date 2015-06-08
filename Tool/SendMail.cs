using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Tool
{
    public class SendMail
    {
        #region 属性 smtpserver  ， email  ， password

        private string _smtpserver = "smtp.163.com";
        private string _email = "gsjwwks@163.com"; //注册自己的邮箱
        private string _emailpassword = "melon455";

        public string SmtpServer
        {
            get { return _smtpserver; }
            set { _smtpserver = value; }
        }
        public string Email { get { return _email; } set { _email = value; } }
        public string EmailPassword { get { return _emailpassword; } set { _emailpassword = value; } }

        #endregion

        public void SendEmail(string strTo, string strSubject, string strBody)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(Email, strTo, strSubject, strBody);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            SendSMTPEMail(message);

        }
        private void SendSMTPEMail(MailMessage message)
        {
            try
            {
                SmtpClient client = new SmtpClient(SmtpServer);
                client.UseDefaultCredentials = true;
                client.Credentials = new System.Net.NetworkCredential(Email, EmailPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
