using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CM.BalancedScoreboard.Resources;
using CM.BalancedScoreboard.Services.Abstract;

namespace CM.BalancedScoreboard.Services.Implementation
{
    public class DataAnnotationsConfig : ITypeConfig
    {
        private readonly IResourceManager _resourceManager;

        public DataAnnotationsConfig(IResourceManager reourceManager)
        {
            _resourceManager = reourceManager;
        }

        public Dictionary<string, Dictionary<string, object>> GetAttributes<T>() where T : class
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

        private Dictionary<string, object> GetPropertyAttributes(MemberInfo property)
        {
            Dictionary<string, object> propertyAttributeValues = null;
            foreach (var attribute in property.GetCustomAttributes())
            {
                if (propertyAttributeValues == null)
                    propertyAttributeValues = new Dictionary<string, object>();

                var displayAttribute = attribute as DisplayAttribute;
                if (displayAttribute != null)
                {
                    propertyAttributeValues.Add("DisplayName", _resourceManager.GetString(displayAttribute.Name));
                }
                var lengthAttribute = attribute as StringLengthAttribute;
                if (lengthAttribute != null)
                {
                    propertyAttributeValues.Add("MaxLength", lengthAttribute.MaximumLength);
                }
                var rangeAttribute = attribute as RangeAttribute;
                if (rangeAttribute != null)
                {
                    propertyAttributeValues.Add("Range", new {
                        MinValue = rangeAttribute.Minimum,
                        MaxValue = rangeAttribute.Maximum
                    } );
                }
            }
            return propertyAttributeValues;
        }
    }
}
