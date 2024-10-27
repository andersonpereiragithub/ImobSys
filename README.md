# ImobSys

_Sistema de Controle e Gestão de Imóveis_

## Progresso do Projeto

### Primeiros Passos Concluídos
- [x] Criação do projeto no Visual Studio 2022
- [x] Organização das camadas e pastas do projeto
- [x] Criação das classes principais (`Imovel`, `Endereco`, `Condominio`, etc.)
- [x] Implementação dos enums (`TipoImovel`, `FinalidadeImovel`, etc.)
- [x] Criação do repositório GitHub e commit inicial
- [x] Configuração inicial do repositório de dados para persistência em JSON
- [x] Implementação da camada de serviço para `ImovelService`
- [x] Desenvolvimento inicial da interface no Console (menu e opções)
- [x] Implementação de geração de IDs únicos para `Imovel` e `Cliente`

### Próximas Etapas
- [ ] Configurar interface de console para melhor usabilidade
    - Tamanho da tela, cores, título do console
    - Máscaras para entradas específicas (CPF, Inscrição de IPTU)
- [ ] Tratamento robusto de entrada de dados para evitar valores incorretos e duplicidade de cadastros
- [ ] Implementar métodos de listagem e consulta
    - Listar todos os clientes e imóveis
    - Listar todos os imóveis de um cliente específico
- [ ] Implementar documentações e tutoriais de uso para o projeto ImobSys
- [ ] Adicionar validações de campo obrigatórias e opcionais
- [ ] Adicionar testes unitários para as classes de domínio e camada de serviços
- [ ] Preparar para futuras migrações para banco de dados relacional

## Objetivo do Projeto
O **ImobSys** é um sistema de controle e gestão de imóveis, desenvolvido inicialmente como uma aplicação de console. O sistema foi estruturado para permitir futuras migrações para interfaces web ou desktop e suporte a persistência de dados em diferentes formatos (começando com JSON e preparado para transição para banco de dados).

O objetivo principal do projeto é oferecer uma solução organizada e expansível para controle de propriedades, com recursos para cadastro, consulta, e atualização de informações de imóveis e clientes, com funcionalidades de validação e integridade dos dados.

---

_Projeto privado desenvolvido por [Anderson Paiva Pereira]._

---

_Projeto privado desenvolvido por [Anderson Paiva Pereira]._
