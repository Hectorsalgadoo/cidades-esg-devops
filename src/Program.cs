var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Endpoint de Health Check
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", timestamp = DateTime.UtcNow }));

// Endpoint de Indicadores ESG
app.MapGet("/api/esg/indicadores", () =>
{
    var indicadores = new[]
    {
        new { Id = 1, Categoria = "Ambiental", Nome = "Redução de Emissão de CO2", Valor = "85%", Status = "Dentro da Meta" },
        new { Id = 2, Categoria = "Social", Nome = "Inclusão Digital Urbana", Valor = "92%", Status = "Excelente" },
        new { Id = 3, Categoria = "Governança", Nome = "Transparência de Dados Públicos", Valor = "100%", Status = "Conforme" }
    };
    return Results.Ok(indicadores);
});

app.Run();