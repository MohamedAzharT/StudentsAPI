using Microsoft.EntityFrameworkCore;
using StudentsAPI.DataModels;
using StudentsAPI.StudentServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure the DbContext to use SQL Server and the connection string

builder.Services.AddScoped<StudentService>();

builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.Run();
