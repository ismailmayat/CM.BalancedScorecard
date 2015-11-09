using CM.BalancedScoreboard.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CM.BalancedScoreboard.Services.Utils
{
    public static class EnumUtil<T>
    {
        public static IEnumerable<string> GetNames()
        {
            return Enum.GetNames(typeof(T));
        }

        public static IEnumerable<object> GetOptions(IResourceManager resourceManager)
        {
            var options = new List<object>();
            foreach (Enum element in Enum.GetValues(typeof(T)))
            {
                options.Add(new
                {
                    id = element,
                    name = resourceManager.GetString(GetCustomAttributeValue<DisplayAttribute>(element).Name)
                });
            }

            return options;
        }

        private static TA GetCustomAttributeValue<TA>(Enum element) where TA : Attribute
        {
            return typeof (T).GetField(element.ToString()).GetCustomAttribute<TA>();
        }
    }
}
