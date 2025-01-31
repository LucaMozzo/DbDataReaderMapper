using System;

namespace Tests.Model
{
    internal class EmployeeWrongTypeNeedsConversionBaseClass
    {
        public int Id { get; set; }
        public int? Age { get; set; }
        public string FullName { get; set; }
        public int Address { get; set; } // refers to the address length. Used to test the custom converter on the superclass field
    }
}
