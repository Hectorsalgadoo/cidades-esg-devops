Funcionalidade: Gestão de Indicadores ESG em Cidades Inteligentes
  Como um gestor urbano ou cidadão
  Quero consultar, registar e gerir indicadores de sustentabilidade (ESG)
  Para garantir a transparência e conformidade ambiental e social da cidade

  Cenário: Consultar indicadores ESG por categoria com sucesso (Caminho Feliz)
    Dado que a API de Cidades ESG está ativa e acessível
    Quando eu envio uma requisição GET para "/api/esg/indicadores" com a categoria "Ambiental"
    Então o código de status da resposta deve ser 200
    E o corpo da resposta deve conter uma lista com indicadores da categoria "Ambiental"