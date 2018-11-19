using IntegracaoPsP.Factory.Biblioteca;
using IntegracaoPsP.Factory.Gerenciador;
using IntegracaoPsP.Factory.Interfaces;
using IntegracaoPsP.Factory.Log;
using IntegracaoPsP.Factory.Processador;
using IntegracaoPsP.Factory.Proxy;
using IntegracaoPsP.Infraestrutura.Container;
using IntegracaoPsP.Infraestrutura.Dominio.Entidades.Log;
using IntegracaoPsP.Infraestrutura.Dominio.Entidades.Outros;
using IntegracaoPsP.Infraestrutura.Dominio.Entidades.WS;
using IntegracaoPsP.Infraestrutura.Dominio.Enum;
using IntegracaoPsP.Infraestrutura.Repositorio.Base;
using IntegracaoPsP.Infraestrutura.Repositorio.Interfaces;
using System;
using System.ServiceProcess;
using Unity;

namespace FileSearchIntegrationService.Service
{
    public partial class TimerIntegracaoS2GPI : ServiceBase
    {
        #region Propriedades
        private Temporizador TimeTracker;
        private Logger Logger;
        #endregion

        #region Construtor
        public TimerIntegracaoS2GPI()
        {
            InitializeComponent();
            StartUp();
#if (DEBUG)
            OnStart(new string[] { });
#endif
        }
        public void StartUp()
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
        #endregion  

        #region Eventos
        protected override void OnStart(string[] args)
        {
            try
            {
                //Inicializa o timer
                TimeTracker = new Temporizador();
            }
            catch (Exception ex)
            {
                //Loga o problema
                Logger = new Logger(ex.Message, EnumStatusLog.Nok, DateTime.Now);
                Logger.WriteLog();
            }
        }

        protected override void OnStop()
        {
            try
            {
                //Loga o termino do serviço
                Logger = new Logger("Serviço parado", EnumStatusLog.Ok, DateTime.Now);
                Logger.WriteLog();
            }
            catch (Exception ex)
            {
                //Loga o problema
                Logger = new Logger(ex.Message, EnumStatusLog.Nok, DateTime.Now);
                Logger.WriteLog();
            }
        }
        #endregion
    }
}
