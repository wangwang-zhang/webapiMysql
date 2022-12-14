using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebMysql.Models;

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

    public bool Post(Employee employee)
    {
        string query = @"
          insert into Employee(EmployeeName, Department, DateOfJoining, PhotoFileName) 
          values 
        (@EmployeeName, @Department, @DateOfJoining, @PhotoFileName);
        ";
        DataTable table = new DataTable();

        _connection.Open();
        using (MySqlCommand myCommand = new MySqlCommand(query, _connection))
        {
            myCommand.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
            myCommand.Parameters.AddWithValue("@Department", employee.Department);
            myCommand.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
            myCommand.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);
            var myReader = myCommand.ExecuteReader();
            table.Load(myReader);
            myReader.Close();
            _connection.Close();
        }

        return true;
    }

    public bool Put(Employee employee)
    {
        string query = @"
           update Employee set 
           EmployeeName = @EmployeeName,
           Department = @Department,
           DateOfjoining = @DateOfjoining,
           PhotoFileName = @PhotoFileName
           where EmployeeId = @EmployeeId;
        ";
        DataTable table = new DataTable();

        _connection.Open();
        using (MySqlCommand myCommand = new MySqlCommand(query, _connection))
        {
            myCommand.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
            myCommand.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
            myCommand.Parameters.AddWithValue("@Department", employee.Department);
            myCommand.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
            myCommand.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);

            var myReader = myCommand.ExecuteReader();
            table.Load(myReader);
            myReader.Close();
            _connection.Close();
        }
        return true;
    }

    public bool Delete(int id)
    {
        var query = @"
            delete from Employee
           where EmployeeId = @EmployeeId
        ";
        DataTable table = new();
        _connection.Open();
        using (MySqlCommand mySqlCommand = new MySqlCommand(query, _connection))
        {
            mySqlCommand.Parameters.AddWithValue("@EmployeeId", id);
            var myReader = mySqlCommand.ExecuteReader();
            table.Load(myReader);
            myReader.Close();
            _connection.Close();
        }
        return true;
    }
}