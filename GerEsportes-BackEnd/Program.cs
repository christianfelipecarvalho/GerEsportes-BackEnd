using GerEsportes_BackEnd;
using GerEsportes_BackEnd.Aplicacao.AplicAgendas;
using GerEsportes_BackEnd.Aplicacao.AplicDashboards;
using GerEsportes_BackEnd.Aplicacao.AplicEmails;
using GerEsportes_BackEnd.Aplicacao.AplicLocais;
using GerEsportes_BackEnd.Aplicacao.AplicLogins;
using GerEsportes_BackEnd.Aplicacao.AplicUsuarios;
using GerEsportes_BackEnd.Aplicacao.Servicos;
using GerEsportes_BackEnd.Infra;
using GerEsportes_BackEnd.Repositorios.Agendas;
using GerEsportes_BackEnd.Repositorios.Emails;
using GerEsportes_BackEnd.Repositorios.Locais;
using GerEsportes_BackEnd.Repositorios.Pings;
using GerEsportes_BackEnd.Repositorios.Usuarios;
using GerEsportes_BackEnd.Repositorios.Usuarios.Documentos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicione serviços ao contêiner.
builder.Services.AddControllers();

// Adicione os serviços necessários ao contêiner.
builder.Services.AddTransient<IServicoEmail, ServicoEmail>();
builder.Services.AddHostedService<ServicoPing>();
builder.Services.AddTransient<IServicoToken, ServicoToken>();

//Adiciona repositórios
builder.Services.AddTransient<IRepEmail, RepEmail>();
builder.Services.AddTransient<IRepPing, RepPing>();
builder.Services.AddTransient<IRepUsuario, RepUsuario>();
builder.Services.AddTransient<IRepDocumentoUsuario, RepDocumentoUsuario>();
builder.Services.AddTransient<IRepLocal, RepLocal>();
builder.Services.AddTransient<IRepAgenda, RepAgenda>();

//Adiciona Aplicações
builder.Services.AddTransient<IAplicUsuario, AplicUsuario>();
builder.Services.AddTransient<IAplicEmail, AplicEmail>();
builder.Services.AddTransient<IAplicLogin, AplicLogin>();
builder.Services.AddTransient<IAplicLocal, AplicLocal>();
builder.Services.AddTransient<IAplicAgenda, AplicAgenda>();
builder.Services.AddTransient<IAplicDashboard, AplicDashboard>();


// Adicione a autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationSettingsToken.PrivateKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// Adicione a política CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Adicione o serviço de contexto do banco de dados usando as configurações do appsettings.json
builder.Services.AddDbContext<Contexto>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GerEsportes-API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
            new string[]{}   
        }
    });
});

var app = builder.Build();

// Configure o pipeline de solicitação HTTP
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseCors();

app.UseAuthentication(); // <- UseAuthentication() deve vir antes de UseAuthorization()
app.UseAuthorization();

app.MapControllers();

app.Run();
