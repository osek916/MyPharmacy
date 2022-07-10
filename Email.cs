using MyPharmacy.Entities;
using System.ComponentModel;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyPharmacy
{
    public class Email
    {
        private SmtpClient _smtp;
        private MailMessage _mail;

        private string _hostSmpt;
        private bool _enableSsl;
        private int _port;
        private string _senderEmail;
        private string _senderEmailPassword;
        private string _senderName;

        public Email(EmailParams emailParams)
        {
            _hostSmpt = emailParams.HostSmpt;
            _senderEmail = emailParams.SenderEmail;
            _senderEmailPassword = emailParams.SenderEmailPassword;
            _senderName = emailParams.SenderName;
            _enableSsl = emailParams.EnableSsl;            
            _port = emailParams.Port;
        }

        public async Task Send(string subject, string body, string to)
        {
            _mail = new MailMessage();
            _mail.Subject = subject;
            _mail.Body = body;
            _mail.From = new MailAddress(_senderEmail, _senderName);
            _mail.To.Add(new MailAddress(to));
            _mail.BodyEncoding = System.Text.Encoding.UTF8;
            _mail.SubjectEncoding = System.Text.Encoding.UTF8;

            _smtp = new SmtpClient
            {
                Host = _hostSmpt,
                EnableSsl = _enableSsl,
                Port = _port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(_senderEmail, _senderEmailPassword)
            };
            _smtp.SendCompleted += OnSendCompleted;

            await _smtp.SendMailAsync(_mail);
        }

        private void OnSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _smtp.Dispose();
            _mail.Dispose();
        }

    }
}
