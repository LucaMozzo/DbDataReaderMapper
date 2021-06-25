[![NuGet](https://img.shields.io/nuget/v/DbDataReaderMapper.svg)](https://www.nuget.org/packages/DbDataReaderMapper/)
# DbDataReaderMapper
This is a .NET library that contains an extension of DbDataReader that automatically maps a row to a model.

Works with all major DB connections, tested on MySQL, SQL Server, Azure SQL and OLEDB (Access).

## Usage

Imagine a database with the following code
```
CREATE TABLE Employee (
  Id          INT          NOT NULL PRIMARY KEY,
  FirstName   VARCHAR(128) NOT NULL,
  LastName    VARCHAR(128) NOT NULL,
  Age         INT          NULL
)
```

We create a model for the object we want to map
```
class EmployeeDao
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? Age { get; set; }
}
```

In our database interface, we import the namespace
```
using DbDataReaderMapper;
```

And when we query the database we can use the extension method `MapToObject` as follows (this example uses the OLEDB connector for simplicity, but it works with others too)
```
OleDbCommand cmd = connection.CreateCommand();
cmd.CommandText = "SELECT * FROM Employee;";
cmd.Connection = connection;
var reader = await cmd.ExecuteReaderAsync();
while (await reader.ReadAsync())
{
    var employeeObj = reader.MapToObject<EmployeeDao>();
}
```

### Additional features

#### Custom property to column mapping

You can map a property of your model to a column with a different name by using the `DbColumn` attribute.

For example, if your database instance uses snake case for column naming, you can do the following:
```
class EmployeeDao
{
    [DbColumn("id")]
    public int Id { get; set; }
    [DbColumn("first_name")]
    public string FirstName { get; set; }
    [DbColumn("last_name")]
    public string LastName { get; set; }
    [DbColumn("age")]
    public int? Age { get; set; }
}
```

#### Allow implicit casting

By default, implicit casting (e.g. mapping an `int` from the database to a `string` in the model) causes an `InvalidCastException`. You can enabled the explicit casting where needed by setting the flag to true when calling the method:
```
var employeeObj = reader.MapToObject<EmployeeDao>(true);
```

## Why do I need this?

If you're managing the connection to your relational database yourself, you're probably doing something like this:
```
OleDbCommand cmd = connection.CreateCommand();
cmd.CommandText = "SELECT Id, FirstName, LastName, Age FROM Table";
cmd.Connection = connection;

var reader = await cmd.ExecuteReaderAsync();
while (await reader.ReadAsync())
{
    var employeeObj = new Employee
    {
        Id = reader.GetInt32(0),
        FirstName = reader.GetString(1),
        LastName = reader.GetString(2),
        Age = reader.GetInt32(3) is DbNull ? null : reader.GetInt32(3)
    });
}
```
or using the column names instead of the indices.

This presents 4 problems:
- If you're using indexes, using `SELECT *` or putting the fields in the wrong order can cause a misplacement of data in your object, or an error
- It's a lot of repetitive unreadable code to write
- Handling the DbNull result on all nullable fields will require even more effort
- If you add a new column to the database, you need to update the mapping as well

With this library everything is taken care of.
