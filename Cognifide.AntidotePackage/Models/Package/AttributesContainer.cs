using System;
using System.Collections.Generic;
using System.Linq;

namespace Cognifide.AntidotePackage.Models.Package
{
    public class AttributesContainer
    {
        private Dictionary<string, object> Attributes { get; set; }

        public AttributesContainer()
        {
            Attributes = new Dictionary<string, object>();
        }

        public AttributesContainer(string packageAttributesString)
        {
            Attributes = new Dictionary<string, object>();
            string[] attributes = packageAttributesString.Split('|');

            foreach (var attribute in attributes.Where(x => !String.IsNullOrEmpty(x)))
            {
                string[] a = attribute.Split('=');
                AddAttribute(a[0], a[1]);
            }
        }

        public void AddAttribute(string key, string value)
        {
            Attributes.Add(key, value);
        }

        public object GetAttribute(string key)
        {
            return Attributes[key];
        }

        public bool RemoveAttribute(string key)
        {
            return Attributes.Remove(key);
        }

        public string ConvertoToPackageAttributes()
        {
            var list = Attributes.Select(attribute => String.Format("{0}={1}", attribute.Key, attribute.Value));
            return String.Join("|", list);
        }
    }
}
