using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.OleDb;
using System.Threading.Tasks;
using DbDataReaderMapper;
using Tests.Model;

namespace Tests
{
    [TestClass]
    public class DbDataReaderExtensionTest
    {
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=TestDatabase.accdb");

        [TestMethod]
        public async Task TestMapperHappyPath()
        {
            OleDbCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandText = "SELECT ID AS Id, FullName, Age, Address, DoB FROM Employee WHERE ID=1;";
            cmd.Connection = connection;
            var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var employeeObj = reader.MapObject<EmployeeAllFields>();
                Assert.AreEqual(employeeObj, new EmployeeAllFields
                {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Age = reader.GetInt32(2),
                    Address = reader.GetString(3),
                    DoB = reader.GetDateTime(4)
                });
            }
            connection.Close();
        }

        [TestMethod]
        public async Task TestMapperNullNullableValues()
        {
            OleDbCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandText = "SELECT ID AS Id, FullName, Age, Address, DoB FROM Employee WHERE ID=2;";
            cmd.Connection = connection;
            var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var employeeObj = reader.MapObject<EmployeeAllFields>();
                Assert.AreEqual(employeeObj, new EmployeeAllFields
                {
                    Id = reader.GetInt32(0),
                    FullName = null,
                    Age = null,
                    Address = null,
                    DoB = null
                });
            }
            connection.Close();
        }

        [TestMethod]
        public async Task TestMapperWithMissingModelFields()
        {
            OleDbCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandText = "SELECT ID AS Id, FullName, Age, Address, DoB FROM Employee WHERE ID=1;";
            cmd.Connection = connection;
            var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var employeeObj = reader.MapObject<EmployeeMissingFields>();
                Assert.AreEqual(employeeObj, new EmployeeMissingFields
                {
                    Id = reader.GetInt32(0)
                });
            }
            connection.Close();
        }

        [TestMethod]
        public async Task TestMapperWithExtraModelFields()
        {
            OleDbCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandText = "SELECT ID AS Id, FullName, Age, Address, DoB FROM Employee WHERE ID=1;";
            cmd.Connection = connection;
            var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var employeeObj = reader.MapObject<EmployeeExtraDifferentFields>();
                Assert.AreEqual(employeeObj, new EmployeeExtraDifferentFields
                {
                    Id = reader.GetInt32(0),
                    Name = null
                });
            }
            connection.Close();
        }
    }
}
