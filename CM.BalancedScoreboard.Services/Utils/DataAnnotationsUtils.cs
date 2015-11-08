using CM.BalancedScoreboard.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace CM.BalancedScoreboard.Services.Utils
{
    public static class DataAnnotationsUtils
    {
        //public static Dictionary<string,string> GetDisplayNameAttributeValue<T>() where T : IViewModel
        //{
        //    Dictionary<string, string> propertyDisplayNames = null;
        //    foreach (var property in GetPropertiesOfTypeWithCustomAttribute<T, DisplayAttribute>())
        //    {
        //        if (propertyDisplayNames == null)
        //            propertyDisplayNames = new Dictionary<string, string>();

        //        propertyDisplayNames.Add(property.Name, GetCustomAttribute<RangeAttribute>(property).);
        //    }

        //    return propertyDisplayNames;
        //}

        //private static AT GetCustomAttribute<AT>(PropertyInfo property) where AT : Attribute
        //{
        //    return property.GetCustomAttribute<AT>();
        //}

        //private static IEnumerable<PropertyInfo> GetPropertiesOfTypeWithCustomAttribute<T, AT>() where T : IViewModel where AT : Attribute
        //{
        //    return typeof(T).GetProperties().Where(p => p.GetCustomAttribute<AT>() != null);
        //}

        public static Dictionary<string, Dictionary<string, object>> GetObjectAttributes<T>() where T : IViewModel
        {
            Dictionary<string, Dictionary<string, object>> typeAttributeValues = null; 
            foreach (var property in typeof(T).GetProperties())
            {
                if (typeAttributeValues == null)
                    typeAttributeValues = new Dictionary<string, Dictionary<string, object>>();

                var propertyAttributes = GetPropertyAttributes(property);
                if (propertyAttributes != null)
                    typeAttributeValues.Add(property.Name, propertyAttributes);
            }
            return typeAttributeValues;
        }

        private static Dictionary<string, object> GetPropertyAttributes(PropertyInfo property)
        {
            Dictionary<string, object> propertyAttributeValues = null;
            foreach (var attribute in property.GetCustomAttributes())
            {
                if (propertyAttributeValues == null)
                    propertyAttributeValues = new Dictionary<string, object>();

                if (attribute is DisplayAttribute)
                {
                    var displayAttribute = (DisplayAttribute)attribute;
                    var resourceManager = new ResourceManager("CM.BalancedScoreboard.Resources.Resources", displayAttribute.ResourceType.Assembly);
                    propertyAttributeValues.Add("DisplayName", resourceManager.GetString(displayAttribute.Name));
                }
                if (attribute is StringLengthAttribute)
                {
                    propertyAttributeValues.Add("MaxLength", ((StringLengthAttribute)attribute).MaximumLength);
                }
                if (attribute is RangeAttribute)
                {
                    propertyAttributeValues.Add("Range", new {
                        MinValue = ((RangeAttribute)attribute).Minimum,
                        MaxValue = ((RangeAttribute)attribute).Maximum
                    } );
                }
            }
            return propertyAttributeValues;
        }
    }
}
