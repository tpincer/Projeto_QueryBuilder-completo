using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sws.LinqQueryBuilder
{
    public static class Helpers
    {
        public static string ArrayToLine(this string[] value)
        {
            return string.Join(", ", value);
        }

        public static string ToFriendlySpacedCol(this string value)
        {
            return value.Replace(",", ", ");
        }

        public static string StreamToString(this Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            return new StreamReader(stream).ReadToEnd();
        }

        public static string RequestHeader(this NameValueCollection collection, string key)
        {
            return collection.Get(key);
        }

        public static string ToFriendlySpacedString(this string input)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            var res = r.Replace(input, " ");

            return res;
        }

        public static string ToCamelCase(this string input)
        {
            var s = input;
            if (!char.IsUpper(s[0])) return s;

            var cArr = s.ToCharArray();
            for (var i = 0; i < cArr.Length; i++)
            {
                if (i > 0 && i + 1 < cArr.Length && !char.IsUpper(cArr[i + 1])) break;
                cArr[i] = char.ToLowerInvariant(cArr[i]);
            }
            return new string(cArr);
        }
    }
}
