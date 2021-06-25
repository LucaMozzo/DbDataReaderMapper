using System;

namespace Tests.Model
{
    /// <summary>
    /// This model only has an id field, without all the other columns in the database
    /// </summary>
    public class EmployeeMissingFields
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmployeeMissingFields fields &&
                   Id == fields.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
