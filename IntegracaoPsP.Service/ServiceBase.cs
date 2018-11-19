using IntegracaoPsP.Factory.Biblioteca;
using IntegracaoPsP.Factory.Gerenciador;
using IntegracaoPsP.Factory.Interfaces;
using IntegracaoPsP.Factory.Processador;
using IntegracaoPsP.Infraestrutura.Container;
using IntegracaoPsP.Infraestrutura.Dominio.Entidades.Log;
using IntegracaoPsP.Infraestrutura.Dominio.Entidades.Outros;
using IntegracaoPsP.Infraestrutura.Dominio.Entidades.WS;
using IntegracaoPsP.Infraestrutura.Repositorio.Base;
using IntegracaoPsP.Infraestrutura.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Unity;

namespace IntegracaoPsP.Service
{
    public class ServiceBase : WebService
    {
        public void OnStart()
        {
            if (DependencyContainer.Container == null)
            {
                DependencyContainer.Container = new UnityContainer();

                DependencyContainer.Container.RegisterType(typeof(IGerenciador<>), typeof(GerenciadorPadrao<>));
                DependencyContainer.Container.RegisterType(typeof(IProcessador<>), typeof(ProcessadorPadrao<>));

                DependencyContainer.Container.RegisterType(typeof(IBiblioteca<LogArquivo>), typeof(LogArquivoBll));
                DependencyContainer.Container.RegisterType(typeof(IBiblioteca<LogIntegracao>), typeof(LogIntegracaoBll));
                DependencyContainer.Container.RegisterType(typeof(IBiblioteca<LogMessage>), typeof(LogMessageBll));
                DependencyContainer.Container.RegisterType(typeof(IBiblioteca<ModeloXsd>), typeof(ModeloXsdBll));
                DependencyContainer.Container.RegisterType(typeof(IBiblioteca<Parametro>), typeof(ParametroBll));
                DependencyContainer.Container.RegisterType(typeof(IBiblioteca<WS_LISTA>), typeof(WsListaBll));
                DependencyContainer.Container.RegisterType(typeof(IBiblioteca<WS_TIPOS>), typeof(WsTipoBll));
                DependencyContainer.Container.RegisterType(typeof(IBiblioteca<Log>), typeof(LogBll));

                //repositorios
                DependencyContainer.Container.RegisterType(typeof(IRepositorio<>), typeof(RepositorioBase<>));
                DependencyContainer.Container.RegisterType(typeof(IRepositorio<>), typeof(RepositorioBaseS2GPI<>));
            }
        }
    }
}