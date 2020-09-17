using System;
using System.Collections.Generic;
using System.Linq;

namespace HungryPizza.Infra.CrossCutting.Tools
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var item in values)
            {
                action(item);
            }
        }

        public static bool HasValue(this string value)
        {
            return !(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
        }

        public static string OnlyNumbers(this string value) => value.HasValue() ? new string(value.Where(c => char.IsNumber(c)).ToArray()) : string.Empty;
    }
}
