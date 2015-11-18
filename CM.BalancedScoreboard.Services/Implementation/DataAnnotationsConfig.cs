using CM.BalancedScoreboard.Resources;
using CM.BalancedScoreboard.Resources.Abstract;
using CM.BalancedScoreboard.Services.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CM.BalancedScoreboard.Services.Implementation
{
    public class DataAnnotationsConfig : ITypeConfig
    {
        readonly IResourceManager _resourceManager;

        public DataAnnotationsConfig(IResourceFactory resourceFactory)
        {
            _resourceManager = resourceFactory.GetResourceManager(ResourceType.Indicators);
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
            Dictionary<string, object> propertyAttributeValues = new Dictionary<string, object>()
            {
                { "Name", property.Name}
            };

            foreach (var attribute in property.GetCustomAttributes())
            {
                if (propertyAttributeValues == null)
                    propertyAttributeValues = new Dictionary<string, object>();

                var requiredAttribute = attribute as RequiredAttribute;
                if (requiredAttribute != null)
                {
                    propertyAttributeValues.Add("Required", true);
                    continue;
                }

                var displayAttribute = attribute as DisplayAttribute;
                if (displayAttribute != null)
                {
                    propertyAttributeValues.Add("DisplayName", _resourceManager.GetString(displayAttribute.Name));
                    continue;
                }

                var lengthAttribute = attribute as StringLengthAttribute;
                if (lengthAttribute != null)
                {
                    propertyAttributeValues.Add("MaxLength", lengthAttribute.MaximumLength);
                    continue;
                }
                var rangeAttribute = attribute as RangeAttribute;
                if (rangeAttribute != null)
                {
                    propertyAttributeValues.Add("Range", new {
                        MinValue = rangeAttribute.Minimum,
                        MaxValue = rangeAttribute.Maximum
                    } );
                    continue;
                }
            }
            return propertyAttributeValues;
        }
    }
}
