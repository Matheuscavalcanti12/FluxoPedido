namespace Controller;

public static class Rotas
{
    public static void  MapearRotas(WebApplication app)
    {
        app.MapGet("/", () =>
        {
            string message = "Hello, World!";
            return message;
        }
        );
}
}
