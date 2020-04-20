using QueryBuilder_Projeto.Models;
using Sws.LinqQueryBuilder;
using Sws.LinqQueryBuilder.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static QueryBuilder_Projeto.Contexts.ConexaoVertica;

namespace QueryBuilder_Projeto.Controllers {
    public class BuilderController : Controller {
        public ActionResult Tabelas()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("select table_name As Tabelas, ");
            builder.Append(CriarHtmlA("table_name"));
            builder.Append(" from tables");


            CarregaTabelasVertica db = new CarregaTabelasVertica(TipoConexao.Conexao.Classe,
                System.Configuration.ConfigurationManager.ConnectionStrings["ContextVertica"].ConnectionString.ToString());

            ViewBag.Tabelas = db.SqlCommandTable(builder.ToString()).DataTableToHtmlTable(
                new TableCssOptions()
                {
                    TableCss = "customers"
                });

            return View();
        }

        public ActionResult Colunas()
        {

            StringBuilder builder = new StringBuilder();
            builder.Append("select t.table_name As Tabela, c.column_name As Coluna, ");
            builder.Append(CriarHtmlInput("c.column_name"));
            builder.Append(" from columns c ");
            builder.Append("inner join tables t ");
            builder.Append("on c.table_id = t.table_id ");
            builder.Append($"where t.table_name = '{Request.QueryString["Tabela"].SearchKeysDanger()}'");

            CarregaTabelasVertica db = new CarregaTabelasVertica(TipoConexao.Conexao.Classe,
                System.Configuration.ConfigurationManager.ConnectionStrings["ContextVertica"].ConnectionString.ToString());

            ViewBag.Tabelas = db.SqlCommandTable(builder.ToString()).DataTableToHtmlTable(
                 new TableCssOptions()
                 {
                     TableCss = "customers"
                 });

            return View();
        }

        public ActionResult Filtro()
        {
            var tabela = Request.QueryString["Tabela"].SearchKeysDanger();

            if (tabela is null)
                return View();

            var colunas = Request.QueryString["Colunas"].SearchKeysDanger();
            var strCol = ArrumarColunas(colunas);

            var command = $"select {strCol} from {tabela}";

            command.SearchKeysDanger();

            var data = new CarregaTabelasVertica(TipoConexao.Conexao.Classe,
                System.Configuration.ConfigurationManager.ConnectionStrings["ContextVertica"]
                .ConnectionString.ToString()).SqlCommandTable(command);

            ViewBag.DefinicaoFiltros = data.Create().Serialize();
            ViewBag.Tabelas = data.DataTableToHtmlTable(
                new TableCssOptions()
                {
                    TableCss = "customers"
                });
            ViewBag.Tab = tabela;
            ViewBag.Cols = strCol;

            return View();
        }

        [HttpPost]
        public JsonResult Renderize()
        {
            var command = Request.InputStream
                .StreamToString()
                .GetSqlCommand(Request.Headers.RequestHeader("tabela"),
                    Request.Headers.RequestHeader("campos"));

            return Json(new CarregaTabelasVertica(TipoConexao.Conexao.Classe,
                System.Configuration.ConfigurationManager.ConnectionStrings["ContextVertica"]
                .ConnectionString.ToString()).SqlCommandTable(command).DataTableToHtmlTable(
                new TableCssOptions()
                {
                    TableCss = "customers"
                }),
                JsonRequestBehavior.AllowGet);
        }

        private string CriarHtmlA(string coluna)
        {
            //return $" CONCAT('<a href=\"/Builder/Colunas?Tabela=\',{coluna}, '\">Selecionar<a>') As Post ";
            return $" CONCAT(CONCAT('<a href=\"/Builder/Colunas?Tabela=\',{coluna}), '\">Selecionar<a>')  As Post";
        }

        private static string CriarHtmlInput(string coluna)
        {
            //return $" Concat('<input type=\"checkbox\" id=\"_', {coluna}, '\"name=\"', {coluna}, '\">') As Escolher ";
            return $" CONCAT(CONCAT(CONCAT(CONCAT('<input type=\"checkbox\" id=\"_', {coluna}), '\"name=\"'),{coluna} ),'\">') As Escolher";
        }

        private string ArrumarColunas(string colunas)
        {
            string cols = colunas.Replace("[", "").Replace("\"", "").Replace("]", "").Replace(",", ", ");
            return string.IsNullOrEmpty(cols) ? "*" : cols;
        }
    }
}