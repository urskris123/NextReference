using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
	class Program
	{
        static void Main(string[] args)
        {
            List<string> Refs = new List<string>
                {
                "S1000112345", "SPk99999999", "SP39263456", "120013579","120013589" ,"SP00003456","S3126112345", "SPREFERENCE" , "250783579" , "391543579" , "130013579" ,"SK00009999", "Shiva8659403",
                "Tucson0001","T2001","89989T200111"
                };

            var refs = Refs.GroupBy(c => Regex.Match(c, "^[a-zA-Z0-9]*$").Value);
            //var refkeys = Refs.Select(x => x.Substring(0,x.LastIndexOf() Last GroupBy(x => Regex.Match(x, @"\[a-zA-Z]*d*").Value);



            return;
            List<Tuple<string, string, string>> ts = new List<Tuple<string, string, string>>();
            // Get Prefix and Post fix of given string 
            foreach (string refer in Refs)
            {
                (string perFix, string num, string refe) = GetDigit(refer);
                if (!string.IsNullOrEmpty(num))
                    ts.Add(new Tuple<string, string, string>(perFix, num, refe));
            }
            // Group by prefix and find next reference of group with max in group
            Dictionary<string, List<Tuple<string, string, string>>> groupedTuple = ts.GroupBy(e => e.Item1).ToDictionary(e => e.Key, e => e.ToList());
            foreach (var item in groupedTuple)
            {
                string postfix = item.Value.Select(x => Convert.ToInt64(x.Item2)).Max().ToString();
                (string perfix, string num, string refe) = item.Value.SingleOrDefault(x => x.Item2.EndsWith(postfix));
                postfix = (Convert.ToInt64(postfix) + 1).ToString(NumberFormatInfo.InvariantInfo).PadLeft(num.Length, '0');
                string prefix = item.Key;
                Console.WriteLine($"{refe}-->{perfix}{postfix}");
            }
        }
        // method split given with prefix and postfix if found char
        static (string, string, string) GetDigit(string refe)
        {
            string lastOf = string.Empty;
            string fistOf = string.Empty;

            foreach (char item in refe.ToCharArray().Reverse())
            {
                if (char.IsDigit(item) && string.IsNullOrEmpty(fistOf))
                {
                    lastOf = $"{item}{lastOf}";
                }
                else
                {
                    fistOf = $"{item}{fistOf}";
                }

            }
            return (fistOf, lastOf, refe);
        }

    }
}
