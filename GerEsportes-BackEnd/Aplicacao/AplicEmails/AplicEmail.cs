using GerEsportes_BackEnd.Aplicacao.Servicos;
using GerEsportes_BackEnd.Dominios.Dtos;
using GerEsportes_BackEnd.Dominios.Emails;
using GerEsportes_BackEnd.Repositorios.Emails;
using GerEsportes_BackEnd.Repositorios.Usuarios;

namespace GerEsportes_BackEnd.Aplicacao.AplicEmails
{
    public class AplicEmail : IAplicEmail
    {

        private readonly IServicoEmail _servicoEmail;
        private readonly IRepEmail _repEmail;
        private readonly IRepUsuario _repUsuario;

        public AplicEmail(IServicoEmail servicoEmail, IRepEmail repEmail, IRepUsuario repUsuario)
        {
            _servicoEmail = servicoEmail;
            _repEmail = repEmail;
            _repUsuario = repUsuario;
        }

        public void EsqueciMinhaSenha(EmailDto dto)
        {
            Email email = new Email();

            var usuario = _repUsuario.RecuperarTodos().Where(p => p.Email.Equals(dto.EmailTo.ToLower())).First();

            if (usuario == null)
              throw new Exception("Usuário com o E-mail " + usuario.Email.ToLower() + " não foi encontrado!");

            Random geradorAleatorio = new Random();
            int numeroAleatorioEntre0e100 = geradorAleatorio.Next(1000, 9999);

            email.Subject = "Equipe de contas GerEsportes";
            email.EmailFrom = "noreplygerenciadoratletas@gmail.com";
            email.EmailTo = dto.EmailTo.ToLower();
            email.OwnerRef = "Equipe de contas do Gerenciador de Atletas";

            email.Text = "<p>Olá " + dto.EmailTo.ToLower() + "!</p>"
                       + "<p>Nós recebemos uma solicitação para um código de uso único para sua conta do Gerenciador de Atletas.</p>"
                       + "<p>Seu código de uso único é: <strong>" + numeroAleatorioEntre0e100.ToString() + "</strong></p>"
                       + "<p>Se você não solicitou este código, poderá ignorar com segurança este E-mail. Outra pessoa pode ter digitado seu endereço de E-mail por engano.</p>"
                       + "<p>Obrigado,</p>"
                        + "<p>Equipe Gerenciador de Atletas!</p>";


            email.CodeRecover = numeroAleatorioEntre0e100;

            email.EmailTo = dto.EmailTo;
            email.SendDateEmail = DateTime.Now.ToUniversalTime();

            _servicoEmail.EnviarEmail(email);
            _repEmail.InserirEmail(email);
        }

        public int ValidarCodigoRecuperacao(ValidarRecuperacaoDto dto)
        {
            var email = _repEmail.RecuperarTodos().Find(p => p.EmailTo == dto.Email.ToLower());

            var usuario = _repUsuario.RecuperarTodos().Find(p => p.Email == dto.Email.ToLower());

            if (dto.CodigoRecuperacao != email.CodeRecover)
                throw new Exception("Usuário ou código incorreto!");

            _repEmail.Excluir(email);

            return usuario.Id;
        }
    }
}
