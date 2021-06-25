using System.Linq;
using System.Data.Common;
using System.Reflection;
using System;

namespace DbDataReaderMapper
{
    public static class DbDataReaderExtension
    {
        /// <summary>
        /// Maps the current row to the specified type
        /// </summary>
        /// <typeparam name="T">The type of the output object</typeparam>
        /// <param name="dataReader"></param>
        /// <param name="forceDefaultOnNull">If true, a non-nullable property that finds a null value in the database, uses the
        /// default value instead of throwing an exception</param>
        /// <returns>The object that contains the data in the current row of the reader</returns>
        public static T MapObject<T>(this DbDataReader dataReader, bool forceDefaultOnNull = false) where T : class, new()
        {
            T obj = new T();
            PropertyInfo[] typeProperties = typeof(T).GetProperties();
            for (int i = 0; i < dataReader.FieldCount; ++i)
            {
                string columnName = dataReader.GetName(i);

                var mappedProperty = typeProperties.Where(tp => tp.Name.Equals(columnName)).FirstOrDefault();
                if (mappedProperty != null)
                {
                    var value = dataReader[columnName];
                    if (value is DBNull)
                    {
                        value = null;
                    }

                    try
                    {
                        if (value == null && forceDefaultOnNull)
                        {
                            mappedProperty.SetValue(obj, default);
                        }
                        else
                        {
                            mappedProperty.SetValue(obj, value);
                        }
                    }
                    catch
                    {
                        throw new InvalidCastException($"Expected type {mappedProperty.PropertyType} but found {value.GetType()} for property {columnName}");
                    }
                }
            }
            return obj;
        }
    }
}
