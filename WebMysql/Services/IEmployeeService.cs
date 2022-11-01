using Microsoft.AspNetCore.Mvc;

namespace WebMysql.Services;

public interface IEmployeeService
{
    public JsonResult GetEmployees();
}