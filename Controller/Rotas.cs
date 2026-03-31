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
                


                usuarios.Add(new Usuario
                {
                    Id = id,
                    Email = email,
                   
                });
           }
           return Results.Ok(usuarios);
        }
        );
}
};


//endpoint para criar cadastro
public static class Cadastro{
public static void NovoCadastro(this WebApplication app)
{
    app.MapPost("/cadastro", (Usuario usuario) =>
    {
           string conexao =  "server=localhost;database=OrderFlow;user=root;password=;";
           using var conn = new MySqlConnection(conexao);
           conn.Open();

           string sql = "INSERT INTO usuario (email,senha) values (@email,@senha)";
           using var cmd = new MySqlCommand(sql, conn);
           cmd.Parameters.AddWithValue("@email",usuario.Email);
           string senhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
           cmd.Parameters.AddWithValue("@senha",senhaHash);

           cmd.ExecuteNonQuery();
           return Results.Ok("Cadastro realizado com sucesso!");
    });
}

}


//endpoint para login
public static class Login
{
    public static void LoginUsuario(this WebApplication app)
    {
        app.MapPost("/login", (Usuario usuario) =>
        {
           string conexao =  "server=localhost;database=OrderFlow;user=root;password=;";
           using var conn = new MySqlConnection(conexao);
           conn.Open();

           string sql = "SELECT * FROM usuario WHERE email = @email";
           using var cmd = new MySqlCommand(sql, conn);
           cmd.Parameters.AddWithValue("@email",usuario.Email);
          
               
           using MySqlDataReader reader = cmd.ExecuteReader();  
           
           if(reader.Read())
           {
               string? senhaHash = reader["senha"]?.ToString();

             if (string.IsNullOrEmpty(senhaHash))
               return Results.Unauthorized();

              bool senhaValida = BCrypt.Net.BCrypt.Verify(usuario.Senha, senhaHash);
               if(senhaValida)
               {
                    return Results.Ok("Login bem-sucedido!");
               }
               else
               {
                    return Results.Unauthorized();
               }
           }
           else
           {
                return Results.Unauthorized();
           }
        });
    }
}