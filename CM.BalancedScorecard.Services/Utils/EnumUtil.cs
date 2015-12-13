using CM.BalancedScorecard.Resources.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CM.BalancedScorecard.Services.Utils
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

    public class Option
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
