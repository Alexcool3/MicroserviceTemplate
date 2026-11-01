#if (useEf)
using AuthService.Data;
#endif
using Scalar.AspNetCore;

namespace AuthService
{
    public partial class Program
    {
        private static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.AddSharedKernel();

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            #if (useEf)

            builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

            #endif
            builder.Services.Scan(scan => scan
                .FromAssemblyOf<Program>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Handler")))
                .AsSelf()
                .WithScopedLifetime());

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseSharedKernel();

            app.Run();
        }
    }
}