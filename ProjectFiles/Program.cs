using MySql.Data.MySqlClient;
using Controller;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

//converte jwt em bytes 
//Aqui é autenticação do token para o sistema, onde é necessário uma chave secreta
// para validar o token, e as regras de validação do token, como o emissor, o público,
// a validade e a chave de assinatura.
var key = Encoding.UTF8.GetBytes("minha_chave_super_secreta_3112_123456789123456789");
//fala pro sistema o tipo de autenticação
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        //permite que o uso da rota sem https
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        //regras de validação 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "meuSistema",
            ValidAudience = "meuSistema",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:8080",
        "http://192.168.130.46:8080",
        "http://192.168.56.1:8080",
        "http://172.21.80.1:8080",
        "http://192.168.3.178:8080"
        
        )
              .AllowAnyHeader()
              .AllowAnyMethod();
        
    });
});

var app = builder.Build();
app.UseCors("CorsPolicy");

app.Usuario();
app.NovoCadastro();
app.LoginUsuario();
app.AdicionarProduto();
app.ListarProduto();
app.ItemProduto();
app.CriarPedido();
app.AdicionarItemPedido();
app.VerItensPedido();
app.AtualizarQuantidadeItem();
app.RemoverItemPedido();
app.DeletarProduto();
app.Run();
