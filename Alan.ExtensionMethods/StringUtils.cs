using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alan.ExtensionMethods
{
    public static class StringUtils
    {
        public static bool eIsEmpty(this string str) { return String.IsNullOrEmpty(str); }
        public static bool eIsNotEmpty(this string str) { return !String.IsNullOrEmpty(str); }
        public static string eIfEmpty(this string str, string whenTrue = "")
        {
            return str.eIsEmpty() ? whenTrue : str;
        }

        public static bool eIsSpace(this string t)
        {
            return String.IsNullOrWhiteSpace(t);
        }
        public static bool eIsNotSpace(this string t)
        {
            return !String.IsNullOrWhiteSpace(t);
        }

        public static string eIfSpace(this string t, string whenTrue = "")
        {
            return t.eIsSpace() ? whenTrue : t;
        }


        public static bool eLastIs(this string t, string lastVal)
        {
            if (t.eIsEmpty() || lastVal.eIsEmpty()) return false;
            int index = t.LastIndexOf(lastVal);
            if (index == -1) return false;
            return t.Length == (lastVal.Length + index);

        }
        public static string eIfLastIs(this string t, string lastVal)
        {
            bool isLast = t.eLastIs(lastVal);
            if (isLast) return t;
            else return t + lastVal;
        }

    }


}
