using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    public static class Utility
    {
        public static string LengthenString(string text)
        {
            if (text.Length > 32) return text;
            for (int i = text.Length; i <= 32; i++)
            {
                text += " ";
            }
            return text;
        }
    }
}
