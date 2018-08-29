using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeoLib.Core
{
    public static class SimpleMapper
    {
        public static void PropertyMap<T1, T2>(T1 source, T2 destination)
            where T1 : class, new()
            where T2 : class, new()
        {
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                // ReSharper disable once InvertIf
                if (destinationProperty != null)
                {
                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }
        }
    }
}
