using GerEsportes_BackEnd.Dominios.Emails;
using GerEsportes_BackEnd.Dominios.Enuns;
using System.Net;
using System.Net.Mail;

namespace GerEsportes_BackEnd.Aplicacao.Servicos
{
    public class ServicoEmail : IServicoEmail
    {

        public void EnviarEmail(Email emailEnviar)
        {
            MailMessage email = new MailMessage();
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 60 * 60;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("noreplygerenciadoratletas@gmail.com", "cxzhatlywsoobcmr");

                email.From = new MailAddress("noreplygerenciadoratletas@gmail.com", emailEnviar.Subject);
                email.Body = emailEnviar.Text;
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;
                email.Subject = emailEnviar.Subject;
                email.To.Add(emailEnviar.EmailTo);

                smtpClient.Send(email);

                emailEnviar.EnumStatusEmail = EnumStatusEmail.SENT;

            }
            catch (SmtpException ex)
            {
                emailEnviar.EnumStatusEmail = EnumStatusEmail.ERROR;
                throw new Exception("Erro ao enviar email: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                emailEnviar.EnumStatusEmail = EnumStatusEmail.ERROR;
                throw new Exception("Erro ao enviar email: " + ex.Message, ex);
            } 
        }
    }
}
