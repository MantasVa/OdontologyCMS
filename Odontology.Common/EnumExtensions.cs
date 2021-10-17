using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Odontology.Common
{
    public static class EnumExtensions
    {
        private static readonly ConcurrentDictionary<MemberInfo, DisplayAttribute> DisplayAttributeCache = new ConcurrentDictionary<MemberInfo, DisplayAttribute>();

        public static string ToDisplayName(this Enum value)
        {
            var displayAttribute = value.GetDisplayAttribute();

            if (displayAttribute?.ResourceType != null)
                return LookupResource(displayAttribute.ResourceType, displayAttribute.Name);

            return displayAttribute?.Name ?? value.ToString();
        }

        private static DisplayAttribute GetDisplayAttribute<T>(this T value)
            => value.GetType().GetField(value.ToString()).GetCachedDisplayAttribute();

        private static DisplayAttribute GetCachedDisplayAttribute(this MemberInfo memberInfo) 
            => DisplayAttributeCache.GetOrAdd(memberInfo, (memberInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[]).FirstOrDefault());

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }
            return resourceKey;
        }
    }
}
