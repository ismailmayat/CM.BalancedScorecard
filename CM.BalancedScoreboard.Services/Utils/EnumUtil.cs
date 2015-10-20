using System;
using System.Collections.Generic;

namespace CM.BalancedScoreboard.Services.Utils
{
    public static class EnumUtil
    {
        public static IEnumerable<string> GetNames<T>()
        {
            return Enum.GetNames(typeof(T));
        }

        public static IEnumerable<object> GetOptions<T>()
        {
            var options = new List<object>();
            foreach (Enum element in Enum.GetValues(typeof(T)))
            {
                options.Add(new
                {
                    id = element,
                    name = element.ToString()
                });
            }

            return options;
        } 
    }
}
