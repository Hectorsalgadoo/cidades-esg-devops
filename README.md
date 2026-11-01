# 🏙️ Projeto — Cidades ESG Inteligentes (DevOps)

Aplicação desenvolvida em **.NET 8 (C#)** para monitoramento, gestão e análise de indicadores **ESG** (*Environmental, Social, Governance*) no contexto de cidades inteligentes. 

O projeto conta com uma arquitetura focada em práticas DevOps, incluindo **containerização multi-stage com Docker**, orquestração com **Docker Compose**, **documentação interativa via Swagger (OpenAPI)**, **suite de testes automatizados (BDD e integração)** e **pipeline automatizada de CI/CD** via **GitHub Actions**.

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem & Framework:** C# (.NET 8) / ASP.NET Core Minimal API
- **Documentação da API:** Swagger / OpenAPI
- **Testes Automatizados:** xUnit, SpecFlow (BDD / Gherkin), FluentAssertions, Microsoft.AspNetCore.Mvc.Testing (`WebApplicationFactory`), Newtonsoft.Json.Schema
- **Containerização:** Docker & Docker Compose
- **CI/CD:** GitHub Actions
- **Controle de Versão:** Git / GitHub

---

## 📌 Endpoints da API (CRUD Completo & Health Check)

A API disponibiliza os seguintes recursos agrupados e estruturados:

| Verbo | Rota | Descrição | Tag |
| :--- | :--- | :--- | :--- |
| **GET** | `/health` | Verificação de saúde e estado da aplicação | Saúde do Sistema |
| **GET** | `/api/esg/indicadores` | Lista todos os indicadores (suporta filtro por `categoria`) | Indicadores ESG |
| **GET** | `/api/esg/indicadores/{id}` | Obtém um indicador específico por ID | Indicadores ESG |
| **POST** | `/api/esg/indicadores` | Cadastra um novo indicador ESG | Indicadores ESG |
| **PUT** | `/api/esg/indicadores/{id}` | Atualiza completamente um indicador existente por ID | Indicadores ESG |
| **DELETE** | `/api/esg/indicadores/{id}` | Remove um indicador ESG por ID | Indicadores ESG |

---

## 🧪 Testes Automatizados & Qualidade de Software

O projeto possui uma suíte completa de testes automatizados localizada em `tests/CidadesESG.Tests`, cobrindo os seguintes aspectos:

1. **Especificação BDD (Behavior-Driven Development):** Escrita em sintaxe **Gherkin** (`EsgIndicadores.feature`), cobrindo cenários de Caminho Feliz (consulta e criação de indicadores) e Caminho Negativo (recursos inexistentes).
2. **Testes de Integração de API:** Testes end-to-end em memória simulando requisições HTTP reais utilizando `WebApplicationFactory<Program>` do ASP.NET Core.
3. **Validação de Contrato JSON Schema:** Validação da estrutura das respostas JSON devolvidas pela API garantindo conformidade de schemas com a biblioteca `Newtonsoft.Json.Schema`.

---

## 🚀 Como Rodar e Executar Localmente

Podes executar o projeto e os testes na tua máquina local de duas formas principais: via linha de comando (.NET SDK) ou utilizando containers (Docker).

### Opção A: Executar os Testes e a API Localmente com o .NET SDK

1. **Pré-requisitos:** Ter o [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado.
2. **Clonar o repositório e navegar para a pasta:**
   ```bash
   git clone [https://github.com/Hectorsalgadoo/cidades-esg-devops.git](https://github.com/Hectorsalgadoo/cidades-esg-devops.git)
   cd cidades-esg-devops