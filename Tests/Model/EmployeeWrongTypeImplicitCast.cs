using System;

namespace Tests.Model
{
    /// <summary>
    /// This model has the same fields as the columns names in the database but a different compatible type for age and dob
    /// </summary>
    public class EmployeeWrongTypeImplicitCast
    {
        public string Id { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public string DoB { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmployeeWrongTypeImplicitCast fields &&
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
