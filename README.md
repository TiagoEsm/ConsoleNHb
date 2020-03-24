# ConsoleNHb
Observações do Nhibernate
 * O arquivo <Model>.hbm.xml deve estar como Embedded Resource
 * O arquivo hibernate.cfg.xml deve estar como Content e Copy Always
 
 Banco de Dados
NOME DO BANCO: MeuBanco
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
