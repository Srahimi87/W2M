using Application.APIClients;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

/*var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();*/

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddApplicationServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}

builder.Services.AddHttpClient();
builder.Services.AddScoped<ProductsAPIClient>();
builder.Services.AddScoped<WalletAPIClient>();
builder.Services.AddScoped<UserAPIClient>();
builder.Services.Configure<APIConfig>(builder.Configuration.GetSection("APIConfig"));
builder.Services.AddSingleton<CustomJwt>();


builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
});



var app = builder.Build();
 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();



app.Run();
