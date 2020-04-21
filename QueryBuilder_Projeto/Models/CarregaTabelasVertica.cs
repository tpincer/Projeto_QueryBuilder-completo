
using QueryBuilder_Projeto.Contexts;
using System;
using System.Data;
using Vertica.Data.VerticaClient;

namespace QueryBuilder_Projeto.Models {
    public class CarregaTabelasVertica : ConexaoVertica, IDisposable {
        public CarregaTabelasVertica(TipoConexao.Conexao TConexao, string NameStringConnection) : base(TConexao, NameStringConnection)
        {
            try
            {
                OpenConexao();
            }
            catch (Exception e)
            {
                _ = e.Message;
            }

        }

        public DataTable SqlCommandTable(string query)
        {

            VerticaCommand cmd = new VerticaCommand(query, conn);
            conn.Open();

            VerticaDataAdapter da = new VerticaDataAdapter(cmd);
            DataTable data = new DataTable();
            da.Fill(data);
            conn.Close();
            da.Dispose();

            return data;
        }
    }
}
