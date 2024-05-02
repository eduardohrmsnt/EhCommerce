using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string FormatWith(this string str, params object[] args) 
        { 
            return string.Format(str, args);
        }
    }
}
