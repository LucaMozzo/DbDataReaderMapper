using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Model
{
    internal class EmployeeWrongTypeNeedsConversionSubclass : EmployeeWrongTypeNeedsConversionBaseClass
    {
        public DateTime? DoB { get; set; }
        
        public override bool Equals(object obj)
        {
            return obj is EmployeeWrongTypeNeedsConversionSubclass fields &&
                   Id == fields.Id &&
                   Age == fields.Age &&
                   Address == fields.Address &&
                   FullName == fields.FullName &&
                   DoB == fields.DoB;
        }
    }
}
