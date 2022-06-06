using Kitchen.Models;
using Kitchen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();


void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<KitchenContext>(options => options
        .UseSqlite(builder.Configuration.GetConnectionString("KitchenDB")));

    builder.Services.AddMvc(options => options.EnableEndpointRouting = false)
        .AddNewtonsoftJson(options => options.SerializerSettings
        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    builder.Services.AddSingleton<ILogger, LoggerService>();
    builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
    builder.Services.AddScoped<IDishRepository, DishRepository>();

}

void ConfigureMiddleware(WebApplication app)
{
    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        using IServiceScope scope = app.Services.CreateScope();
        KitchenContext? context = scope.ServiceProvider.GetService<KitchenContext>();
        DataGenerator.Initialize(context);

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapControllers();
}
