using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sws.LinqQueryBuilder.Html
{
    public class TableCssOptions
    {
        public string TableCss { get; set; } = "";

        public string TBodyCss { get; set; } = "";

        internal string GetTableCss()
        {
            return string.IsNullOrEmpty(TableCss) ? "" : $" class=\"{TableCss}\" ";
        }

        internal string GetTBodyCss()
        {
            return string.IsNullOrEmpty(TBodyCss) ? "" : $" class=\"{TBodyCss}\" ";
        }
    }
}
