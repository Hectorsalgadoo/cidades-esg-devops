using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Xunit;

public class EsgApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EsgApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetIndicadores_DeveRetornar2OkELista_QuandoCategoriaForAmbiental()
    {
        // Act (Caminho Feliz - GET)
        var response = await _client.GetAsync("/api/esg/indicadores?categoria=Ambiental");

        // Assert (Status Code)
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert (Corpo da Resposta JSON)
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task PostIndicadores_DeveRetornar201Created_QuandoDadosForemValidos()
    {
        // Arrange
        var novoIndicador = new
        {
            titulo = "Redução de Emissões de Carbono",
            categoria = "Ambiental",
            valor = 85.5,
            meta = "90%"
        };

        // Act (Caminho Feliz - POST)
        var response = await _client.PostAsJsonAsync("/api/esg/indicadores", novoIndicador);

        // Assert (Status Code 201 Created)
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("id");
    }

    [Fact]
    public async Task GetIndicadoresPorId_DeveRetornar404_QuandoIdForInexistente()
    {
        // Act (Caminho Negativo - ID Inexistente)
        var response = await _client.GetAsync("/api/esg/indicadores/9999");

        // Assert (Status Code 404 Not Found)
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ValidarContratoJsonSchema_DoIndicadorESG()
    {
        // Act
        var response = await _client.GetAsync("/api/esg/indicadores");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var jsonString = await response.Content.ReadAsStringAsync();
        JToken jsonToken = JToken.Parse(jsonString);

        // Arrange (Definição do Contrato JSON Schema exigido no requisito)
        string schemaJson = @"{
            'type': 'array',
            'items': {
                'type': 'object',
                'properties': {
                    'id': {'type': ['integer', 'string']},
                    'titulo': {'type': 'string'},
                    'categoria': {'type': 'string'},
                    'valor': {'type': 'number'}
                },
                'required': ['id', 'categoria']
            }
        }";

        JSchema schema = JSchema.Parse(schemaJson);

        // Assert (Validação de Contrato)
        bool isValid = jsonToken.IsValid(schema);
        isValid.Should().BeTrue("O contrato da API deve corresponder ao JSON Schema definido.");
    }
}