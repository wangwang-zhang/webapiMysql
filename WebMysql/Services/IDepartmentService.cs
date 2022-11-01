using Microsoft.AspNetCore.Mvc;

namespace WebMysql.Services;

public interface IDepartmentService
{
    public JsonResult GetDepartments();
}