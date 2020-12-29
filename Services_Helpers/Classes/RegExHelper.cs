using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Hit.Services.Helpers.Classes.Classes
{
    public class RegExHelper 
    {

        #region "Basic Methods"
        /// <summary>
        /// Match a string against a pattern using Regular Expresion.
        /// <para/>Example:
        /// <para/>   string InputString = "server=1234567;persist security info=True;user id=aaa;initial catalog=my34db;password=qw#4e9fk;";
        /// <para/>   GetString(InputString, "server=(?&#60;server&#62;\\w+);", "server"));
        /// <para/>   Return: 1234567
        /// </summary>
        /// <param name="InputString">Input String</param>
        /// <param name="RegPattern">pattern</param>
        /// <param name="key">key</param>
        /// <returns>the value of key</returns>
        public string GetString(string InputString, string RegPattern, string key)
        {
            Regex r = default(Regex);
            Match m = default(Match);

            r = new Regex(RegPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            m = r.Match(InputString);
            while (m.Success)
            {
                return m.Groups[key].ToString();
               // m = m.NextMatch();
            }
            return "";
        }

        /// <summary>
        /// Match a string against a pattern using Regular Expresion (for many occurrences).
        /// <para/>Example:
        /// <para/>   string InputString = "server=1234567;persist security info=True;user id=aaa;initial catalog=my34db;password=qw#4e9fk;";
        /// <para/>   GetStrings(InputString, "server=(?&#60;server&#62;\\w+);", "server"));
        /// <para/>   Return: 1234567
        /// </summary>
        /// <param name="InputString">Input String</param>
        /// <param name="RegPattern">pattern</param>
        /// <param name="key">key</param>
        /// <returns>the values of key</returns>
        public List<string> GetStrings(string InputString, string RegPattern, string key)
        {
            List<string> results = new List<string>();
            Regex r = default(Regex);
            Match m = default(Match);

            r = new Regex(RegPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            m = r.Match(InputString);
            while (m.Success)
            {
                results.Add(m.Groups[key].ToString());
                m = m.NextMatch();
            }
            return results;
        }

        #endregion


    }
}
