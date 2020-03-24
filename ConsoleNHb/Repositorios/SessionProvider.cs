using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using NHibernate;
using NHibernate.Cfg;
using ConsoleNHb.Models;

namespace ConsoleNHb.Repositorios
{
    public class SessionProvider
    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        public string ConnectionString { get; }
        static readonly object Padlock = new object();

        public SessionProvider(string connectioString = null)
        {
            ConnectionString = connectioString;
        }
        public ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                lock (Padlock) //APENAS PARA EVITAR ERROS DE CONCORRENCIA NO AMBIENTE ONLINE
                {
                    if (_sessionFactory == null)
                    {
                        _configuration = LoadConfiguration();
                        _sessionFactory = _configuration.BuildSessionFactory();
                    }
                }
            }
            return _sessionFactory;
        }
        private Configuration LoadConfiguration()
        {
            Configuration nhConfigurationCache;

            var conf = _configuration = new Configuration();
            conf.Configure();
            if (ConnectionString is object) // EU CONFIGURO A CONN STRING DINAMICAMENTE NO AMBIENTE OFFLINE, MAS POR PADRÃO ELE PEGA DO XML
                conf.SetProperty("connection.connection_string", ConnectionString);
            var assembly = typeof(Produto).Assembly; // ADICIONA TODAS AS CLASSES DO ASSEMBLY MAS VOCÊ PODE COLOCAR MANUALMENTE CLASSES OU PASSAR OUTROS ASSEMBLYS 
            conf.AddAssembly(assembly);
          
            nhConfigurationCache = conf;
            return nhConfigurationCache;
        }
        public Configuration Configuration
        {
            get
            {
                GetSessionFactory();
                return _configuration;
            }
        }
        public ISession OpenSession() => GetSessionFactory().OpenSession();
        public IStatelessSession OpenStatelessSession() => GetSessionFactory().OpenStatelessSession();
    }
}
