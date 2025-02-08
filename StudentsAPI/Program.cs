using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentsAPI.DataModels;
using StudentsAPI.StudentServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure the DbContext to use SQL Server and the connection string

builder.Services.AddScoped<StudentService>();

builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authentication and authorization services
var jwtSettings = builder.Configuration.GetSection("JwtSettings");  // Get JWT settings from appsettings.json
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);  // The secret key for JWT

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
options.RequireHttpsMetadata = false;  // Set to true for production
options.SaveToken = true;
options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),  // Sign and validate using the secret key
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],  // Set issuer from appsettings.json
        ValidAudience = jwtSettings["Audience"],  // Set audience from appsettings.json
        ValidateLifetime = true,  // Validate token expiry
        ClockSkew = TimeSpan.Zero  // Optional: Remove default 5 minutes tolerance
    };
});

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
// Use authentication and authorization middleware
app.UseAuthentication();  // This will check for a valid JWT token
app.UseAuthorization();   // This will authorize the user based on the JWT

app.MapControllers();

app.Run();
