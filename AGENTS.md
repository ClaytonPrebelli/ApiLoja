# AGENTS.md — ApiLoja (Backend .NET 6)

## Projeto
API REST em .NET 6 com Entity Framework Core 6.0.0 + Pomelo MySQL para gestão de uma loja maçônica.
Banco de dados: MariaDB 10.11.3 (host: 158.69.19.64, db: prebelli_loja).

## Estrutura
- **Models/** — Entidades EF Core (UsuarioModels, CargosModels, FamiliaresModels, DocumentosModels, StatusModels, NoticiasModels, CandidatosModels, etc.)
- **Data/DataContext.cs** — DbContext com seed de 3 cargos (Venerável Mestre, Tesoureiro, Secretário)
- **Controllers/** — Controllers REST (UsuariosController, CandidatosController, FotosController, NoticiasController, LivrosController, DocumentosController)
- **Repositories/** — Repositories + interfaces (UsuariosRepository, CandidatosRepository, TokenRepository, FamiliaresRepository, LivrosRepository, NoticiasRepository, StatusRepository, FotosRepository)
- **Responses/** — DTOs de resposta (LoginResponse, NiverFamiliaresResponse, etc.)
- **Migrations/** — Migrations EF Core

## Conexão
Lida de `appsettings.json` via `builder.Configuration.GetConnectionString("DefaultConnection")`.
**NUNCA** colocar credenciais hardcoded no código — usar appsettings.json (que está no .gitignore).

## Entidade Principal: UsuarioModels
Campos: Id, Nome, CIM, CPF, RG, Nascimento, Naturalidade, Estado, Nacionalidade, EstadoCivil, TipoSanguineo, CEP, Profissao, Endereco, Numero, Cidade, Bairro, Email, Fone, Pai, Mae, Iniciacao, Elevacao, Exaltacao, Observacoes, ContatoEmergencia, FoneEmergencia, isCandidato, isAprendiz, isCompanheiro, isMestre, isAdmin, isSuperAdmin, Pass, DataAfiliacao, FormaAfiliacao, **CargoId** (FK para CargosModels), StatusId (FK para StatusModels).
Navigation: Cargo (CargosModels?), Status (StatusModels), Familiares, Documentos, Cobrancas.

## Alterações Recentes (Jul 2026)

### 1. Tabela Cargos (substitui campo Titulo)
- Criada entidade `CargosModels` (Id, Nome)
- Adicionado `DbSet<CargosModels> Cargos` no DataContext com seed data
- `UsuarioModels`: removido `string? Titulo`, adicionado `int? CargoId` + `virtual CargosModels? Cargo`
- Migration: `20260716213217_AddCargosTable`

### 2. Remoção da entidade Lojas
- Entidade única hardcoded: "Cavaleiros de Salomão nº 7106"
- **Deletados**: LojaModels.cs, LojasController.cs, LojasRepository.cs, ILojasRepository.cs, LojasResponse.cs
- Removido `DbSet<LojaModels> Lojas` do DataContext
- Removido `LojaId` de UsuarioModels e DocumentosModels
- Removido DI de ILojasRepository/LojasRepository do Program.cs
- UsuariosRepository: todas as queries removido `.Include(x => x.Loja)`, hardcoded loja na carteirinha
- UsuariosController: removido ILojasRepository dependency, filtro loja no listar
- FamiliaresRepository: removido join com Lojas, hardcoded nome loja
- FotosController: removido ILojasRepository
- LoginResponse: removido LojaId
- NiverFamiliaresResponse: campo Loja com default hardcoded
- Migration: `20260716214522_RemoveLojasTable`

### 3. Connection string no appsettings
- Movida string de conexão de hardcoded em Program.cs para `appsettings.json` → `"ConnectionStrings": { "DefaultConnection": "..." }`
- Program.cs lê via `builder.Configuration.GetConnectionString("DefaultConnection")`
- appsettings.json e appsettings.Development.json adicionados ao .gitignore
- Criado appsettings.example.json com placeholders

## Comandos Úteis
- `dotnet build` — Build do projeto
- `dotnet ef database update` — Aplica migrations pendentes
- `dotnet ef migrations add <Nome>` — Cria nova migration

## Pendências / Notas
- O engine da tabela Cargos no banco foi corrigido de MyISAM para InnoDB manualmente (MyISAM não suporta FK). Se recriar o banco do zero, verificar o engine.
- A migration `RemoveLojasTable` dropa coluna `LojaModelsId` de Documentos — verificar se há dados existentes antes de aplicar.
- A tabela `CobrancasModels` existe mas não foi alterada nesta sessão.
- O controller `FotosController.GravarFotoLojas` foi mantido como stub (sem referência a Loja entity).
