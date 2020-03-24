using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNHb.Repositorios
{
    public class RepositorioBase<T>
    {
        private SessionProvider _provider;

        protected SessionProvider Provider { get => _provider; set => _provider = value; }

        public RepositorioBase(SessionProvider provider)
        {
            Provider = provider;
        }

        //SEM ASYNC
        public virtual void Insert(T entidade)
        {
            using (var session = Provider.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(entidade);
                    transaction.Commit();
                }
            }
        }

        public async virtual Task InsertAsync(T entidade)
        {
            using (var session = Provider.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    await session.SaveAsync(entidade).ConfigureAwait(false);
                    await transaction.CommitAsync().ConfigureAwait(false);
                }
            }
        }

        //EXEMPLO COM LISTA
        public async virtual Task InsertAsync(IList<T> entidades)
        {
            using (var session = Provider.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    for (int i = 0; i < entidades.Count; i++)
                    {
                        var entidade = entidades[i];
                        await session.SaveAsync(entidade).ConfigureAwait(false);

                        //ALGUNS EXEMPLOS DA DOCUMENTAÇÂO SUGERIAM DAR UM FLUSH E CLEAR A CADA 20
                        if (i % 20 == 0) 
                        {
                            await session.FlushAsync().ConfigureAwait(false); 
                            session.Clear();
                        }
                    }
                    await transaction.CommitAsync().ConfigureAwait(false);
                }
            }
        }

        public async virtual Task UpdateAsync(T entidade)
        {
            using (var session = Provider.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    await session.UpdateAsync(entidade).ConfigureAwait(false);
                    await transaction.CommitAsync().ConfigureAwait(false);
                }
            }
        }

        public async virtual Task SaveOrUpdateAsync(T o)
        {
            using (var session = Provider.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    await session.MergeAsync(o).ConfigureAwait(false);
                    await transaction.CommitAsync().ConfigureAwait(false);
                }
            }
        }

        public async virtual Task DeleteAsync(T o)
        {
            using (var session = Provider.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    await session.DeleteAsync(o).ConfigureAwait(false);
                    await transaction.CommitAsync().ConfigureAwait(false);
                }
            }
        }
        
        //BUSCAS
        public async virtual Task<T> FindAsync(object id)
        {
            //IStatelessSession É MAIS PERFORMATICO PARA SELECT
            using (IStatelessSession session = Provider.OpenStatelessSession()) 
                return await session.GetAsync<T>(id);
        }
    }
}
