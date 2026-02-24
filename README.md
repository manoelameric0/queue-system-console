#  Sistema de Gerenciamento de Fila

Aplicação console desenvolvida em C# para simular um sistema de atendimento com regra de prioridade.

Este projeto foi criado com o objetivo de praticar estruturas de dados (Queue e Stack),
separação de responsabilidades (arquitetura em camadas) e implementação de regras de negócio.

---

##  Regras de Negócio

- O sistema possui dois tipos de clientes:
  - Normal
  - Preferencial

- Após **3 atendimentos normais**, o próximo atendimento deve ser **preferencial**, caso exista.

- O sistema permite:
  - Adicionar cliente
  - Chamar próximo cliente
  - Desfazer último atendimento (Undo)
  - Visualizar filas e histórico

---

##  Conceitos Aplicados

- Queue (FIFO)
- Stack (LIFO)
- Separação em camadas (Models, Services, Interfaces)
- Isolamento de regra de negócio
- Tratamento de exceções
- Uso de Enum
- Evolução incremental com commits organizados

---

##  Estrutura do Projeto

```
QueueSystem
│
├── Models
│   └── Client.cs
│
├── Enums
│   └── ClientType.cs
│
├── Services
│   └── QueueService.cs
│
├── Repository
│   └── IQueueRepository.cs
│   └── InMemoryQueueRepository.cs
│
├── Interfaces
│   └── IQueueService.cs
│
└── Program.cs
```

---

##  Como Executar

1. Clonar o repositório
2. dotnet --version 10
3. Abrir no Visual Studio ou VS Code
4. Executar (build) o projeto

---

##  Melhorias Futuras

- Implementar testes unitários
- Adicionar persistência (arquivo ou banco de dados)
- Melhorar interação com usuário
- Gerar ID automático para clientes
- Aprimorar validações

---

##  Autor

Desenvolvido por Manoel Americo como parte do processo de evolução em backend.
