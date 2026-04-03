using MySql.Data.MySqlClient;
using Controller;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Usuario();
app.NovoCadastro();
app.LoginUsuario();
app.AdicionarProduto();
app.ListarProduto();
app.ItemProduto();
app.Run();
