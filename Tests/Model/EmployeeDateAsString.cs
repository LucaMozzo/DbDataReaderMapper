using System;

namespace Tests.Model
{
    /// <summary>
    /// This model has the date of birth set as a string instead of a date
    /// </summary>
    public class EmployeeDateAsString
    {
        public string DoB { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmployeeDateAsString fields &&
                   DoB == fields.DoB;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DoB);
        }
    }
}
