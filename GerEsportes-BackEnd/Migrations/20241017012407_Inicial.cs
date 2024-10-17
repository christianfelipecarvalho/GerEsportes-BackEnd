using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GerEsportes_BackEnd.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "geremail",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    owner_ref = table.Column<string>(type: "text", nullable: false),
                    email_from = table.Column<string>(type: "text", nullable: false),
                    email_to = table.Column<string>(type: "text", nullable: false),
                    subject = table.Column<string>(type: "text", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    send_date_email = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    code_recover = table.Column<int>(type: "integer", nullable: false),
                    status_email = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_geremail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gerlocal",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rua = table.Column<string>(type: "text", nullable: false),
                    cidade = table.Column<string>(type: "text", nullable: false),
                    cep = table.Column<string>(type: "text", nullable: false),
                    complemento = table.Column<string>(type: "text", nullable: false),
                    numero = table.Column<string>(type: "text", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gerlocal", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gertoken",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gertoken", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gerusuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    datanascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cargo = table.Column<string>(type: "text", nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    cref = table.Column<string>(type: "text", nullable: false),
                    federacao = table.Column<string>(type: "text", nullable: false),
                    tipousuario = table.Column<int>(type: "integer", nullable: false),
                    modalidade = table.Column<int>(type: "integer", nullable: false),
                    categoria = table.Column<int>(type: "integer", nullable: false),
                    timegenero = table.Column<int>(type: "integer", nullable: false),
                    genero = table.Column<int>(type: "integer", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    cpfrg = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gerusuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Ping",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    response = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ping", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "geragenda",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    modalidade = table.Column<int>(type: "integer", nullable: false),
                    datainicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    datafim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tipoevento = table.Column<string>(type: "text", nullable: false),
                    codigolocal = table.Column<int>(type: "integer", nullable: false),
                    codigousuario = table.Column<int>(type: "integer", nullable: false),
                    datasalvamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    observacao = table.Column<string>(type: "text", nullable: true),
                    categoria = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_geragenda", x => x.id);
                    table.ForeignKey(
                        name: "FK_geragenda_gerlocal_codigolocal",
                        column: x => x.codigolocal,
                        principalTable: "gerlocal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_geragenda_gerusuario_codigousuario",
                        column: x => x.codigousuario,
                        principalTable: "gerusuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gerdocumentousuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomedocumento = table.Column<string>(type: "text", nullable: false),
                    guiddocumento = table.Column<string>(type: "text", nullable: false),
                    extensao = table.Column<string>(type: "text", nullable: false),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    imagemperfil = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gerdocumentousuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_gerdocumentousuario_gerusuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "gerusuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_geragenda_codigolocal",
                table: "geragenda",
                column: "codigolocal");

            migrationBuilder.CreateIndex(
                name: "IX_geragenda_codigousuario",
                table: "geragenda",
                column: "codigousuario");

            migrationBuilder.CreateIndex(
                name: "IX_gerdocumentousuario_usuario_id",
                table: "gerdocumentousuario",
                column: "usuario_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "geragenda");

            migrationBuilder.DropTable(
                name: "gerdocumentousuario");

            migrationBuilder.DropTable(
                name: "geremail");

            migrationBuilder.DropTable(
                name: "gertoken");

            migrationBuilder.DropTable(
                name: "Ping");

            migrationBuilder.DropTable(
                name: "gerlocal");

            migrationBuilder.DropTable(
                name: "gerusuario");
        }
    }
}
