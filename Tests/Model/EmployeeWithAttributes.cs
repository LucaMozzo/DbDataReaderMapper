using System;
using DbDataReaderMapper;

namespace Tests.Model
{
    /// <summary>
    /// This model has the Id and other fields with names different from the columns in the database but uses attributes to do the mapping
    /// </summary>
    public class EmployeeWithAttributes
    {
        [DbColumn("ID")]
        public int Id { get; set; }
        [DbColumn("FullName")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmployeeWithAttributes fields &&
                   Id == fields.Id &&
                   Name == fields.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
