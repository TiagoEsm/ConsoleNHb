using ConsoleNHb.Models;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleNHb.Repositorios
{
    public class ProdutoRepositorio : RepositorioBase<Produto>
    {
        public ProdutoRepositorio(SessionProvider provider)
            : base(provider)
        {
        }

        public async Task<IList<Produto>> BuscaPorGrupoAsync(ProdutoGrupo grupo)
        {
            using (var session = Provider.OpenStatelessSession())
                return await session.Query<Produto>()
                    .Where(p => p.Grupo.Id == grupo.Id)
                    .ToListAsync();
        }
    }
}
