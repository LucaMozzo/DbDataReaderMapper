using System;

namespace Tests.Model
{
    /// <summary>
    /// Adds address field on top of superclass
    /// </summary>
    public class EmployeeSubClass : EmployeeSuperClass
    {
        public string Address { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmployeeSubClass fields &&
                   Id == fields.Id &&
                   Age == fields.Age &&
                   Address == fields.Address &&
                   FullName == fields.FullName &&
                   DoB == fields.DoB;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Age, Address, FullName, DoB);
        }
    }
}
