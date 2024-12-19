namespace webServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            CreateController(app);
            
            app.Run();
        }
        private static void CreateController(WebApplication app)
        {
            var route = new RouteEnumerator(app.Configuration);
            app.MapGet(route.Connect, (HttpContext httpContext) =>
            {

            });

            app.MapGet(route.Home, (HttpContext httpContext) =>
            {
                return "Hello world";
            });
        }
    }
}
