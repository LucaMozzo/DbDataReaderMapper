using System;

namespace Tests.Model
{
    public struct EmployeeAllFieldsStruct
    {
        public int Id { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public DateTime? DoB { get; set; }
    }
}
