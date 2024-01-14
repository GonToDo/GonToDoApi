using GonToDoApi.Core;
using GonToDoApi.Core.DataBase;
using GonToDoApi.Services;

namespace GonToDoApi;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<DataBaseSettings>(
            builder.Configuration.GetSection("DataBase")
        );

        builder.Services.AddTransient<AccountService>();
        builder.Services.AddTransient<CategoryService>();
        builder.Services.AddTransient<TaskService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        {
            Log.Warning("IsDevelopment");

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}