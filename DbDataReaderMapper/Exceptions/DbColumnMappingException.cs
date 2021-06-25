using System;

namespace DbDataReaderMapper.Exceptions
{
    public class DbColumnMappingException : Exception
    {
        public DbColumnMappingException(string message) : base(message) { }
    }
}
