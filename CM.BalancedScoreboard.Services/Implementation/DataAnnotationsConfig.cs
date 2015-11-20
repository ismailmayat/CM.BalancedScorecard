using CM.BalancedScoreboard.Resources;
using CM.BalancedScoreboard.Resources.Abstract;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CM.BalancedScoreboard.Services.Utils;

namespace CM.BalancedScoreboard.Services.Implementation
{
    public class DataAnnotationsConfig : ITypeConfig
    {
        readonly IResourceManager _resourceManager;

        enum AttributeName
        {
            Name,
            DisplayName,
            Required,
            MaxLength,
            Range,
            ErrorMessage,
            InputType,
            Options
        }

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
            var propertyAttribute = new Dictionary<string, object>()
            {
                { AttributeName.Name.ToString(), property.Name}
            };

            foreach (var attribute in property.GetCustomAttributes())
            { 
                var displayAttribute = attribute as DisplayAttribute;
                if (displayAttribute != null)
                {
                    propertyAttribute.Add(AttributeName.DisplayName.ToString(), _resourceManager.GetString(displayAttribute.Name));
                    continue;
                }

                var customDataTypeAttribute = attribute as CustomDataTypeAttribute;
                if (customDataTypeAttribute != null)
                {
                    propertyAttribute.Add(AttributeName.InputType.ToString(), GenerateInputType(customDataTypeAttribute));
                    continue;
                }

                var enumDataTypeAttribute = attribute as EnumDataTypeAttribute;
                if (enumDataTypeAttribute != null)
                {
                    var options = typeof(EnumUtil<>).MakeGenericType(enumDataTypeAttribute.EnumType).GetMethod("GetOptions").Invoke(this, new object[] { _resourceManager });
                    propertyAttribute.Add(AttributeName.Options.ToString(), options);
                }

                var dataTypeAttribute = attribute as DataTypeAttribute;
                if (dataTypeAttribute != null)
                {
                    propertyAttribute.Add(AttributeName.InputType.ToString(), dataTypeAttribute.DataType.ToString().ToLower());
                    continue;
                }

                var requiredAttribute = attribute as RequiredAttribute;
                if (requiredAttribute != null)
                {
                    propertyAttribute.Add(AttributeName.Required.ToString(), true);
                    if (!string.IsNullOrEmpty(requiredAttribute.ErrorMessageResourceName))
                    {
                        GenerateErrorMessage(requiredAttribute, AttributeName.Required, propertyAttribute);
                    }
                    continue;
                }

                var lengthAttribute = attribute as StringLengthAttribute;
                if (lengthAttribute != null)
                {
                    propertyAttribute.Add(AttributeName.MaxLength.ToString(), lengthAttribute.MaximumLength);
                    continue;
                }

                var rangeAttribute = attribute as RangeAttribute;
                if (rangeAttribute != null)
                {
                    propertyAttribute.Add(AttributeName.Range.ToString(), new {
                        MinValue = rangeAttribute.Minimum,
                        MaxValue = rangeAttribute.Maximum
                    } );
                }
            }
            return propertyAttribute;
        }

        public string GenerateInputType(CustomDataTypeAttribute attribute)
        {
            var cDataType = (CDataType)Enum.Parse(typeof(CDataType), attribute.CustomDataType);
            switch (cDataType)
            {
                case CDataType.Month:
                    return "month";
                case CDataType.Range:
                    return "range";
                case CDataType.YesNo:
                    return "checkbox";
                case CDataType.Number:
                    return "number";
                default:
                    return "text";
            }
        }

        private void GenerateErrorMessage(ValidationAttribute attribute, AttributeName attributeName, Dictionary<string, object> propertyAttributes)
        {
            if (!propertyAttributes.ContainsKey(AttributeName.ErrorMessage.ToString()))
                propertyAttributes.Add(AttributeName.ErrorMessage.ToString(), new Dictionary<string, string>());

            var errorMessages = (Dictionary<string,string>)propertyAttributes[AttributeName.ErrorMessage.ToString()];
            errorMessages.Add(attributeName.ToString(), _resourceManager.GetString(attribute.ErrorMessageResourceName));
        }
    }
}
