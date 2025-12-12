
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading.Tasks;

namespace Host
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("defaultConnectionString");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            
            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

           using var scope = app.Services.CreateScope();
           var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
                await DbInitializer.SeedData(context);
            }
            catch(Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();   
                logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            }

            app.Run();
        }
    }
}
