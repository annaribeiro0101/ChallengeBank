# 🏦 ChallengeBank API

O ChallengeBank é uma **Web API** construída com ASP.NET Core 8 para gerenciar contas bancárias. O projeto segue uma arquitetura em camadas, focando na separação de responsabilidades e na aplicação robusta de regras de negócio, especialmente em operações financeiras críticas como transferências e gestão de status de contas.

## ✨ Funcionalidades Implementadas

O sistema expõe endpoints para as seguintes operações:

* **Cadastro de Contas:** Permite a abertura de novas contas, garantindo a unicidade do **Documento** do titular (regra de negócio: não permite documentos duplicados).
* **Consulta e Filtro:** Pesquisa de contas por **Documento** ou **Nome do Cliente** (com filtro *case-insensitive*).
* **Inativação de Contas:** Altera o status da conta para `inativa`. Esta operação é auditada, gerando um **Registro de Log de Desativação** (documento, data/hora e usuário responsável).
* **Transferência entre Contas:** Permite a movimentação de saldo entre contas, com as seguintes validações transacionais:
    * Ambas as contas (origem e destino) devem estar ativas.
    * A conta de origem deve possuir saldo suficiente.
    * A operação é **atômica** (débito e crédito são persistidos ou revertidos juntos).

## 📐 Arquitetura do Projeto

O projeto `ChallengeBank` utiliza uma arquitetura em camadas (similar a *Clean Architecture*), organizada em quatro projetos principais:

| Projeto | Camada | Responsabilidade |
| :--- | :--- | :--- |
| `ChallengeBank.Api` | Apresentação | Controllers, DTOs de Entrada/Saída, Configuração do Swagger. |
| `ChallengeBank.Application`| Aplicação | Serviços e lógica de negócio principal (`ContaService`). |
| `ChallengeBank.Domain` | Domínio | Entidades (`Conta`, `ContaDesativadaLog`), Interfaces de Repositório e regras de negócio essenciais. |
| `ChallengeBank.Infrastructure`| Infraestrutura | Implementação do Entity Framework Core (`DbContext`), Migrations e Repositórios. |

## 🛠️ Tecnologias Utilizadas

* **Plataforma:** .NET 8 (ASP.NET Core)
* **Banco de Dados:** SQLite (com Entity Framework Core)
* **ORM:** Entity Framework Core (EF Core)
* **Mapeamento:** AutoMapper (usado para converter Entidades para DTOs, quebrando ciclos de referência JSON).
* **Padrões:** Injeção de Dependência (DI) e Repositório.

## 🚀 Como Rodar Localmente

Siga estes passos para configurar e executar a API em sua máquina:

### 1. Pré-requisitos
* Visual Studio 2022 ou Superior.
* .NET 8 SDK (ou versão superior).

### 2. Configuração do Banco de Dados
A API utiliza o Entity Framework Core com Migrations e SQLite. O banco de dados (`challengebank.db`) é criado e atualizado via comando:

1.  Abra o **Package Manager Console** no Visual Studio.
2.  Selecione o projeto **`ChallengeBank.Infrastructure`** como "Default Project".
3.  Execute o comando para aplicar todas as migrações pendentes:
    ```bash
    Update-Database
    ```

### 3. Execução da API
1.  No Visual Studio, defina **`ChallengeBank.Api`** como o projeto de inicialização.
2.  Selecione o perfil de execução (`https` é recomendado).
3.  Pressione **F5** ou clique em **Run**.
4.  O navegador será aberto no **Swagger UI** (`http://localhost:XXXX/swagger`), onde você pode testar todos os endpoints.

## 💡 Endpoints Principais (via Swagger)

| Método | Endpoint | Descrição |
| :--- | :--- | :--- |
| `POST` | `/contas/cadastrar` | Cria uma nova conta bancária. |
| `GET` | `/contas/consultar` | Consulta todas as contas ou aplica filtros por nome/documento. |
| `PUT` | `/contas/inativar/{documento}` | Inativa uma conta pelo documento e registra a ação no log. |
| `POST` | `/contas/transferir` | Realiza uma transferência entre duas contas. |
