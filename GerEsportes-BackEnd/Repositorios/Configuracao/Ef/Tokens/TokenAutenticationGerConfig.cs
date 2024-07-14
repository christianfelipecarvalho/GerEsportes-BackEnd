using GerEsportes_BackEnd.Dominios.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Tokens
{
    public class TokenAutenticationGerConfig : IEntityTypeConfiguration<TokenAutenticationGer>
    {
        public void Configure(EntityTypeBuilder<TokenAutenticationGer> builder)
        {

            builder.ToTable("gertoken");

            builder.HasKey(c => new { c.Id });

            builder.Property(x => x.Id)
                .HasColumnName("id");
        }
    }
}
