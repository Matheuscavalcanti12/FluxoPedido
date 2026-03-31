namespace Controller;

using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
public static class Rotas
{
    public static void  Usuario(this WebApplication app)
    {
        app.MapGet("/usuario", () =>
        {
           string conexao =  "server=localhost;database=OrderFlow;user=root;password=;";
           using var conn = new MySqlConnection(conexao);
           conn.Open();

           string sql = "SELECT * FROM usuario";
           using var cmd = new MySqlCommand(sql, conn);
           using MySqlDataReader reader = cmd.ExecuteReader();  

           List<Usuario> usuarios = new List<Usuario>();
           while (reader.Read())
           {
                int id = reader.GetInt32("id");
                string email = reader["email"]?.ToString();
                string senha = reader["senha"]?.ToString();
                string role = reader["role"]?.ToString();


                usuarios.Add(new Usuario
                {
                    id = id,
                    email = email,
                    senha = senha,
                    role = role
                });
           }
           return Results.Ok(usuarios);
        }
        );
}
}
