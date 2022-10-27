using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebMysql.Models;

namespace WebMysql.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : Controller
{
    private readonly MySqlConnection _connection;

    public DepartmentController(IConfiguration configuration, MySqlConnection connection)
    {
        _connection = connection;
    }

    [HttpGet]
    public JsonResult Get()
    {
        string query = @"
          select DepartmentId, DepartmentName
          from Department
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

    [HttpPost]
    public JsonResult Post(Department department)
    {
        string query = @"
          insert into Department(DepartmentName) values 
                        (@DepartmentName);
        ";
        DataTable table = new DataTable();
        _connection.Open();
        using (MySqlCommand myCommand = new MySqlCommand(query, _connection))
        {
            myCommand.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            var myReader = myCommand.ExecuteReader();
            table.Load(myReader);
            myReader.Close();
            _connection.Close();
        }

        return new JsonResult("Added successfully");
    }

    [HttpPut]
    public JsonResult Put(Department department)
    {
        string query = @"
           update Department set 
           DepartmentName = @DepartmentName
           where DepartmentId = @DepartmentId;
        ";
        DataTable table = new DataTable();


        _connection.Open();
        using (MySqlCommand myCommand = new MySqlCommand(query, _connection))
        {
            myCommand.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
            myCommand.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            var myReader = myCommand.ExecuteReader();
            table.Load(myReader);
            myReader.Close();
            _connection.Close();
        }

        return new JsonResult("Updated successfully");
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(int id)
    {
        string query = @"
           delete from Department 
           where DepartmentId = @DepartmentId;
        ";
        DataTable table = new DataTable();
        _connection.Open();
        using (MySqlCommand myCommand = new MySqlCommand(query, _connection))
        {
            myCommand.Parameters.AddWithValue("@DepartmentId", id);
            var myReader = myCommand.ExecuteReader();
            table.Load(myReader);
            myReader.Close();
            _connection.Close();
        }
        return new JsonResult("Deleted successfully");
    }
}