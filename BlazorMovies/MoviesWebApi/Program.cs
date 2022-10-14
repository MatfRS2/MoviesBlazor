using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MoviesWebApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesWebApiContext") ?? throw new InvalidOperationException("Connection string 'MoviesWebApiContext' not found.")));

string dopusteneAdrese = "dopustene_adrese";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: dopusteneAdrese,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7206",
                                              "http://localhost:7206");
                      });
});

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(dopusteneAdrese);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
