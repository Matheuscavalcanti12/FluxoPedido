using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
   string message = "Hello, World!";
   return message;
}
);
app.Run();
