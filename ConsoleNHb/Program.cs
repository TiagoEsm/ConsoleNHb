/*
 * O arquivo <Model>.hbm.xml deve estar como Embedded Resource
 * O arquivo hibernate.cfg.xml deve estar como Content e Copy Always
 */

/*
 * NOME DO BANCO: MeuBanco
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
 */
using ConsoleNHb.Models;
using ConsoleNHb.Repositorios;
using System;

namespace ConsoleNHb
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new SessionProvider(); //Uma instancia por banco

            var grupoRepositorio = new RepositorioBase<ProdutoGrupo>(provider);
            var produtoRepositorio = new ProdutoRepositorio(provider);

            var grupoAlimentacao = new ProdutoGrupo
            {
                Descricao = "Alimentacao"
            };
            var grupoBebidas = new ProdutoGrupo
            {
                Descricao = "Bebidas"
            };

            Console.WriteLine($"CADASTRANDO");
            grupoRepositorio.Insert(grupoAlimentacao);
            grupoRepositorio.Insert(grupoBebidas);

            for (int i = 0; i < 1000; i++)
            {
                produtoRepositorio.Insert(
                    new Produto {
                        Nome = $"Produto {i:000}",
                        Grupo = i % 2 == 0 ? grupoAlimentacao : grupoBebidas,
                        PrecoVenda = i + 0.99m
                    });
            }
            Console.WriteLine($"CADASTRADO!");

            var bebidas = produtoRepositorio.BuscaPorGrupoAsync(grupoBebidas)
                .GetAwaiter().GetResult();
            Console.WriteLine($"BEBIDAS");
            foreach (var produto in bebidas)
            {
                Console.WriteLine($"({produto.Id}){produto.Nome}: {produto.PrecoVenda}");
            }

            Console.WriteLine($"Concluido. Pressione qualquer tecla para sair");
            Console.ReadKey();
        }
    }
}
