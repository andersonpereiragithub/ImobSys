# ImobSys

_Sistema de Controle e Gest�o de Im�veis_

## Progresso do Projeto

### Primeiros Passos Conclu�dos
- [x] Cria��o do projeto no Visual Studio 2022
- [x] Organiza��o das camadas e pastas do projeto
- [x] Cria��o das classes principais (`Imovel`, `Endereco`, `Condominio`, etc.)
- [x] Implementa��o dos enums (`TipoImovel`, `FinalidadeImovel`, etc.)
- [x] Cria��o do reposit�rio GitHub e commit inicial
- [x] Configura��o inicial do reposit�rio de dados para persist�ncia em JSON
- [x] Implementa��o da camada de servi�o para `ImovelService`
- [x] Desenvolvimento inicial da interface no Console (menu e op��es)
- [x] Implementa��o de gera��o de IDs �nicos para `Imovel` e `Cliente`

### Pr�ximas Etapas
- [ ] Configurar interface de console para melhor usabilidade
    - Tamanho da tela, cores, t�tulo do console
    - M�scaras para entradas espec�ficas (CPF, Inscri��o de IPTU)
- [ ] Tratamento robusto de entrada de dados para evitar valores incorretos e duplicidade de cadastros
- [ ] Implementar m�todos de listagem e consulta
    - Listar todos os clientes e im�veis
    - Listar todos os im�veis de um cliente espec�fico
- [ ] Implementar documenta��es e tutoriais de uso para o projeto ImobSys
- [ ] Adicionar valida��es de campo obrigat�rias e opcionais
- [ ] Adicionar testes unit�rios para as classes de dom�nio e camada de servi�os
- [ ] Preparar para futuras migra��es para banco de dados relacional

## Objetivo do Projeto
O **ImobSys** � um sistema de controle e gest�o de im�veis, desenvolvido inicialmente como uma aplica��o de console. O sistema foi estruturado para permitir futuras migra��es para interfaces web ou desktop e suporte a persist�ncia de dados em diferentes formatos (come�ando com JSON e preparado para transi��o para banco de dados).

O objetivo principal do projeto � oferecer uma solu��o organizada e expans�vel para controle de propriedades, com recursos para cadastro, consulta, e atualiza��o de informa��es de im�veis e clientes, com funcionalidades de valida��o e integridade dos dados.

---

_Projeto privado desenvolvido por [Anderson Paiva Pereira]._