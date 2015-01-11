using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dayspent.Core.Utils
{
    public static class StringParserExtensions
    {
        public static string[] GetHashTags(this string text)
        {
            var result = new List<string>();
            var regex = new Regex(@"(?<=#)\w+");
            var matches =  regex.Matches(text);

            foreach (Match m in matches)
            {
                result.Add(m.Value);
                
            }
            return result.ToArray();

            
        }
    }
}
