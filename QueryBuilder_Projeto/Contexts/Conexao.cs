using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Vertica.Data.VerticaClient;

namespace QueryBuilder_Projeto.Contexts {

    public class ConexaoVertica : IDisposable {
        public string mErro = "";

        // Variaveis de configuração de acesso ao banco de dados
        public VerticaConnection conn;
        public string connectionString;

        public ConexaoVertica(TipoConexao.Conexao TConexao, string nameStringConnection)
        {
            GetConexao(TConexao, nameStringConnection);
        }

        public ConexaoVertica()
        {

        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                conn.Close();

                conn.Dispose();
            }
        }

        // Verifica se existe erro
        public Boolean ExisteErro()
        {
            if (mErro.Length > 0)
            {
                return true;
            }

            return false;
        }

        private void GetConexao(TipoConexao.Conexao TConexao, string NameStringConnection)
        {
            try
            {
                string connectionStrings = "";
                connectionStrings = ConfigurationManager.ConnectionStrings["ContextVertica"].ConnectionString;

                // Faz a Conexao com o Banco de Dados
                conn = new VerticaConnection(connectionStrings);
                VerticaCommand comando = conn.CreateCommand();

            }
            catch (Exception erro)
            {
                this.mErro = erro.Message;
                this.conn = null;
            }
        }

        // Abre conexao com o Banco de Dados
        public bool OpenConexao()
        {
            bool _return = true;
            try
            {
                conn.Open();
            }
            catch (Exception erro)
            {
                this.mErro = erro.Message;
                _return = false;
            }

            return _return;
        }

        // Fecha conexao com o Banco de Dados
        public void CloseConexao()
        {
            conn.Close();

            conn.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Variavel"></param>
        /// <returns></returns>
        public static string GetWebConfig(string Variavel)
        {
            string strValue = "";
            Configuration rootWebConfig =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
            ConnectionStringSettings connString;
            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings[Variavel];
                if (null != connString)
                    strValue = connString.ConnectionString;
                else
                    strValue = "erro";
            }

            return strValue;
        }

        /// <summary>
        /// Definição de tipos de Conexão 
        /// </summary>
        public static class TipoConexao {
            public enum Conexao { WebConfig = 1, Classe = 2 };

        }

    }
}
