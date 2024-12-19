using System.Text;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

/*
var key = Encoding.UTF8.GetBytes("secret-key-secret-key-secret-key-secret-key-secret-key-secret-key");
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://localhost:5004"; // URL сервера аутентификации
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(key);
    });
*/



builder.Services.AddOcelot(builder.Configuration);


var app = builder.Build();


app.MapControllers();

await app.UseOcelot();

app.Run();