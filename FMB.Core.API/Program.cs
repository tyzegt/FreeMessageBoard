using FMB.Services.Comments;
using FMB.Services.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FMB.Core.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IConfiguration configuration = builder.Configuration;
        var services = builder.Services;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<PostsContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostsContext")));
        services.AddDbContext<CommentsContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CommentsContext")));
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

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}