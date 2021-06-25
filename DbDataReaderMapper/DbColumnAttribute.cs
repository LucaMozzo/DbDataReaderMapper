using System;

namespace DbDataReaderMapper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnAttribute : Attribute
    {
        public string Name { get; private set; }

        /// <summary>
        /// Maps to a column in the result set with the given name
        /// </summary>
        /// <param name="name">The name of the column to map to</param>
        public DbColumnAttribute(string name)
        {
            Name = name;
        }
    }
}
