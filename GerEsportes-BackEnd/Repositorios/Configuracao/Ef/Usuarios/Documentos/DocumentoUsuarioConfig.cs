using GerEsportes_BackEnd.Dominios.Usuarios.Documentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Usuarios.Documentos
{
    public class DocumentoUsuarioConfig : IEntityTypeConfiguration<DocumentoUsuario>
    {
        public void Configure(EntityTypeBuilder<DocumentoUsuario> builder)
        {
            builder.ToTable("gerdocumentousuario");

            builder.HasKey(c => new { c.Id });

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.GuidDocumento)
                .HasColumnName("guiddocumento");

            builder.Property(x => x.NomeDocumento)
                .HasColumnName("nomedocumento");

            builder.Property(x => x.Extensao)
                .HasColumnName("extensao");

            builder.Property(x => x.ImagemPerfil)
               .HasColumnName("imagemperfil");

            builder.Property(x => x.CodigoUsuario)
                .HasColumnName("usuario_id");

            builder.HasOne(x => x.Usuario)
            .WithMany(u => u.DocumentoUsuario)
            .HasForeignKey(f => f.CodigoUsuario);
        }
    }
}
