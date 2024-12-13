namespace ApiConsalud
{
    using System.Configuration;
    using Utilidades;
    public class Program
    {
        //public Program(IConfiguration configuration)
        //{

        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<ApiKeyConfiguration>(builder.Configuration.GetSection("ApiKey"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

                app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api/Facturas"),
                appBuilder => appBuilder.UseMiddleware<ApiKeyMiddleware>()
                );

          

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
    }
}
