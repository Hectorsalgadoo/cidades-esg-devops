using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Xunit;
using Xunit.Abstractions;

public class EsgApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public EsgApiIntegrationTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    [Trait("Categoria", "Caminho Feliz")]
    public async Task GetIndicadores_DeveRetornar2OkELista_QuandoCategoriaForAmbiental()
    {
        _output.WriteLine("[TESTE EXECUTADO]: Consulta de Indicadores ESG filtrados por Categoria (Ambiental) - Status Esperado: 200 OK");
        
        var response = await _client.GetAsync("/api/esg/indicadores?categoria=Ambiental");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Categoria", "Caminho Feliz")]
    public async Task PostIndicadores_DeveRetornar201Created_QuandoDadosForemValidos()
    {
        _output.WriteLine("[TESTE EXECUTADO]: Registo de Novo Indicador ESG (POST) - Status Esperado: 201 Created");

        var novoIndicador = new
        {
            categoria = "Ambiental",
            nome = "Redução de Emissões de Carbono",
            descricao = "Frota verde",
            valorAtual = "85%",
            meta = "90%",
            status = "Em Progresso",
            ultimaAtualizacao = "2026-Q3"
        };

        var response = await _client.PostAsJsonAsync("/api/esg/indicadores", novoIndicador);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Id");
    }

    [Fact]
    [Trait("Categoria", "Caminho Negativo")]
    public async Task GetIndicadoresPorId_DeveRetornar404_QuandoIdForInexistente()
    {
        _output.WriteLine("[TESTE EXECUTADO]: Consulta por ID Inexistente (Cenário Negativo) - Status Esperado: 404 Not Found");

        var response = await _client.GetAsync("/api/esg/indicadores/9999");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    [Trait("Categoria", "Contrato e Schema")]
    public async Task ValidarContratoJsonSchema_DoIndicadorESG()
    {
        _output.WriteLine("[TESTE EXECUTADO]: Validação de Contrato JSON Schema da API de Indicadores ESG");

        var response = await _client.GetAsync("/api/esg/indicadores");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var jsonString = await response.Content.ReadAsStringAsync();
        JToken jsonToken = JToken.Parse(jsonString);

        string schemaJson = @"{
            'type': 'array',
            'items': {
                'type': 'object',
                'properties': {
                    'Id': {'type': 'integer'},
                    'Categoria': {'type': 'string'},
                    'Nome': {'type': 'string'},
                    'Meta': {'type': 'string'}
                },
                'required': ['Id', 'Categoria', 'Nome']
            }
        }";

        JSchema schema = JSchema.Parse(schemaJson);
        bool isValid = jsonToken.IsValid(schema);
        isValid.Should().BeTrue("O contrato da API deve corresponder ao JSON Schema definido.");
    }
}