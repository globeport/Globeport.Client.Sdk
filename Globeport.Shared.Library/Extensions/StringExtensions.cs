using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Extensions
{
    public static class StringExtensions
    {
        public static string Right(this string source, int length)
        {
            return source.Substring(source.Length - length, length);
        }

        public static string Left(this string source, int length)
        {
            return source.Substring(0, Math.Min(length, source.Length));
        }

        public static string Join(this IEnumerable<string> items, string delimiter = ",", string prefix = "'", string postfix = "'")
        {
            return string.Join(delimiter, items.Select(i => $"{prefix}{i}{postfix}"));
        }

        public static string RemoveFirst(this string source, string remove)
        {
            int index = source.IndexOf(remove);
            return (index < 0)
                ? source
                : source.Remove(index, remove.Length);
        }

        public static string RemoveStart(this string source, string remove)
        {
            int index = source.IndexOf(remove);
            return (index == 0)
                ? source.Remove(index, remove.Length)
                : source;
        }

        public static string RemoveExtension(this string source)
        {
            int index = source.LastIndexOf(".");
            return (index >= 0)
                ? source.Substring(0, index)
                : source;
        }

        public static string RemovePrefix(this string source)
        {
            int index = source.LastIndexOf(".");
            return (index >= 0)
                ? source.Substring(index + 1, source.Length - index - 1)
                : source;
        }

        public static string AddIndexer(this string source, string index)
        {
            return source + "[" + index + "]";
        }

        public static string RemoveIndexer(this string source)
        {
            var index = source.LastIndexOf('[');
            if (index > 0) return source.Substring(0, index);
            return source;
        }

        public static string GetIndexer(this string source)
        {
            var index = source.LastIndexOf('[');
            if (index > 0) return source.Substring(index + 1, source.Length - index - 2);
            return null;
        }

        public static bool HasIndexer(this string source)
        {
            int index = source.LastIndexOf("[");
            return (index > 0 && index < source.Length - 1);
        }

        public static string RemoveEnd(this string source, string remove)
        {
            int index = source.LastIndexOf(remove);
            return (index >= 0 && index + remove.Length == source.Length)
                ? source.Substring(0, index)
                : source;
        }

        public static bool HasExtension(this string source, string extension)
        {
            int index = source.LastIndexOf(".");
            return (index >= 0)
                ? (source.Substring(index + 1, source.Length - index - 1) == extension)
                : false;
        }

        public static bool HasExtension(this string source)
        {
            int index = source.LastIndexOf(".");
            return (index > 0 && index < source.Length - 1);
        }

        public static string GetExtension(this string source)
        {
            int index = source.LastIndexOf(".");
            if (index > 0) return source.Substring(index + 1, source.Length - index - 1);
            return null;
        }

        public static string AddExtension(this string source, string extension)
        {
            return source + "." + extension;
        }

        public static IEnumerable<string> SplitLines(this string source)
        {
            return source.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool IsEmpty(this string source)
        {
            return source == null || source == string.Empty;
        }

        public static byte[] ToBytes(this string source)
        {
            return System.Text.Encoding.UTF8.GetBytes(source.ToCharArray());
        }

        public static string FromBytes(this byte[] source)
        {
            return System.Text.Encoding.UTF8.GetString(source, 0, source.Length);
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return source == null || source == string.Empty;
        }

        public static T IfNotNullOrEmpty<T>(this string source, Func<string, T> func)
        {
            if (!source.IsNullOrEmpty()) return func(source);
            return default(T);
        }

        public static void ProcessCsv(this string file, Action<string[]> processLineValues, char[] valueSeparator = null, char[] lineSeparator = null)
        {
            var lines = file.Split(lineSeparator ?? ValueSeparators.NewLine);
            foreach (var line in lines.Where(i => !i.IsEmpty()))
            {
                var values = line.Split(valueSeparator ?? ValueSeparators.Comma);
                processLineValues(values);
            }
        }

        public static string[] SplitCsv(this string source)
        {
            if (source.IsEmpty()) return new string[] { };
            return source.Split(ValueSeparators.Comma);
        }

        public static string JoinCsv(this IEnumerable<string> source)
        {
            if (source == null) return string.Empty;
            return string.Join(",", source);
        }

        public static string SplitPascal(this string source)
        {
            return Regex.Replace(source, "(\\B[A-Z])", " $1");
        }

        public static string FormatString(this string source, params object[] args)
        {
            return string.Format(source, args);
        }

        public static string FormatString(this string value, IDictionary<string, string> data)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var sb = new StringBuilder();
            var key = new StringBuilder();

            var inBraces = false;
            var skipClose = false;

            foreach (var ch in value)
            {
                if (inBraces)
                {
                    if (ch == '{')
                    {
                        if (key.Length <= 0)
                        {
                            inBraces = false;
                            sb.Append('{');
                        }
                        else
                            key.Append(ch);
                    }
                    else if (ch == '}')
                    {
                        inBraces = false;

                        string item;

                        if (ReferenceEquals(null, data))
                            throw new ArgumentNullException("data");
                        else if (!data.TryGetValue(key.ToString(), out item))
                            throw new FormatException("Key {" + key.ToString() + "} not found");
                        else if (!ReferenceEquals(null, item))
                            sb.Append(item.ToString());

                        key.Clear();
                    }
                    else
                        key.Append(ch);
                }
                else if (ch == '{')
                {
                    inBraces = true;
                    skipClose = true;
                }
                else if (ch == '}')
                    if (!skipClose)
                    {
                        sb.Append(ch);
                        skipClose = true;
                    }
                    else
                        skipClose = false;
                else
                {
                    sb.Append(ch);
                    skipClose = false;
                }
            }

            if (inBraces)
                throw new FormatException("Unclosed } in the string.");

            return sb.ToString();
        }

        public static string RemoveNewLines(this string source)
        {
            return source.Replace("\n", "").Replace("\r", "");
        }

        public static bool IsUrl(this string source)
        {
            return source.StartsWith("http://") || source.StartsWith("https://");
        }

        public static string ReplaceFirst(this string source, string search, string replace)
        {
            int pos = source.IndexOf(search);
            if (pos < 0) return source;
            return $"{source.Substring(0, pos)}{replace}{source.Substring(pos + search.Length)}";
        }

        public static string ReplaceLast(this string source, string search, string replace)
        {
            int pos = source.LastIndexOf(search);
            if (pos < 0) return source;
            return source.Remove(pos, search.Length).Insert(pos, replace);
        }

        public static string[] Split(this string str, string splitter)
        {
            return str.Split(new[] { splitter }, StringSplitOptions.None);
        }

        public static bool IsInteger(this string str)
        {
            int temp;
            return int.TryParse(str, out temp);
        }

        public static int GetStableHashCode(this string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

        public static string Overwrite(this string text, int position, int length, string newText)
        {
            return $"{text.Substring(0, position)}{newText}{text.Substring(position + length)}";
        }
    }
}
