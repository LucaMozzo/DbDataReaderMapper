using System;

namespace Tests.Model
{
    /// <summary>
    /// This model has the same fields as the columns names in the database
    /// </summary>
    public class EmployeeAllFields
    {
        public int Id { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public DateTime? DoB { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmployeeAllFields fields &&
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
