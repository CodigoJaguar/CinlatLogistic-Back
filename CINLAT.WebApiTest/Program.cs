using CINLAT.WebApiTest.Application;
using CINLAT.WebApiTest.Application.DTOs;
using CINLAT.WebApiTest.Infrastructure.Extensions;
using CINLAT.WebApiTest.Infrastructure.Security;
using CINLAT.WebApiTest.Infrastructure.Services;
using CINLAT.WebApiTest.Persistence;
using CINLAT.WebApiTest.Persistence.LoadData;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin() 
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CountryLoadService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//builder.WebHost.UseUrls("http://*:5025");
builder.Services.AddAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}
app.MapOpenApi();
app.MapScalarApiReference();

//app.UseHttpsRedirection();


app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();


// Generar migracion automatica y agregar users de autorizacion a DB
await app.SeedDataAutentication();


app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var countryLoader = scope.ServiceProvider.GetRequiredService<CountryLoadService>();
    await countryLoader.LoadCountriesAsync();
}

app.Run();
