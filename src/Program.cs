using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cidades ESG Inteligentes - API de Gestão Urbana",
        Version = "v1.0",
        Description = "API RESTful para monitorização, recolha e análise de indicadores ESG (Environmental, Social, Governance) no contexto de cidades inteligentes.",
        Contact = new OpenApiContact
        {
            Name = "Equipe DevOps ESG",
            Email = "suporte@cidades-esg.com"
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cidades ESG API v1");
    c.DocumentTitle = "Documentação API - Cidades ESG Inteligentes";
});

// Lista de indicadores ESG em memória
var indicadoresESG = new List<IndicadorESG>
{
    new(1, "Ambiental", "Redução de Emissão de CO2", "Frota de autocarros elétricos e mobilidade verde", "85%", "90%", "Em Progresso", "2026-Q3"),
    new(2, "Ambiental", "Eficiência Energética Iluminação Pública", "Troca de lâmpadas por LED inteligente com sensores", "94%", "95%", "Dentro da Meta", "2026-Q3"),
    new(3, "Social", "Inclusão Digital e Acesso à Internet", "Pontos de Wi-Fi gratuito em zonas de vulnerabilidade social", "92%", "85%", "Meta Atingida", "2026-Q2"),
    new(4, "Social", "Taxa de Reciclagem Comunitária", "Coleta seletiva porta a porta e ecopeças municipais", "68%", "75%", "Atenção", "2026-Q3"),
    new(5, "Governança", "Transparência e Dados Abertos", "Disponibilização de orçamentos públicos em portal aberto", "100%", "100%", "Conforme", "2026-Q3"),
    new(6, "Governança", "Digitalização de Serviços Públicos", "Abertura de processos sem papel via portal do cidadão", "88%", "90%", "Dentro da Meta", "2026-Q3")
};

// 1. Health Check
app.MapGet("/health", () => Results.Ok(new
{
    status = "Healthy",
    service = "CidadesESG.API",
    version = "1.0.0",
    environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "Production",
    timestamp = DateTime.UtcNow
}))
.WithTags("Saúde do Sistema");

// 2. Obter todos os indicadores (com filtro opcional de categoria)
app.MapGet("/api/esg/indicadores", (string? categoria) =>
{
    if (string.IsNullOrWhiteSpace(categoria))
        return Results.Ok(indicadoresESG);

    var filtrados = indicadoresESG.Where(i => i.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase)).ToList();
    return Results.Ok(filtrados);
})
.WithTags("Indicadores ESG");

// 3. Obter indicador por ID
app.MapGet("/api/esg/indicadores/{id:int}", (int id) =>
{
    var indicador = indicadoresESG.FirstOrDefault(i => i.Id == id);
    return indicador is not null ? Results.Ok(indicador) : Results.NotFound(new { mensagem = $"Indicador com ID {id} não foi encontrado." });
})
.WithTags("Indicadores ESG");

// 4. Registar novo indicador ESG (POST)
app.MapPost("/api/esg/indicadores", (IndicadorESG novoIndicador) =>
{
    var id = indicadoresESG.Any() ? indicadoresESG.Max(i => i.Id) + 1 : 1;
    var indicadorComId = novoIndicador with { Id = id };
    indicadoresESG.Add(indicadorComId);
    return Results.Created($"/api/esg/indicadores/{id}", indicadorComId);
})
.WithTags("Indicadores ESG");

// 5. Atualizar indicador existente (PUT)
app.MapPut("/api/esg/indicadores/{id:int}", (int id, IndicadorESG indicadorAtualizado) =>
{
    var index = indicadoresESG.FindIndex(i => i.Id == id);
    if (index == -1)
        return Results.NotFound(new { mensagem = $"Indicador com ID {id} não foi encontrado para atualização." });

    var item = indicadorAtualizado with { Id = id };
    indicadoresESG[index] = item;
    return Results.Ok(item);
})
.WithTags("Indicadores ESG");

// 6. Remover indicador ESG (DELETE)
app.MapDelete("/api/esg/indicadores/{id:int}", (int id) =>
{
    var indicador = indicadoresESG.FirstOrDefault(i => i.Id == id);
    if (indicador is null)
        return Results.NotFound(new { mensagem = $"Indicador com ID {id} não foi encontrado para remoção." });

    indicadoresESG.Remove(indicador);
    return Results.Ok(new { mensagem = $"Indicador com ID {id} foi removido com sucesso." });
})
.WithTags("Indicadores ESG");

app.Run();

// Estrutura do objeto
public record IndicadorESG(
    int Id,
    string Categoria,
    string Nome,
    string Descricao,
    string ValorAtual,
    string Meta,
    string Status,
    string UltimaAtualizacao
);