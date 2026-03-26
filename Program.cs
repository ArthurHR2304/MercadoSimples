using Microsoft.EntityFrameworkCore;
using MercadoSimples.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// No .NET 10, o Swagger agora é configurado assim:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilita o Swagger para você testar
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

app.UseHttpsRedirection();

app.UseAuthorization(); // Adicione esta linha por segurança

app.MapControllers(); // ESSA LINHA É A QUE FAZ OS PRODUTOS APARECEREM

app.Run();