# Sistema de Gerenciamento de Fila

Aplicação console desenvolvida em C# que simula um sistema de atendimento com regra de prioridade dinâmica baseada em histórico.

O projeto evoluiu de uma implementação simples utilizando estruturas como Queue e Stack para uma arquitetura mais flexível, baseada em **regras de negócio desacopladas, uso de interfaces e testes unitários**.

---

## Objetivo do Projeto

Este projeto foi desenvolvido com foco em evolução prática em backend, com ênfase em:

* Modelagem de regras de negócio reais
* Separação de responsabilidades
* Escrita de testes unitários
* Uso de boas práticas de arquitetura

Durante o desenvolvimento, o sistema passou por refatorações importantes para melhorar consistência e testabilidade.

---

## Regras de Negócio

O sistema trabalha com dois tipos de clientes:

* **Comum**
* **Prioridade**

### Regra principal

* Após **3 atendimentos consecutivos de clientes comuns**, o próximo atendimento deve ser **prioritário**, caso exista.

### Decisão baseada em histórico

Diferente de uma abordagem com contador, o sistema utiliza o **histórico de atendimentos** como fonte de verdade:

* Analisa os **últimos 3 atendimentos**
* Se todos forem do tipo **Comum**, a policy sugere **Prioridade**
* Caso não exista cliente prioritário na fila, o sistema continua com **Comum**

### Funcionalidades

*  Adicionar cliente
*  Chamar próximo cliente
*  Desfazer último atendimento (Undo)
*  Visualizar fila e historico atual
![Menu do Sistema](QueueManagementSystem.Console/assets/Menu.png)

---

## Decisão da Ordem de Atendimento (Policy)

A lógica de decisão foi isolada na classe `CallOrderPolicy`.

### Responsabilidade

Determinar qual tipo de cliente deve ser chamado com base em:

* Histórico de atendimentos
* Existência de clientes prioritários na fila

### Por que usar uma Policy?

* Evita regras complexas dentro do Service
* Facilita testes isolados
* Permite evolução da regra sem impactar outras partes do sistema

---

## Arquitetura do Projeto

O projeto foi estruturado em camadas com responsabilidades bem definidas:

```
QueueSystem
│
├── Models            → Entidades do domínio (Client)
├── Enums             → Tipos e definições (ClientType, MenuOption)
├── Policies          → Regras de decisão (CallOrderPolicy)
├── Services          → Orquestração da lógica (QueueService)
├── Repositories      → Persistência em memória
├── UI                → Interface de interação (Console)
└── Program.cs        → Ponto de entrada
```

### Papéis das camadas

* **Models** → Representam o domínio e validam estado
* **Services** → Coordenam o fluxo da aplicação
* **Policies** → Contêm regras de decisão isoladas
* **Repositories** → Abstraem acesso aos dados
* **UI** → Responsável apenas pela interação com o usuário

---

## Testes Unitários

O projeto conta com testes utilizando **xUnit** e **Moq**, cobrindo diferentes camadas:

### Entidade (Client)

* Validação de nome
* Validação de tipo
* Garantia de estado consistente

### Policy (CallOrderPolicy)

* Últimos 3 atendimentos comuns
* Cenários com e sem cliente prioritário
* Histórico insuficiente
* Histórico vazio

### Service (QueueService)

* Validação de duplicidade
* Comportamento do Undo
* Uso de mocks para isolamento de dependências

### Repository

* Adição e remoção de clientes
* Verificação de existência (case insensitive)

### Destaque

Os testes foram escritos com foco em **comportamento**, garantindo que regras de negócio continuem corretas mesmo após refatorações.

---

## Evolução do Projeto

### Versão inicial

* Uso de Queue e Stack
* Controle de prioridade via contador

### Refatoração

* Substituição por List para maior flexibilidade e limitar a quantidade de histórico a 20.
* Remoção de contador

### Versão atual

* Regra baseada em histórico
* Introdução de Policy
* Uso de Repository com interface
* Testes unitários com mock

---

##  Decisões Técnicas Importantes

###  Uso de histórico ao invés de contador

* Evita inconsistência de estado
* Torna a regra mais confiável

### Separação da Policy

* Reduz acoplamento
* Facilita testes e manutenção

### Uso de interfaces (Repository e Policy)

* Permite desacoplamento
* Facilita uso de mocks nos testes

### Uso de List

* Maior controle sobre manipulação de dados
* Suporte a regras mais complexas (como Undo)

---

## Como Executar

1. Clonar o repositório
2. Garantir .NET 10 instalado
3. Executar o projeto:

```bash
dotnet run
```

Para rodar os testes:

```bash
dotnet test
```

---

## Melhorias Futuras

* Persistência em banco de dados
* Exposição via API REST
* Interface gráfica
* Logs e auditoria de atendimentos
* Métricas de desempenho da fila

---

## Autor

Desenvolvido por **Manoel Americo** como parte do processo de evolução em backend.
