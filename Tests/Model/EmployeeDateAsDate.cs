using System;

namespace Tests.Model
{
    /// <summary>
    /// This model has only the date of birth set
    /// </summary>
    public class EmployeeDateAsDate
    {
        public DateTime? DoB { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmployeeDateAsDate fields &&
                   DoB == fields.DoB;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DoB);
        }
    }
}
