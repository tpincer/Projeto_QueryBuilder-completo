using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sws.LinqQueryBuilder
{
    public static class QueryBuilder
    {
        public static string GetSqlCommand(this string value, string tableName, string parans)
        {
            return $"select {parans.ToFriendlySpacedCol()} from {tableName} where {value.SearchKeysDanger()}";
        }
    }
}
