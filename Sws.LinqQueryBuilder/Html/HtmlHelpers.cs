using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sws.LinqQueryBuilder.Html
{
    public static class HtmlHelpers
    {
        public static string DataTableToHtmlTable(this DataTable dt, TableCssOptions options = null)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"<table{options?.GetTableCss()} id=\"tb1\">");
            sb.Append($"<thead{options?.GetTBodyCss()}>");
            sb.Append($"<tr>");

            for (int i = 0; i < dt.Columns.Count; i++)
                sb.Append("<th>" + dt.Columns[i].ColumnName + "</th>");

            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr>");
                for (int j = 0; j < dt.Columns.Count; j++)
                    sb.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
    }
}
