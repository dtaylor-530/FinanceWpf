
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;


using System.Reflection;

using System.Linq.Expressions;

using System.ComponentModel;


namespace FinanceWpf.Common
{




    public static class TypeExtensions
    {



        //Stack Overflow nawfal Oct 9 '13 at 7:44
        public static MethodInfo GetGenericMethod(this Type type, string name, Type[] parameterTypes)
        {
            return type.GetMethods()
                 .FirstOrDefault(m =>
                 m.Name == name &&
                 m.GetParameters()
                 .Select(p => p.ParameterType)
                 .SequenceEqual(parameterTypes, new SimpleTypeComparer()));
        }

        //Stack Overflow Dustin Campbell answered Oct 27 '10 at 17:58
        private class SimpleTypeComparer : IEqualityComparer<Type>
        {
            public bool Equals(Type x, Type y)
            {
                return x.Assembly == y.Assembly &&
                    x.Namespace == y.Namespace &&
                    x.Name == y.Name;
            }

            public int GetHashCode(Type obj)
            {
                throw new NotImplementedException();
            }
        }





        //public static IEnumerable<Type> GetInheritingTypes(Type type)
        //{
        //    var x = AssemblyHelper.GetSolutionAssemblies();

        //    var sf = x.SelectMany(sd => sd.GetExportedTypes());

        //    var v = sf.Where(p => p.GetInterfaces().Any(t => t == type));

        //    return v;

        //}




        public static Type[] GetTypesInNamespace(this Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        public static string GetDescription(this MethodInfo value)
        {


            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])value.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }


        //public static class Class2
        //{

        //    public static IEnumerable<TSource> MinBy<TSource, TKey>(this IEnumerable<TSource> source,
        //     Func<TSource, TKey> selector)
        //    {
        //        return source.MinBy(selector, null);

        //    }

        //    // From MoreLinq
        //    public static IEnumerable<TSource> MinBy<TSource, TKey>(this IEnumerable<TSource> source,
        //   Func<TSource, TKey> selector, IComparer<TKey> comparer)
        //    {
        //        if (source == null) throw new ArgumentNullException(nameof(source));
        //        if (selector == null) throw new ArgumentNullException(nameof(selector));

        //        comparer = comparer ?? Comparer<TKey>.Default;
        //        return ExtremaBy(source, selector, (x, y) => -Math.Sign(comparer.Compare(x, y)));
        //    }


        //    // > In mathematical analysis, the maxima and minima (the respective
        //    // > plurals of maximum and minimum) of a function, known collectively
        //    // > as extrema (the plural of extremum), ...
        //    // >
        //    // > - https://en.wikipedia.org/wiki/Maxima_and_minima

        //    static IEnumerable<TSource> ExtremaBy<TSource, TKey>(IEnumerable<TSource> source,
        //        Func<TSource, TKey> selector, Func<TKey, TKey, int> comparer)
        //    {
        //        foreach (var item in Extrema())
        //            yield return item;

        //        IEnumerable<TSource> Extrema()
        //        {
        //            using (var e = source.GetEnumerator())
        //            {
        //                if (!e.MoveNext())
        //                    return new List<TSource>();

        //                var extrema = new List<TSource> { e.Current };
        //                var extremaKey = selector(e.Current);

        //                while (e.MoveNext())
        //                {
        //                    var item = e.Current;
        //                    var key = selector(item);
        //                    var comparison = comparer(key, extremaKey);
        //                    if (comparison > 0)
        //                    {
        //                        extrema = new List<TSource> { item };
        //                        extremaKey = key;
        //                    }
        //                    else if (comparison == 0)
        //                    {
        //                        extrema.Add(item);
        //                    }
        //                }

        //                return extrema;
        //            }
        //        }



        //    }
        //}
    }
}