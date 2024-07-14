using GerEsportes_BackEnd.Dominios.Emails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Emails
{
    public class EmailConfig : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("geremail");

            builder.HasKey(c => new { c.Id });

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.OwnerRef)
                .HasColumnName("owner_ref");

            builder.Property(x => x.EmailFrom)
                .HasColumnName("email_from");

            builder.Property(x => x.EmailTo)
                .HasColumnName("email_to");

            builder.Property(x => x.Subject)
                .HasColumnName("subject");

            builder.Property(x => x.Text)
                .HasColumnName("text");

            builder.Property(x => x.SendDateEmail)
                .HasColumnName("send_date_email");

            builder.Property(x => x.CodeRecover)
                .HasColumnName("code_recover");

            builder.Property(x => x.EnumStatusEmail)
                .HasColumnName("status_email");
        }
    }
}
