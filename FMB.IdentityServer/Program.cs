using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FMB.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            SeedData.EnsureSeedData(defaultConnectionString);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var assembly = typeof(Program).Assembly.GetName().Name;

            builder.Services.AddDbContext<IdentityContext>(options =>
                options.UseNpgsql(defaultConnectionString, b => b.MigrationsAssembly(assembly)));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

            builder.Services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(defaultConnectionString, opt => opt.MigrationsAssembly(assembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(defaultConnectionString, opt => opt.MigrationsAssembly(assembly));
                })
                .AddDeveloperSigningCredential();



            var app = builder.Build();

            app.UseIdentityServer();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}