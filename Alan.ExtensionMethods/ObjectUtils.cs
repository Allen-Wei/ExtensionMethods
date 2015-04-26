using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alan.ExtensionMethods
{
    public static class ObjectUtils
    {
        public static bool eIsNull(this object obj)
        {
            return obj == null || DBNull.Value == obj;
        }
        public static bool eIsNotNull(this object obj)
        {
            return !obj.eIsNull();
        }
        public static T eIfNull<T>(this T obj, T whenTrue)
        {
            return obj.eIsNull() ? whenTrue : obj;
        }
        public static bool eIsValueType<T>()
        {
            return typeof(T).IsValueType;
        }
        public static bool eIsValueType(this object obj)
        {
            return obj == null ? false : obj.GetType().IsValueType;
        }
    }
 
}
