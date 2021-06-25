using System;
using DbDataReaderMapper;

namespace Tests.Model
{
    /// <summary>
    /// This model has the Id and other fields with names different from the columns in the database but uses attributes to do the mapping
    /// The mapping of name maps to the same name as another property causing a clash
    /// </summary>
    public class EmployeeWithAttributesSameNameAsProperty
    {
        [DbColumn("ID")]
        public int Id { get; set; }
        [DbColumn("FullName")]
        public string Name { get; set; }
        public string FullName { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmployeeWithAttributesSameNameAsProperty fields &&
                   Id == fields.Id &&
                   Name == fields.Name &&
                   FullName == fields.FullName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, FullName);
        }
    }
}
