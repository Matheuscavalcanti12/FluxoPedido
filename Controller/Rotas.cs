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

//criando endpoint para adicionar produtos
public static class ProdutoRotas
{
   public static void AdicionarProduto(this WebApplication app)
     {
        app.MapPost("/produto", (Produto produto) =>
         { string conexao =  "server=localhost;database=OrderFlow;user=root;password=;";
           using var conn = new MySqlConnection(conexao);
           conn.Open();

           string sql = "INSERT INTO produto (desc_produto, valor, imagem) values (@desc_produto, @valor, @imagem)";
           using var cmd = new MySqlCommand(sql, conn);
           
           cmd.Parameters.AddWithValue("@desc_produto",produto.Desc_produto);
           cmd.Parameters.AddWithValue("@valor",produto.Preco);
           cmd.Parameters.AddWithValue("@imagem",produto.Imagem);

            int rows = cmd.ExecuteNonQuery();
            return Results.Ok(rows); 

            return Results.Ok("Produto adicionado com sucesso!");

          
    });
     
    }
}

/*endpoint get para listar os produtos gerais(Usuario consegue ver todos os produtos disponiveis
ou, ao clique do botao Produtos, lista todos os produtos da loja) */
public static class Listar{
    public static void ListarProduto(this WebApplication app)
    {
        app.MapGet("/listarProdutos", () =>
        {
              string conexao  = "server=localhost;database=OrderFlow;user=root;password=;";
              using var conn = new MySqlConnection (conexao);
              conn.Open();

              string query = "SELECT id_produto,desc_produto, valor, imagem FROM produto";
              
              using var cmd = new MySqlCommand (query,conn);
              using MySqlDataReader reader = cmd.ExecuteReader();  

              List<Produto> list = new List<Produto>();
              
               while (reader.Read())
                {
                      int id = reader.GetInt32("id_produto");
                       string desc_produto = reader["desc_produto"].ToString();
                       decimal valor = reader.GetDecimal("valor");
                       string imagem = reader["imagem"].ToString();
                

                list.Add( new Produto{
                 Id = id,
                 Desc_produto = desc_produto,
                 Preco = valor,
                 Imagem = imagem

                 });
                }
              return Results.Ok(list); 
           });

    }
 }


/*endpoint criado para quando o usuario clicar no produto,
 a pagina abrir aquele produto especifico*/

 public static class Item
{
    public static void ItemProduto(this WebApplication app)
    {
        app.MapGet("/produto/{id}", (int id) =>
        {
            string conexao = "server=localhost;database=OrderFlow;user=root;password=;";
            using var conn = new MySqlConnection (conexao);
            conn.Open();

            string query = "SELECT id_produto,desc_produto,valor,imagem from produto where id_produto= @id_produto";
            using var cmd = new MySqlCommand(query,conn);

            
            /*o banco vai retornar apenas um produto correspondente ao ID do produto.
            @id_produto → nome do parâmetro na query SQL.
            id → valor que você quer usar no lugar desse parâmetro.
            Isso funciona tanto para SELECT quanto para INSERT, UPDATE ou DELETE.
            */
            cmd.Parameters.AddWithValue("@id_produto", id);

            using MySqlDataReader reader = cmd.ExecuteReader();  

          


            if (reader.Read())
            {
                //o endpoint envia exatamente os dados do produto solicitado.
                //como eu quero que eu veja um produto especifico,
                //eu crio esse objeto passando todos os dados do produto que o banco me retorna
                
           
              
                var produto = new Produto
                {
                    Id = reader.GetInt32("id_produto"),
                    Desc_produto = reader["desc_produto"].ToString(),
                    Preco = reader.GetDecimal("valor"),
                    Imagem = reader["imagem"].ToString()
                };
                
                   return Results.Ok(produto); 
             }
          return Results.NotFound();
        });
    }
}


//endpoint criando pedido

public static class Pedidos
{
    public static void CriarPedido(this WebApplication app)
    {
        app.MapPost("/pedido", (Pedido pedido) =>
        {

            /*endpoint criado para criar pedido do usuario
               o usuario recebe uma Foreign Key do id na tabela pedido
               essa FK é o id do usuario que fez o pedido, ou seja,
               o pedido tem um id_usuario que referencia o id do usuario na tabela usuario.
            */
            string conexao = "server=localhost;database=OrderFlow;user=root;password=;";
            using var conn = new MySqlConnection (conexao);
            conn.Open();

            string sql = "INSERT INTO pedido (id_usuario) values (@id_usuario)";
            using var cmd = new MySqlCommand(sql, conn);
           
           
           cmd.Parameters.AddWithValue("@id_usuario",pedido.Id_usuario);

            int rows = cmd.ExecuteNonQuery();
            return Results.Ok(rows); 

            
        });
    }
}

//endpoint para adicionar itens ao pedid(Adicionar ao carrinho)

public static class ItensPedido
{
    public static void AdicionarItemPedido(this WebApplication app)
    {
        app.MapPost("/pedido/item", (ItemPedido item) =>
        {
            string conexao = "server=localhost;database=OrderFlow;user=root;password=;";
            using var conn = new MySqlConnection (conexao);
            conn.Open();

            string sql = "INSERT INTO item_pedido (id_pedido, id_produto, quantidade) values (@id_pedido, @id_produto, @quantidade)";
            using var cmd = new MySqlCommand(sql, conn);
           
           
           cmd.Parameters.AddWithValue("@id_pedido",item.Id_pedido);
           cmd.Parameters.AddWithValue("@id_produto",item.Id_produto);
           cmd.Parameters.AddWithValue("@quantidade",item.Quantidade);

            int rows = cmd.ExecuteNonQuery();
            return Results.Ok(rows); 

            
        });
    }
}

