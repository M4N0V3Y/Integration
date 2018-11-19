using IntegracaoPsP.Factory.Interfaces;
using IntegracaoPsP.Factory.Log;
using IntegracaoPsP.Infraestrutura.Container;
using IntegracaoPsP.Infraestrutura.Dominio.Entidades.Outros;
using IntegracaoPsP.Infraestrutura.Dominio.Enum;
using IntegracaoPsP.Infraestrutura.Proxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Services;
using Unity;

namespace IntegracaoPsP.Service
{
    /// <summary>
    /// Summary description for PsP
    /// </summary>
    [WebService(Namespace = "http://linkcon.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class PsP : ServiceBase
    {
        #region Propriedades
        private FtpProxy Proxy;
        private Logger Logger;
        private IGerenciador<Parametro> GerenciadorPrametro;
        #endregion

        #region WebMethods
        [WebMethod(Description = @"Método responsável em verificar se existem arquivos a serem processados")]
        public string BuscarArquivos()
        {
            OnStart();
            Proxy = new FtpProxy();
            try
            {
                return Proxy.PercorrerDiretorio(null);
            }
            catch (Exception e)
            {
                Logger = new Logger(e.ToString(), EnumStatusLog.Nok, DateTime.Now);
                Logger.WriteLog();
                // return "NOK"; 
                return string.Format(" [ NOK ] " + e.ToString());
            }
        }

        [WebMethod(Description = @"Método responsável processar um arquivo de fomra individual")]
        public string BuscarArquivosIndividual(string caminho)
        {
            OnStart();
            Proxy = new FtpProxy();
            try
            {
                return Proxy.PercorrerDiretorio(caminho);
            }
            catch (Exception e)
            {
                Logger = new Logger(e.ToString(), EnumStatusLog.Nok, DateTime.Now);
                Logger.WriteLog();
                return "NOK";
            }
        }

        [WebMethod(Description = @"Método que retorna o valor de um determinado parametro")]
        public string BuscarParametro(string nomeParametro)
        {
            OnStart();
            try
            {
                GerenciadorPrametro = DependencyContainer.Container.Resolve<IGerenciador<Parametro>>();
                return ((Parametro)GerenciadorPrametro.Gerenciar(new Parametro() { Nome = nomeParametro }, EnumOperacao.SelecionarPorNome)).Valor;
            }
            catch (Exception e)
            {
                Logger = new Logger(e.ToString(), EnumStatusLog.Nok, DateTime.Now);
                Logger.WriteLog();
                return "NOK";
            }
        }

        [WebMethod(Description = @"Método de teste do webservice")]
        public string TestarConexaoWS()
        {
            try
            {
                return "OK";
            }
            catch (Exception e)
            {
                Logger = new Logger(e.ToString(), EnumStatusLog.Nok, DateTime.Now);
                Logger.WriteLog();
                return "NOK";
            }
        }

        #endregion
    }
}
