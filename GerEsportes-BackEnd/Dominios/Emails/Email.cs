using GerEsportes_BackEnd.Dominios.Enuns;

namespace GerEsportes_BackEnd.Dominios.Emails
{
    public class Email
    {
        public int Id { get; set; }
        public string OwnerRef { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime SendDateEmail { get; set; }
        public int CodeRecover { get; set; }
        public EnumStatusEmail EnumStatusEmail { get; set; }
    }
}
