using GerEsportes_BackEnd.Dominios.Locais;
using GerEsportes_BackEnd.Dominios.Pings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Locais
{
    public class LocalConfig : IEntityTypeConfiguration<Local>
    {
        public void Configure(EntityTypeBuilder<Local> builder)
        {

            builder.ToTable("gerlocal");

            builder.HasKey(c => new { c.Id });

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Cidade)
              .HasColumnName("cidade");

            builder.Property(x => x.Rua)
              .HasColumnName("rua");

            builder.Property(x => x.Cep)
              .HasColumnName("cep");

            builder.Property(x => x.Complemento)
              .HasColumnName("complemento");

            builder.Property(x => x.Numero)
              .HasColumnName("numero");

            builder.Property(x => x.Ativo)
             .HasColumnName("ativo");

            builder.Property(x => x.Descricao)
            .HasColumnName("descricao");
        }
    }
}
