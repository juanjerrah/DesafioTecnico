
# Desafio técnico - Localiza&Co

Este projeto tem o objetivo de cumprir o desafio técnico para a vaga de desenvolvedor backend júnior da Localiz&Co. O desafio consiste na criação de um sistema para controlar a frota de veículos de uma locadora, permitindo cadastrar novos veículos, atualizar o status desses veículos e ter o registro de todas as movimentações no sistema, ou seja, registrar desde o moomento que o veículo entra na base, todos os seus alugueis e retornos, ate sua saída da base.


## Funcionalidades

- Cadastro de novos veículos
- Alteração de status dos veiculos(alugado - retornado)
- Remoção de veículos
- Registro das movimentações dos veículos
- Obter veículos por filtro
- Obter as movimentações de um veículo específico


## Stack utilizada

**Back-end:** .Net Core 6, PostgreSql, Entity Framework


## Documentação da API

## Obtém veículos

```http
  [GET] api/Locadora/Veiculo
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `VeiculoId` | `Guid` | **Opcional**. Id do veículo que deseja filtrar |
| `Placa` | `string` | **Opcional**. Placa do veículo que deseja filtrar |
| `TipoVeiculo` | `enum` | **Opcional**. Tipo do veículo que deseja filtrar |
| `StatusVeiculo` | `enum` | **Opcional**. Status do veículo que deseja filtrar |

#### Retorna uma lista de veículos que se encaixam com os filtros passados
```json
{
  "status": 200, 
  "dataCount": 0,
  "data": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "placa": "string",
      "eTipoVeiculo": "string",
      "eStatusVeiculo": "string"
    }
  ]
}
```
## Inserir veículo

```http
  [POST] api/Locadora/Veiculo
```

### Body de registro de novo veículo
```json
{
  "placa": "string",
  "tipoDoVeiculo": 1,
  "statusDoVeiculo": 1
}
```
#### Retorna o novo registro que foi criado
### Exemplo Retorno
```json
{
  "status": 100,
  "dataCount": 0,
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "placa": "string",
    "eTipoVeiculo": "string",
    "eStatusVeiculo": "string"
  }
}
```
## Atualizar veículo
```http
  [PATCH] api/Locadora/Veiculo
```

### Body de alteracao de veículo
```json
{
  "placa": "string",
  "statusDoVeiculo": 1
}
```
#### Retorna o registro que foi alterado
### Exemplo Retorno
```json
{
  "status": 200,
  "dataCount": 0,
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "placa": "string",
    "eTipoVeiculo": "string",
    "eStatusVeiculo": "string"
  }
}
```
## Excluir veículo
```http
  [DELETE] api/Locadora/Veiculo
```

### Body de alteracao de veículo

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Placa` | `string` | **Obrigatório**. Placa do veículo que deseja excluir |

#### Retorna o status da ação de exclusão
### Exemplo Retorno
```json
{
  "status": 200,
  "dataCount": 0,
  "data": true
}
```

## Obtém movimentações de um veículo

```http
  [GET] api/Locadora/Movimentacao/MovimentacoesVeiculo
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `VeiculoId` | `Guid` | **Obrigatório**. Id do veículo que deseja filtrar |

#### Retorna todos os registros do veículo referente a placa
```json
{
  "status": 200,
  "dataCount": 0,
  "data": [
    {
      "descricao": "string",
      "movimentacaoVeiculo": "string"
    }
  ]
}
```
