using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebMysql.Dao;
using WebMysql.Models;
using WebMysql.Services;

namespace WebMysql.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public JsonResult Get()
    {
        return _departmentService.GetDepartments();
    }

    [HttpPost]
    public JsonResult Post(Department department)
    {
        return _departmentService.InsertDepartmentName(department);
    }

    [HttpPut]
    public JsonResult Put(Department department)
    {
        return _departmentService.UpdateDepartmentNameById(department);
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(int id)
    {
        return _departmentService.DeleteDepartmentById(id);
    }
}