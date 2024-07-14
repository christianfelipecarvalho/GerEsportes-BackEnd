using GerEsportes_BackEnd.Dominios.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Usuarios
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("gerusuario");

            builder.HasKey(c => new { c.Id });

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Nome)
                .HasColumnName("nome");

            builder.Property(x => x.Senha)
                .HasColumnName("senha");

            builder.Property(x => x.Email)
                .HasColumnName("email");

            builder.Property(x => x.DataNascimento)
                .HasColumnName("datanascimento")
                  .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); 

            builder.Property(x => x.Cargo)
                .HasColumnName("cargo");

            builder.Property(x => x.Telefone)
                .HasColumnName("telefone");

            builder.Property(x => x.Cref)
                .HasColumnName("cref");

            builder.Property(x => x.Ativo)
                .HasColumnName("ativo");

            builder.Property(x => x.Categoria)
                .HasColumnName("categoria");

            builder.Property(x => x.Federacao)
                .HasColumnName("federacao");

            builder.Property(x => x.Modalidade)
               .HasColumnName("modalidade");

            builder.Property(x => x.TipoUsuario)
                .HasColumnName("tipousuario");

            builder.Property(x => x.CpfRg)
                .HasColumnName("cpfrg");

            builder.Property(x => x.Genero)
                .HasColumnName("genero");

            builder.Property(x => x.Time)
                .HasColumnName("timegenero");
        }
    }
}
