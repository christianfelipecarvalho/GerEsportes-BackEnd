using GerEsportes_BackEnd.Dominios.Agendas;
using GerEsportes_BackEnd.Dominios.Emails;
using GerEsportes_BackEnd.Dominios.Locais;
using GerEsportes_BackEnd.Dominios.Pings;
using GerEsportes_BackEnd.Dominios.Tokens;
using GerEsportes_BackEnd.Dominios.Usuarios;
using GerEsportes_BackEnd.Dominios.Usuarios.Documentos;
using GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Agendas;
using GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Emails;
using GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Locais;
using GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Pings;
using GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Tokens;
using GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Usuarios;
using GerEsportes_BackEnd.Repositorios.Configuracao.Ef.Usuarios.Documentos;
using Microsoft.EntityFrameworkCore;

namespace GerEsportes_BackEnd
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options): base(options)
        {}

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Email> Email { get; set; }

        public DbSet<PingEntity> Ping { get; set; }

        public DbSet<DocumentoUsuario> DocumentoUsuario { get; set; }

        public DbSet<Agenda> Agenda { get; set; }

        public DbSet<Local> Local { get; set; }

        public DbSet<TokenAutenticationGer> Token { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);

            modelBuilder.ApplyConfiguration<Email>(new EmailConfig());

            modelBuilder.ApplyConfiguration<Usuario>(new UsuarioConfig());

            modelBuilder.ApplyConfiguration<PingEntity>(new PingEntityConfig());

            modelBuilder.ApplyConfiguration<DocumentoUsuario>(new DocumentoUsuarioConfig());

            modelBuilder.ApplyConfiguration<Local>(new LocalConfig());

            modelBuilder.ApplyConfiguration<Agenda>(new AgendaConfig());

            modelBuilder.ApplyConfiguration<TokenAutenticationGer>(new TokenAutenticationGerConfig());
        }
    }
}
