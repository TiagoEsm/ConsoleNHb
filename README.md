# ConsoleNHb
## Observações do Nhibernate
 * Os arquivos de mapeamento \<Model\>.hbm.xml devem estar como Embedded Resource
 * O arquivo hibernate.cfg.xml deve estar como Content e Copy Always, ele contem informações de conexão com o banco e do dialeto
 * Tem um arquivo opcional que é o schema do Nhibernate só para o Visual Studio auxiliar, mas eu não coloquei
 
## Banco de Dados
NOME DO BANCO: MeuBanco

```SQL

 CREATE TABLE [dbo].[Produto](
   [ProdutoId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
   [Nome] varchar(100) NOT NULL,
   [GrupoId] [int] NOT NULL,
   [PrecoVenda] decimal(9,2) NOT NULL
   )
 CREATE TABLE [dbo].[ProdutoGrupo](
   [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
   [Descricao] varchar(100) NOT NULL
   )
```

## Classes
* SessionProvider -> Centraliza a criação da Sessão do NHibernate, utilizo uma instancia por banco
* RepositorioBase<T> -> Exemplo básico de um Crud Genérico
* ProdutoRepositorio -> Exemplo básico de uma busca com LINQ
