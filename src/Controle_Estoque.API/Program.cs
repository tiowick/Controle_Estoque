using Controle_Estoque.API.Configuracao;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Domain.Notificacoes;
using Controle_Estoque.Infra.Data.Context;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
     .ConfigureApiBehaviorOptions(options =>
     {
         options.SuppressModelStateInvalidFilter = true;
     });


builder.Services.AddMvc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
       builder.Configuration.GetConnectionString("DefaultConnection"),
       new MySqlServerVersion(new Version(8, 0, 41)) // Ajuste para sua versão do MySQL
   );
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

    options.AddPolicy("Production", builder =>
                builder
                    .WithOrigins("http://localhost:5188")
                    .WithMethods()
                    .AllowAnyHeader());


    //options.AddPolicy("Production", builder =>
    //  builder
    //      .WithOrigins("https://sistemaswebjs.com.br") // Permitir requisições desse domínio
    //      .AllowAnyMethod()
    //      .AllowAnyHeader());
});


builder.Services.Configure<FormOptions>(x =>
{
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.ValueLengthLimit = int.MaxValue;
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ResolveDependencies();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(60);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});


var app = builder.Build();

app.UseCors("Development");

//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));



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
