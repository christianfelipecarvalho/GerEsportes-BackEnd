using GerEsportes_BackEnd.Dominios.Pings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Pings
{
    public class PingEntityConfig : IEntityTypeConfiguration<PingEntity>
    {
        public void Configure(EntityTypeBuilder<PingEntity> builder)
        {

            builder.ToTable("gerping");

            builder.HasKey(c => new { c.Id });

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Reponse)
                .HasColumnName("response");
        }
    }
}
