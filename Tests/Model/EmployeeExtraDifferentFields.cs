using System;

namespace Tests.Model
{
    /// <summary>
    /// This model has the Id and other fields with names different from the columns in the database
    /// </summary>
    public class EmployeeExtraDifferentFields
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmployeeExtraDifferentFields fields &&
                   Id == fields.Id &&
                   Name == fields.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
