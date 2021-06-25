using System;
using DbDataReaderMapper;

namespace Tests.Model
{
    /// <summary>
    /// This model has the Id and other fields with names different from the columns in the database but uses attributes to do the mapping
    /// The mapping of name maps to the same name as another property which in turns maps to a different column name, avoiding the clash
    /// </summary>
    public class EmployeeWithAttributesPropertyNameSwap
    {
        [DbColumn("Address")]
        public string FullName { get; set; }
        [DbColumn("FullName")]
        public string Address { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmployeeWithAttributesPropertyNameSwap fields &&
                   Address == fields.Address &&
                   FullName == fields.FullName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Address, FullName);
        }
    }
}
