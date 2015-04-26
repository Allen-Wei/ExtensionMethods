using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alan.ExtensionMethods
{
    public static class BasicConvert
    {

        public static T eParse<T>(this object obj)
        {
            T value;
            try
            {
                value = (T)Convert.ChangeType(obj, typeof(T));
            }
            catch
            {
                value = default(T);
            }
            return value;
        }

        public static T eStringParse<T>(this string value)
        {
            var result = default(T);
            try
            {
                if (value.eIsSpace()) return result;
                var tc = TypeDescriptor.GetConverter(typeof(T));
                result = (T)tc.ConvertFrom(value);
            }
            catch { }
            return result;
        }

        public static DateTime eToDate(this object t, DateTime whenFail = new DateTime())
        {
            if (t.eIsNull()) return whenFail;
            DateTime.TryParse(t.ToString(), out whenFail);
            return whenFail;
        }


        public static int eToInt(this object current, int whenFail = 0)
        {
            if (current.eIsNull()) return whenFail;

            var str = current.ToString();
            if (str.eIsEmpty()) return whenFail;

            int intVal;
            return int.TryParse(str, out intVal) ? intVal : whenFail;
        }

        public static double eToDouble(this object current, double whenFail = 0d)
        {
            if (current.eIsNull()) return whenFail;

            var str = current.ToString();
            if (str.eIsEmpty()) return whenFail;

            double dbVal;
            return double.TryParse(str, out dbVal) ? dbVal : whenFail;
        }

        public static float eToFloat(this object current, float whenFail = 0)
        {
            if (current.eIsNull()) return whenFail;

            var str = current.ToString();
            if (str.eIsEmpty()) return whenFail;

            float floatVal;
            return float.TryParse(str, out floatVal) ? floatVal : whenFail;
        }


        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        public static List<T> eToInstances<T>(this DataTable table)
        {
            var results = table.Rows.OfType<DataRow>().Select(row => row.eToInstance<T>()).ToList();
            return results;
        }

        public static T eToInstance<T>(this DataRow row)
        {
            var instance = Activator.CreateInstance<T>();
            instance.GetType()
                .GetProperties()
                .Where(p => p.CanWrite)
                .ForEach(property =>
                {
                    try
                    {
                        var columnValue = row[property.Name];
                        if (columnValue != null)
                        {
                            property.SetValue(instance, columnValue, null);
                        }
                    }
                    catch { }
                }
                );
            return instance;
        }

        public static void eSetValuesExclude(this object entity, object target, params string[] excludes)
        {
            entity.GetType().GetProperties().Where(prop => !excludes.Contains(prop.Name)).ForEach(prop => prop.SetValue(entity, target.eGetValue(prop.Name), null));
        }

        public static void eSetValuesInclude(this object entity, object target, params string[] includes)
        {
            entity.GetType().GetProperties().Where(prop => includes.Contains(prop.Name)).ForEach(prop => prop.SetValue(entity, target.eGetValue(prop.Name), null));
        }
        public static object eGetValue<T>(this T entity, string propertyName)
        {
            var property = entity.GetType().GetProperties().FirstOrDefault(prop => prop.Name == propertyName);
            if (property == null) return null;
            return property.GetValue(entity, null);
        }

        public static void eSetValue<T>(this T entity, string propertyName, object value)
        {
            var property = entity.GetType().GetProperties().FirstOrDefault(prop => prop.Name == propertyName);
            if (property == null) return;
            property.SetValue(entity, value, null);
        }

    }
}
