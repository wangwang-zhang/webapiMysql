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
    private readonly IWebHostEnvironment _webHostEnvironment;

    public EmployeeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
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

    [HttpPut]
    public JsonResult Put(Employee employee)
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
        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
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
        return new JsonResult("Updated successfully");
    }
    [HttpDelete("{id}")]
    public JsonResult Delete(int id)
    {
        string query = @"
           delete from Employee 
           where EmployeeId = @EmployeeId;
        ";
        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@EmployeeId", id);
                var myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
        }
        return new JsonResult("Deleted successfully");
    }
    [Route("SaveFile")]
    [HttpPost]
    public JsonResult SaveFile()
    {
        try
        {
            var httpRequest = Request.Form;
            int fileCount = httpRequest.Files.Count;
            var fileNames = new string[fileCount];
            for (int i = 0; i < fileCount; i++)
            {
                var postedFile = httpRequest.Files[i];
                fileNames[i] = postedFile.FileName;
                var physicalPath = _webHostEnvironment.ContentRootPath + "/Photos/" + fileNames[i];
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
            }
            return new JsonResult(fileNames);
        }
        catch (Exception)
        {
            return new JsonResult("anonymous.png");
        }
    }
}