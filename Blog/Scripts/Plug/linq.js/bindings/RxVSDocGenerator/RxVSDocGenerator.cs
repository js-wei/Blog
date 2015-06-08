using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

static class RxVSDocGenerator
{
    const string RootNamespace = "Rx";

    static class Template
    {
        /// <summary>0 - FullName</summary>
        public const string Object = @"{0} = {{}}";

        /// <summary>FullName, Parameters, Summary, Param</summary>
        public const string Class = @"
{FullName} = function({Parameters})
{
    /// <summary>{Summary}</summary>
{Param}
}";

        /// <summary>0 - FullName, 1- BaseFullName</summary>
        public const string Inheritance = @"{0}.prototype = new {1};";

        /// <summary>ClassName, MethodName, Parameters, Summary, Param, ReturnsType</summary>
        public const string InstanceFunction = @"
{ClassName}.prototype.{MethodName} = function({Parameters})
{
    /// <summary>{Summary}</summary>
{Param}
    /// <returns type='{ReturnsType}'></returns>
}";

        /// <summary>ClassName, MethodName, Parameters, Summary, Param, ReturnsType</summary>
        public const string StaticFunction = @"
{ClassName}.{MethodName} = function({Parameters})
{
    /// <summary>{Summary}</summary>
{Param}
    /// <returns type='{ReturnsType}'></returns>
}";

        /// <summary>0 - type, 1- name</summary>
        public const string Param = @"    /// <param type='{0}' name='{1}'></param>";

        /// <summary>0 - ClassName, 1- FieldName</summary>
        public const string InstanceField = @"{0}.prototype.{1} = null;";

        /// <summary>0 - ObjectName, 1- FieldName</summary>
        public const string ObjectField = @"{0}.{1} = null;";
    }

    static void Main()
    {
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            Console.WriteLine("Error:put RxJS.dll, RxJS.xml on same directory. or other problem ?");
            Console.WriteLine("press key");
            Console.ReadLine();
        };

      
      

    }


    // Utility Extensions

    static string ToJoinedString<T>(this IEnumerable<T> source, string separator)
    {
        var index = -1;
        return source.Aggregate(new StringBuilder(), (sb, s) =>
                (++index == 0) ? sb.Append(s) : sb.Append(separator).Append(s))
            .ToString();
    }

    static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) where TKey : IComparable
    {
        return source.Aggregate((x, y) => selector(x).CompareTo(selector(y)) > 0 ? x : y);
    }

    static string RegexReplace(this string input, string pattern, string replacement)
    {
        return Regex.Replace(input, pattern, replacement);
    }

    static string TemplateReplace(this string template, object replacement)
    {
        var dict = replacement.GetType().GetProperties()
            .ToDictionary(pi => pi.Name, pi => pi.GetValue(replacement, null).ToString());

        return Regex.Replace(template,
            "{(" + string.Join("|", dict.Select(kvp => Regex.Escape(kvp.Key)).ToArray()) + ")}",
            m => dict[m.Groups[1].Value]);
    }

   

  
}