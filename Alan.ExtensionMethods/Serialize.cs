using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Data;

namespace Alan.ExtensionMethods
{
    public static class Serialize
    {
        public static string eToJson(this object t)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(t);
        }

        public static string eToJson(this DataTable table)
        {
            if (table.eIsNull()) return null;

            var list = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn column in table.Columns)
                {
                    dict.Add(column.ColumnName, row[column]);
                }
                list.Add(dict);
            }
            return list.eToJson();
        }


        public static T eToEntity<T>(this string t)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(t);
        }
    }
}
