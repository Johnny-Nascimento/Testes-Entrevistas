using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Teste_Playmove_API.Mappers;
using Teste_Playmove_API.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FornecedorDbContext>(f => f.UseSqlServer(builder.Configuration.GetConnectionString("FornecedoresConnectionString")));

builder.Services.AddAutoMapper(typeof(FornecedorProfile).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Fornecedores.API",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Johnny Nascimento",
            Email = "nascimentoho1998@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/johnny-nascimento-315b0316a/")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
});

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
