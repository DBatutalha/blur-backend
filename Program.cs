using BlurApi.Data;
using BlurApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
    policy =>
    {
        policy.WithOrigins([
                "http://localhost:5173",
                "http://localhost:3000",
            ])
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DatabaseSeederService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeederService>();
    try
    {
        dbContext.Database.EnsureCreated();
        await seeder.SeedAsync();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error creating database: {ex.Message}");
    }
}

app.UseCors("AllowReactApp");
app.MapControllers();


app.UseHttpsRedirection();

app.Run();