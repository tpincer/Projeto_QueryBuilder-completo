using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sws.LinqQueryBuilder
{
    public static class ConvertTypes
    {
        public static string JQueryType(this Type type)
        {
            var typeReal = type.RemoveUndelineType();

            switch (typeReal.Name.ToLower())
            {
                case string str when str == "decimal" || str == "double":
                    return "double";
                case string str when str == "int32" || str == "int64":
                    return "integer";
                case "datetime":
                    return "date";
                case "string":
                    return "string";
                case "boolean":
                    return "boolean";
            }

            throw new Exception("Unmapped data type");
        }

        public static Type RemoveUndelineType(this Type type)
        {
            if (type.IsGenericType)
                return Nullable.GetUnderlyingType(type);
            return type;
        }
    }
}
