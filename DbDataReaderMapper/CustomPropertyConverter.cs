using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DbDataReaderMapper
{
    public class CustomPropertyConverter
    {
        private Dictionary<PropertyInfo, Delegate> _conversionFunctions;

        public CustomPropertyConverter()
        {
            _conversionFunctions = new Dictionary<PropertyInfo,Delegate>();
        }

        /// <summary>
        /// Add a custom conversion for serializing values to the DAO
        /// This can be used, for instance, to serialize a string into an Enum value
        /// </summary>
        /// <remarks>
        /// DbNull values are converted to null
        /// </remarks>
        /// <typeparam name="T">The DAO type</typeparam>
        /// <typeparam name="U">The database type</typeparam>
        /// <typeparam name="V">The mapped output type</typeparam>
        /// <param name="property">The output property that needs conversion</param>
        /// <param name="conversionFunction">The conversion function that converts the database output type to the DAO property type</param>
        public CustomPropertyConverter AddConversion<T, U, V>(Expression<Func<T, V>> property, Func<U,V> conversionFunction)
        {
            var memberInfo = ((MemberExpression)property.Body).Member;
            if (memberInfo.MemberType == MemberTypes.Property)
            {
                _conversionFunctions.Add((PropertyInfo)memberInfo, conversionFunction);
            }
            else
            {
                throw new ArgumentException("The property selector should reference a property in the DAO");
            }

            return this;
        }

        internal Delegate this[PropertyInfo key]
        {
            get
            {
                var resolvedKey = _conversionFunctions.Keys.FirstOrDefault(k => k.Name.Equals(key.Name));
                return resolvedKey == null
                    ? null
                    : _conversionFunctions[resolvedKey];
            }
        }
    }
}
