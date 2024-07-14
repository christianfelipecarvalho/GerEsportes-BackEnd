using GerEsportes_BackEnd.Dominios.Agendas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Agendas
{
    public class AgendaConfig : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.ToTable("geragenda");

            builder.HasKey(c => new { c.Id });

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Modalidade)
                .HasColumnName("modalidade");

            builder.Property(x => x.DataInicio)
               .HasColumnName("datainicio");

            builder.Property(x => x.DataFim)
               .HasColumnName("datafim");

            builder.Property(x => x.DataSalvamento)
               .HasColumnName("datasalvamento");

            builder.Property(x => x.Titulo)
               .HasColumnName("titulo");

            builder.Property(x => x.CodigoLocal)
               .HasColumnName("codigolocal");

            builder.Property(x => x.CodigoUsuario)
               .HasColumnName("codigousuario");

            builder.Property(x => x.TipoEvento)
               .HasColumnName("tipoevento");

            builder.Property(x => x.Categoria)
               .HasColumnName("categoria");

            builder.Property(x => x.Obs)
               .HasColumnName("observacao");


            builder.HasOne(x => x.Usuario)
                        .WithMany()
                        .HasForeignKey(c => c.CodigoUsuario);

            builder.HasOne(x => x.Local)
                        .WithMany()
                        .HasForeignKey(c => c.CodigoLocal);
        }
    }
}

