Funcionalidade: Gestão de Indicadores ESG em Cidades Inteligentes
  Como um gestor urbano ou cidadão
  Quero consultar, registar e gerir indicadores de sustentabilidade (ESG)
  Para garantir a transparência e conformidade ambiental e social da cidade

  Cenário: Consultar indicadores ESG por categoria com sucesso (Caminho Feliz)
    Dado que a API de Cidades ESG está ativa e acessível
    Quando eu envio uma requisição GET para "/api/esg/indicadores" com a categoria "Ambiental"
    Então o código de status da resposta deve ser 200
    E o corpo da resposta deve conter uma lista com indicadores da categoria "Ambiental"

    Cenário: Registar um novo indicador ESG com sucesso (Caminho Feliz)
    Dado que a API de Cidades ESG está ativa e acessível
    E que eu possuo os dados de um novo indicador de "Governança" com meta "100%"
    Quando eu envio uma requisição POST para "/api/esg/indicadores" com esses dados
    Então o código de status da resposta deve ser 201
    E o corpo da resposta deve retornar o indicador criado contendo um ID gerado

    Cenário: Tentar consultar um indicador ESG por um ID inexistente (Cenário Negativo)
    Dado que a API de Cidades ESG está ativa e acessível
    Quando eu envio uma requisição GET para "/api/esg/indicadores/9999"
    Então o código de status da resposta deve ser 404
    E o corpo da resposta deve retornar uma mensagem informando que o indicador não foi encontrado