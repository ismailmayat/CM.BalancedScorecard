using CM.BalancedScoreboard.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CM.BalancedScoreboard.Services.Utils
{
    public static class DataAnnotationsUtils
    {
        public static Dictionary<string,string> GetDisplayNameAttributeValue<T>() where T : IViewModel
        {
            Dictionary<string, string> propertyDisplayNames = null;
            foreach (var property in GetPropertiesOfTypeWithCustomAttribute<T, DisplayNameAttribute>())
            {
                if (propertyDisplayNames == null)
                    propertyDisplayNames = new Dictionary<string, string>();

                propertyDisplayNames.Add(property.Name, GetCustomAttribute<DisplayNameAttribute>(property).DisplayName);
            }

            return propertyDisplayNames;
        }

        private static AT GetCustomAttribute<AT>(PropertyInfo property) where AT : Attribute
        {
            return property.GetCustomAttribute<AT>();
        }

        private static IEnumerable<PropertyInfo> GetPropertiesOfTypeWithCustomAttribute<T, AT>() where T : IViewModel where AT : Attribute
        {
            return typeof(T).GetProperties().Where(p => p.GetCustomAttribute<AT>() != null);
        }
    }
}
