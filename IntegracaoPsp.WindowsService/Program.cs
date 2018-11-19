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
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace FileSearchIntegrationService.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if (!DEBUG)
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TimerIntegracaoS2GPI()
            };
            ServiceBase.Run(ServicesToRun);
#else
            TimerIntegracaoS2GPI service = new TimerIntegracaoS2GPI();
            System.Threading.Thread.Sleep(99999999);
#endif
        }
    }
}
