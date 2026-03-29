# pwd-checker-api

API de validação de senha construída com ASP.NET Core 10.0, apresentando um padrão chain of responsibility para regras de validação flexíveis.

## Decisões e Motivações

Para execução deste projeto foram realizadas duas grandes decisões que merecem destaque:
1. **Escolha da arquitetura VerticalSlice**
    - **Motivação:** A escolha da arquitetura VerticalSlice foi motivada por o projeto ser um projeto inicialmente pequeno onde é possível voltar o core da aplicação para a funcionalidade em si, deixando o projeto mais coeso na estrutura da "Feature" e com baixo acomplamento com possíveis features que possam ser implementadas; <br><br>


2. **Escolha do padrão Chain of Responsability** (para validação das regras de senha)
    - **Motivação:** A escolha deste padrão se deu para evitar o acomplamento elevado entre as regras de negócio de validação, fazendo com que cada regra tenha uma única responsabilidade (SRP), permitindo que seja possível testar unitariamente cada regra de validação. <br>
    Como resutaldo, o código ficou mais fácil de dar manutenção e extensível para a inclusão de futuras regras de validação, sem impacto nas demais.

## Funcionalidades do Projeto

- ✅ Validação de senha com múltiplos critérios
- ✅ Padrão de design Chain of Responsibility
- ✅ Documentação Swagger/OpenAPI
- ✅ Testes unitários abrangentes com Moq
- ✅ Containerização Docker
- ✅ Configuração pronta para produção

## Estrutura do Projeto

```
pwd-checker-api/
├── src/
│   ├── pwd-checker-api/              # Projeto principal da API
│   │   ├── Features/
│   │   │   └── PasswordValidate/
│   │   │       ├── Domain/           # Lógica de negócio principal, handlers
│   │   │       ├── Application/      # Serviços, DTOs, handlers
│   │   │       └── Infra/            # Injeção de dependências
│   │   └── Program.cs                # Bootstrap da aplicação
│   └── pwd-checker-api-test/         # Testes unitários
│       └── Features/
│           └── PasswordValidate/
│               └── [Domain, Application] testes
├── Dockerfile                        # Build Docker multi-stage
├── docker-compose.yml                # Ambiente de desenvolvimento
├── docker-compose.prod.yml           # Ambiente de produção
└── README.md                         # Documentação da aplicação
```
## Regras de Validação de Senha

A API usa uma cadeia de handlers (padrão Chain of Responsability) para validar senhas:

1. **MinLengthHandler** - Mínimo 8 caracteres
2. **DigitHandler** - Pelo menos um dígito (0-9)
3. **LowercaseHandler** - Pelo menos uma letra minúscula (a-z)
4. **UppercaseHandler** - Pelo menos uma letra maiúscula (A-Z)
5. **SpecialCharHandler** - Pelo menos um caractere especial (!@#$%^&*)
6. **NoRepeatCharHandler** - Sem caracteres repetidos

## Quick Start

### Local Development

#### Pré-requisitos
- .NET 10.0 SDK
- Visual Studio Code ou IDE de sua escolha

#### Executar a API
```bash
cd src/pwd-checker-api
dotnet run
```

A API estará disponível em `http://localhost:5238`

#### Acessar Swagger UI
```
http://localhost:5238/swagger/index.html
```

#### Executar Testes
```bash
dotnet test src/pwd-checker-api-test/pwd-checker-api-test.csproj
```

### Docker

#### Quick Start - Docker Compose
```bash
# Modo de desenvolvimento
docker-compose up

# Modo de produção
docker-compose -f docker-compose.prod.yml up -d
```

#### Comandos Docker Manuais
```bash
# Construir imagem
docker build -t pwd-checker-api:latest .

# Executar container
docker run -p 5238:5238 pwd-checker-api:latest
```

## Endpoints da API

### Validar Senha
```
POST /api/v1/password/validate
Content-Type: application/json

{
  "password": "MySecure123!@#"
}
```

**Resposta (Sucesso):**
```json
{
  "isValid": true,
  "message": "Password is valid"
}
```

**Resposta (Falha):**
```json
{
  "isValid": false,
  "message": "Password is too short"
}
```

### CURL de Exemplo
```bash
curl -X 'POST' \
  'http://localhost:5238//api/v1/password/validate' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "password": "2342342"
}'
```

## Testes

### Testes Unitários
```bash
cd src/pwd-checker-api-test
dotnet test
```

## Desenvolvimento

### Compilar e Executar
```bash
dotnet build
dotnet run --project src/pwd-checker-api/pwd-checker-api.csproj
```

### Executar Testes Específicos
```bash
dotnet test --filter "TestName"
```

### Visualizar Documentação Swagger
- Desenvolvimento (local): `http://localhost:5238/swagger`
- Desenvolvimento (Docker): `http://localhost:5000/swagger`
