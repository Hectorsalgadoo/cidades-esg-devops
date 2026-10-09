# 🏙️ Projeto — Cidades ESG Inteligentes (DevOps)

Aplicação desenvolvida em **.NET 8 (C#)** para monitoramento, gestão e análise de indicadores **ESG** (*Environmental, Social, Governance*) no contexto de cidades inteligentes. 

O projeto conta com uma arquitetura focada em práticas DevOps, incluindo **containerização multi-stage com Docker**, orquestração com **Docker Compose**, **documentação interativa via Swagger (OpenAPI)** e **pipeline automatizada de CI/CD** via **GitHub Actions**.

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem & Framework:** C# (.NET 8) / ASP.NET Core Minimal API
- **Documentação da API:** Swagger / OpenAPI
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

## 🚀 Como Executar Localmente com Docker Compose

### Pré-requisitos
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado e em execução.
- [Git](https://git-scm.com/) instalado.

### Passo a Passo

1. **Clonar o repositório:**
   ```bash
   git clone [https://github.com/Hectorsalgadoo/cidades-esg-devops.git](https://github.com/Hectorsalgadoo/cidades-esg-devops.git)
   cd cidades-esg-devops