using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace WebMysql.Dao;

public class EmployeeDaoImpl : IEmployeeDao
{
    private readonly MySqlConnection _connection;

    public EmployeeDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public JsonResult Get()
    {
        string query = @"
          select EmployeeId, EmployeeName,Department,
                 DATE_FORMAT(DateOfJoining, '%Y-%m-%d') as DateOfJoining,
                 PhotoFileName
          from Employee;
        ";
        DataTable table = new DataTable();
        _connection.Open();
        using (MySqlCommand myCommand = new MySqlCommand(query, _connection))
        {
            var myReader = myCommand.ExecuteReader();
            table.Load(myReader);
            myReader.Close();
            _connection.Close();
        }
        return new JsonResult(table);
    }
}