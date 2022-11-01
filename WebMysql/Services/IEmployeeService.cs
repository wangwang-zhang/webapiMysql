using Microsoft.AspNetCore.Mvc;
using WebMysql.Models;

namespace WebMysql.Services;

public interface IEmployeeService
{
    public JsonResult GetEmployees();
    public JsonResult InsertEmployee(Employee employee);
}