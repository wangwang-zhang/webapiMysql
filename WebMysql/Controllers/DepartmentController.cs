using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebMysql.Models;

namespace WebMysql.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : Controller
{
    private readonly IConfiguration _configuration;

    public DepartmentController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public JsonResult Get()
    {
        string query = @"
          select DepartmentId, DepartmentName
          from Department
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
    public JsonResult Post(Department department)
    {
        string query = @"
          insert into Department(DepartmentName) values 
                        (@DepartmentName);
        ";
        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                var myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
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
        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
                myCommand.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                var myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
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
        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@DepartmentId", id);
                var myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
        }
        return new JsonResult("Deleted successfully");
    }
}