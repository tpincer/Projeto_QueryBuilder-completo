using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sws.LinqQueryBuilder
{
    public static class SqlInjection
    {
        public static string SearchAll(this string value)
        {
            var text = value.ToUpper();

            if (text.IndexOf("WHERE") >= 0)
                throw new ArgumentException("the word 'WHERE' is not allowed");

            if (text.IndexOf("AND") >= 0)
                throw new ArgumentException("the word 'AND' is not allowed");

            if (text.IndexOf("OR") >= 0)
                throw new ArgumentException("the word 'OR' is not allowed");

            if (text.IndexOf("IN") >= 0)
                throw new ArgumentException("the word 'IN' is not allowed");

            if (text.IndexOf("BETWEEN") >= 0)
                throw new ArgumentException("the word 'BETWEEN' is not allowed");

            if (text.IndexOf("UNION") >= 0)
                throw new ArgumentException("the word 'UNION' is not allowed");

            if (text.IndexOf("JOIN") >= 0)
                throw new ArgumentException("the word 'JOIN' is not allowed");

            if (text.IndexOf("FROM") >= 0)
                throw new ArgumentException("the word 'FROM' is not allowed");

            if (text.IndexOf("SELECT") >= 0)
                throw new ArgumentException("the word 'SELECT' is not allowed");

            if (text.IndexOf("INNER") >= 0)
                throw new ArgumentException("the word 'INNER' is not allowed");

            return SearchKeysDanger(text);
        }

        public static string SearchKeysDanger(this string value)
        {
            var text = value.ToUpper();

            if (text.IndexOf("CREATE") >= 0)
                throw new ArgumentException("the word 'CREATE' is not allowed");

            if (text.IndexOf("DATABASE") >= 0)
                throw new ArgumentException("the word 'DATABASE' is not allowed");

            if (text.IndexOf("DROP") >= 0)
                throw new ArgumentException("the word 'DROP' is not allowed");

            if (text.IndexOf("UPDATE") >= 0)
                throw new ArgumentException("the word 'UPDATE' is not allowed");

            if (text.IndexOf("INSERT") >= 0)
                throw new ArgumentException("the word 'INSERT' is not allowed");

            if (text.IndexOf("ALTER") >= 0)
                throw new ArgumentException("the word 'ALTER' is not allowed");

            if (text.IndexOf("DELETE") >= 0)
                throw new ArgumentException("the word 'DELETE' is not allowed");

            return value;
        }
    }
}
