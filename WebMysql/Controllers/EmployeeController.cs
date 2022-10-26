using Microsoft.AspNetCore.Mvc;
using System.Data;
using MySql.Data.MySqlClient;
using WebMysql.Models;


namespace WebMysql.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : Controller
{
    private readonly IConfiguration _configuration;

    public EmployeeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public JsonResult Get()
    {
        string query = @"
          select EmployeeId, EmployeeName,Department,
                 DATE_FORMAT(DateOfJoining, '%Y-%m-%d') as DateOfJoining,
                 PhotoFileName
          from Employee;
        ";
        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
            {
                var myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
        }
        return new JsonResult(table);
    }
    [HttpPost]
    public JsonResult Post(Employee employee)
    {
        string query = @"
          insert into Employee(EmployeeName, Department, DateOfJoining, PhotoFileName) 
          values 
        (@EmployeeName, @Department, @DateOfJoining, @PhotoFileName);
        ";
        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                myCommand.Parameters.AddWithValue("@Department", employee.Department);
                myCommand.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                myCommand.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);
                var myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
        }
        return new JsonResult("Added successfully");
    }

    
}