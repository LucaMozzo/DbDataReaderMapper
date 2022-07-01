using System;

namespace Tests.Model
{
    /// <summary>
    /// This model has the same fields as the columns names in the database except address
    /// </summary>
    public class EmployeeSuperClass
    {
        public int Id { get; set; }
        public int? Age { get; set; }
        public string FullName { get; set; }
        public DateTime? DoB { get; set; }
    }
}
