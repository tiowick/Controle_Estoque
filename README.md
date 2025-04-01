# Controle de Estoque

## Desenvolvendo uma Api para controle de estoque

### Projeto com finalidadede para estudos

# Documentação do Sistema de Controle de Estoque

## Visão Geral
O sistema é uma API REST desenvolvida em .NET para gerenciamento de estoque, permitindo o controle de produtos, empresas, filiais e movimentações de estoque.

## Estrutura do Projeto

### 1. Domínio (ControleEstoque.Domain)
Contém as regras de negócio e entidades principais do sistema:

#### Entidades Principais:
- Empresa: Representa as empresas cadastradas no sistema
- Filial: Representa as filiais vinculadas às empresas
- Produto: Cadastro e gestão de produtos
- Estoque: Controle de quantidade de produtos
- Movimentacao: Registro de entradas e saídas de produtos

#### Validações:
- Validadores específicos para cada entidade
- Sistema de notificações para tratamento de erros
- Validação de documentos
- Tratamento de textos e erros SQL

### 2. API (Controle_Estoque.API)

#### Controllers:
- EmpresaController: Gestão de empresas
- FilialController: Gestão de filiais
- ProdutoController: Gestão de produtos
- EstoqueController: Controle de estoque
- MovimentacaoController: Registro de movimentações

## Fluxos Principais

### 1. Gestão de Empresas
- Cadastro de empresas com validação de documentos (CNPJ)
- Vinculação de filiais
- Consultas e atualizações de dados

### 2. Gestão de Produtos
- Cadastro de produtos com identificadores únicos
- Controle de estoque por produto
- Vinculação com empresas e filiais

### 3. Movimentações de Estoque
- Registro de entradas e saídas
- Tipos de movimentação: Entrada (0) e Saída (1)
- Validação de quantidade disponível
- Atualização automática do estoque

### 4. Validações e Tratamento de Erros
- Sistema de notificações para erros
- Validações específicas por entidade
- Tratamento de exceções personalizado
- Padronização de mensagens de erro

## Endpoints Principais

### Empresas
- GET /api/empresas: Lista todas as empresas
- POST /api/empresas: Cadastra nova empresa
- PUT /api/empresas/{id}: Atualiza dados da empresa
- DELETE /api/empresas/{id}: Remove empresa

### Produtos
- GET /api/produtos: Lista todos os produtos
- POST /api/produtos: Cadastra novo produto
- PUT /api/produtos/{id}: Atualiza dados do produto
- DELETE /api/produtos/{id}: Remove produto

### Movimentações
- GET /api/movimentacoes: Lista todas as movimentações
- POST /api/movimentacoes: Registra nova movimentação
- GET /api/movimentacoes/produto/{id}: Lista movimentações por produto
- GET /api/movimentacoes/empresa/{id}: Lista movimentações por empresa
- GET /api/movimentacoes/filial/{id}: Lista movimentações por filial

## Padrões de Projeto Utilizados
- Repository Pattern: Para acesso a dados
- SOLID: Princípios de design de software
- DDD: Domain Driven Design (separação em camadas)
- Injeção de Dependência: Para acoplamento baixo
- DTO/ViewModels: Para transferência de dados


# Vamos trocar experiências? 
## Me chama aqui no WhatsApp : [71981859864](https://api.whatsapp.com/send?phone=5571981859864&text=Opa%2C%20iai%20tudo%20bom%3F%20Bora%20trocar%20experi%C3%AAncias%21%21)



