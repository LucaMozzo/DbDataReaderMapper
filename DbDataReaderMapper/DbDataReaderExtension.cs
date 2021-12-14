using System.Linq;
using System.Data.Common;
using System.Reflection;
using System;
using DbDataReaderMapper.Exceptions;
using System.Collections.Generic;

namespace DbDataReaderMapper
{
    public static class DbDataReaderExtension
    {
        /// <summary>
        /// Maps the current row to the specified type
        /// </summary>
        /// <typeparam name="T">The type of the output object</typeparam>
        /// <param name="dataReader">The data source</param>
        /// <param name="customPropertyConverter">Use a custom converter for certain values</param>
        /// <returns>The object that contains the data in the current row of the reader</returns>
        public static T MapToObject<T>(this DbDataReader dataReader, CustomPropertyConverter customPropertyConverter = null) where T : class, new()
        {
            T obj = new T();
            PropertyInfo[] typeProperties = typeof(T).GetProperties();
            var customNameMappings = typeProperties
                .Where(tp => GetColumnAttribute(tp) != null)
                .ToDictionary(tp => GetColumnAttribute(tp), tp => tp);

            for (int i = 0; i < dataReader.FieldCount; ++i)
            {
                string columnName = dataReader.GetName(i);

                var mappedProperty = typeProperties.Where(tp => tp.Name.Equals(columnName)).FirstOrDefault();
                var mappedPropertyCustomName = customNameMappings.ContainsKey(columnName) ? customNameMappings[columnName] : null;

                if (IsAttributePropertyNamingClash(customNameMappings, columnName, mappedProperty, mappedPropertyCustomName))
                {
                    /*
                     * If the attribute has the same name as another property in the model that doesn't have a custom name, it causes a clash
                     */
                    throw new DbColumnMappingException($"Attribute {columnName} has the same name as a property defined in the model");
                }

                // the attribute name takes precedence over the property name
                var resolvedMappedProperty = mappedPropertyCustomName ?? mappedProperty;

                if (resolvedMappedProperty != null)
                {
                    var value = dataReader[columnName];
                    if (value is DBNull)
                    {
                        value = null;
                    }

                    try
                    {
                        if (customPropertyConverter != null && customPropertyConverter[resolvedMappedProperty] != null)
                        {
                            resolvedMappedProperty.SetValue(obj, customPropertyConverter[resolvedMappedProperty].DynamicInvoke(value));
                        }
                        else
                        {
                            resolvedMappedProperty.SetValue(obj, value);
                        }
                    }
                    catch
                    {
                        throw new InvalidCastException($"Expected type {resolvedMappedProperty.PropertyType} but found {value.GetType()} for property {columnName}");
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// Determines whether the attribute custom name clashes with a property
        /// </summary>
        /// <remarks>
        /// A clash happens in a scenario similar to this
        /// `[DbColumn("Name")]`
        /// `public string Address { get; set; }`
        /// `public string Name { get; set; }`
        /// because column `Name` Can map either to the first property or to the second one
        /// </remarks>
        /// <param name="customNameMappings">The dictionary of custom attribute names -> model property mappings</param>
        /// <param name="columnName">The column name from the database</param>
        /// <param name="mappedProperty">The mapped property from the model definition</param>
        /// <param name="mappedPropertyCustomName">The mapped property from the attributes</param>
        /// <returns>True if there is a clash between the attribute and a property</returns>
        private static bool IsAttributePropertyNamingClash(Dictionary<string, PropertyInfo> customNameMappings,
            string columnName, PropertyInfo mappedProperty, PropertyInfo mappedPropertyCustomName) 
            => mappedProperty != null && mappedPropertyCustomName != null && !customNameMappings.Values.Any(tp => tp.Name.Equals(columnName));

        /// <summary>
        /// Gets the custom name attribute from the property
        /// </summary>
        /// <param name="property">The property in the model</param>
        /// <returns>The custom name if it's specified, null otherwise</returns>
        private static string GetColumnAttribute(PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(true);
            var customName = attributes
                .Select(attr => attr as DbColumnAttribute)
                .Where(attr => attr != null)
                .Select(attr => attr.Name)
                .FirstOrDefault();

            return customName;
        }
    }
}
