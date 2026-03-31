using MySql.Data.MySqlClient;
using Controller;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


Rotas.MapearRotas(app);
app.Run();
