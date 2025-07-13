using ApiBackend.Dal;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen();


// Add services to the container.

builder.Services.AddControllers();
    
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostReact",
        builder => builder
            .WithOrigins("http://localhost:3000") // port de ton app React
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});
var app = builder.Build();
app.UseCors("AllowLocalhostReact");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
builder.Services.AddEndpointsApiExplorer();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

