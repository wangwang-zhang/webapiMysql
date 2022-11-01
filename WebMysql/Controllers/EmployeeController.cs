using Microsoft.AspNetCore.Mvc;
using System.Data;
using MySql.Data.MySqlClient;
using WebMysql.Models;
using WebMysql.Services;


namespace WebMysql.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : Controller
{
    private readonly MySqlConnection _connection;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IWebHostEnvironment webHostEnvironment, MySqlConnection connection, IEmployeeService employeeService)
    {
        _webHostEnvironment = webHostEnvironment;
        _connection = connection;
        _employeeService = employeeService;
    }

    [HttpGet]
    public JsonResult Get()
    {
        return _employeeService.GetEmployees();
    }
    
    [HttpPost]
    public JsonResult Post(Employee employee)
    {
        return _employeeService.InsertEmployee(employee);
    }

    [HttpPut]
    public JsonResult Put(Employee employee)
    {
        return _employeeService.UpdateEmployeeById(employee);
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(int id)
    {
        string query = @"
           delete from Employee 
           where EmployeeId = @EmployeeId;
        ";
        DataTable table = new DataTable();

        _connection.Open();
        using (MySqlCommand myCommand = new MySqlCommand(query, _connection))
        {
            myCommand.Parameters.AddWithValue("@EmployeeId", id);
            var myReader = myCommand.ExecuteReader();
            table.Load(myReader);
            myReader.Close();
            _connection.Close();
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