using CM.BalancedScoreboard.Resources.Abstract;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System;
using System.Globalization;
using System.Collections;

namespace CM.BalancedScoreboard.Resources.Implementation
{
    public class LocalResourceManager : IResourceManager
    {
        readonly ResourceManager _resourceManager;

        public LocalResourceManager(string resourceNamespace)
        {
            _resourceManager =  new ResourceManager(resourceNamespace, Assembly.GetExecutingAssembly());
        }

        public string GetString(string name)
        {
            return _resourceManager.GetString(name);
        }

        public Dictionary<string, string> GetStrings()
        {
            var strings = new Dictionary<string, string>();
            foreach (DictionaryEntry resource in _resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true))
            {
                strings.Add(resource.Key.ToString(), resource.Value.ToString());
            }
            return strings;
        }
    }
}
