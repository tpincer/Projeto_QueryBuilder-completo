using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Sws.LinqQueryBuilder
{
    public static class GenerateFilters
    {
        public static IList<Filters> Create(this Type source, bool camelCase = false)
        {
            IList<Filters> filters = new List<Filters>();
            PropertyInfo[] properties = source.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetCustomAttribute(typeof(IgnoreDataMemberAttribute)) != null) continue;

                filters.Add(AddFiltersList(property.Name, property.PropertyType, camelCase));
            }

            return filters;
        }

        public static IList<Filters> Create<TSource>(this TSource source, bool camelCase = false) where TSource : class
        {
            return Create(source.GetType());
        }

        public static IList<Filters> Create(this DataTable table, bool camelCase = false)
        {
            IList<Filters> filters = new List<Filters>();

            foreach (DataColumn column in table.Columns)
                filters.Add(AddFiltersList(column.ColumnName, column.DataType, camelCase));

            return filters;
        }

        private static Filters AddFiltersList(string name, Type type, bool camelCase)
        {
            return new Filters
            {
                Label = camelCase ? name.ToCamelCase() : name,
                Field = name,
                Type = type.JQueryType(),
                Id = name
            };
        }

        public static string Serialize(this IList<Filters> source)
        {
            return JsonConvert.SerializeObject(source, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public class Filters
        {
            public Filters()
            {
                PrettyOutputTransformer = o => o;
            }
            public string Label { get; set; }
            public string Field { get; set; }
            public string Type { get; set; }
            public string Input { get; set; }
            public bool? Multiple { get; set; }
            public object Values { get; set; }
            public List<string> Operators { get; set; }
            public string Template { get; set; }
            public bool? ItemBankNotFilterable { get; set; }
            public bool? ItemBankNotColumn { get; set; }
            public Func<object, object> PrettyOutputTransformer { get; set; }
            public string Id { get; set; }
            public string Plugin { get; set; }
        }

    }
}
