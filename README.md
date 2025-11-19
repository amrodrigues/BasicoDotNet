# Decisões de Design e Implementação - Desafio Técnico Bernhoeft GRT

## 1. Introdução

Este documento detalha as decisões de design e implementação tomadas para modificar a API existente, visando suportar as novas funcionalidades de **CRUD (Create, Read, Update, Delete)** para a entidade **Aviso**, conforme solicitado no desafio técnico.

***

## 2. Ajustes de Negócio e Modelo de Dados

### 2.1. Controle de Data de Criação e Edição

Para atender à necessidade de rastrear quando um aviso foi criado e editado, foram adicionados dois novos campos à entidade `AvisoEntity` (localizada na camada **Domain**):

| Campo | Tipo | Descrição |
| :--- | :--- | :--- |
| **DataCriacao** | `DateTime` | Armazena a data e hora da criação do aviso. |
| **DataEdicao** | `DateTime?` | Armazena a data e hora da última edição do aviso. É anulável. |

**Decisão de Design:** A lógica de preenchimento e atualização desses campos foi **encapsulada dentro da própria entidade** (`AvisoEntity`), seguindo o princípio de **Domain Driven Design (DDD)**. Isso garante que as regras de negócio de *timestamp* sejam aplicadas de forma consistente, independentemente de onde a entidade for manipulada.

### 2.2. Soft Delete

A prática de **soft delete** foi implementada para que os avisos não sejam permanentemente removidos do banco de dados, sendo apenas marcados como inativos.

| Campo | Tipo | Descrição |
| :--- | :--- | :--- |
| **Ativo** | `bool` | Indica se o aviso está ativo (`true`) ou inativo (`false`). Manipulado apenas pelo método `Deletar()`. |

**Decisão de Implementação:**

1.  **Entidade:** Foi adicionado o método **`Deletar()`** em `AvisoEntity.cs`, que define `Ativo = false`.
2.  **Repositório:** O método **`DeleteAsync`** em `AvisoRepository.cs` foi **sobrescrito (`override`)** para chamar o método `Deletar()` da entidade e, em seguida, persistir a alteração com `UpdateAsync`, em vez de realizar uma exclusão física (`Hard Delete`).
3.  **Buscas:** O método **`ObterTodosAvisosAsync`** em `AvisoRepository.cs` foi ajustado para incluir a cláusula **`Where(x => x.Ativo)`**, garantindo que apenas avisos ativos sejam retornados nas listagens.

***

## 3. Validações com Fluent Validation

O **Fluent Validation** foi utilizado para garantir que as regras de **validação de entrada** sejam aplicadas antes que a requisição chegue à camada de aplicação (**Handlers**).

| Requisição | Regra de Validação | Arquivo |
| :--- | :--- | :--- |
| **`CreateAvisoRequest`** | Titulo e Mensagem não podem ser nulos ou vazios. | `CreateAvisoRequestValidation.cs` |
| **`UpdateAvisoRequest`** | Id deve ser maior que zero. Mensagem não pode ser nula ou vazia. | `UpdateAvisoRequestValidation.cs` |
| **`GetAvisoByIdRequest`** | Id deve ser maior que zero. | `GetAvisoByIdRequestValidation.cs` |
| **`DeleteAvisoRequest`** | Id deve ser maior que zero. | `DeleteAvisoRequestValidation.cs` |

**Decisão de Design:** A validação de `Id > 0` foi aplicada nos *requests* de consulta e comando que utilizam o ID, prevenindo que requisições inválidas passem para a camada de aplicação.

***

## 4. Implementação da Camada de Aplicação (Handlers)

Foram criados os seguintes **Handlers** para orquestrar as operações de negócio, seguindo o padrão **CQRS** (Command Query Responsibility Segregation):

| Handler | Requisição | Resposta | Funcionalidade |
| :--- | :--- | :--- | :--- |
| **`CreateAvisoHandler`** | `CreateAvisoRequest` | `CreateAvisoResponse` | Cria a entidade, persiste e retorna o ID. |
| **`UpdateAvisoHandler`** | `UpdateAvisoRequest` | `UpdateAvisoResponse` | Busca o aviso, verifica se está ativo, chama o método `AtualizarMensagem` da entidade (que atualiza `DataEdicao`) e persiste. Lança `NotFoundException` se não encontrado. |
| **`DeleteAvisoHandler`** | `DeleteAvisoRequest` | `DeleteAvisoResponse` | Busca o aviso, verifica se está ativo, chama o `DeleteAsync` do repositório (soft delete) e persiste. Lança `NotFoundException` se não encontrado. |
| **`GetAvisoByIdHandler`** | `GetAvisoByIdRequest` | `GetAvisoByIdResponse` | Busca o aviso por ID, verifica se está ativo e mapeia para o DTO. Lança `NotFoundException` se não encontrado. |

***

## 5. Implementação dos Endpoints da API

O `AvisosController.cs` foi atualizado para incluir os novos endpoints, seguindo o padrão **REST** e utilizando a arquitetura de *Mediator* (`RestApiController` e `Mediator.Send`):

| Método HTTP | Rota | Handler Invocado |
| :--- | :--- | :--- |
| **GET** | `/avisos/{id}` | `GetAvisoByIdRequest` |
| **POST** | `/avisos` | `CreateAvisoRequest` |
| **PUT** | `/avisos/{id}` | `UpdateAvisoRequest` |
| **DELETE** | `/avisos/{id}` | `DeleteAvisoRequest` |
| **GET** | `/avisos` | `GetAvisosRequest` |

**Decisão de Design:** Os endpoints foram decorados com os atributos **`ProducesResponseType`** apropriados (`200 OK`, `201 Created`, `400 Bad Request`, `404 Not Found`) para garantir a documentação da API via **Swagger/OpenAPI** e padronizar o retorno de erros. O endpoint `GET /avisos` foi mantido, mas retorna apenas avisos ativos devido à alteração no repositório.