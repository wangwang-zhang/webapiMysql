using Microsoft.AspNetCore.Mvc;
using WebMysql.Models;

namespace WebMysql.Services;

public interface IDepartmentService
{
    public JsonResult GetDepartments();
    public JsonResult InsertDepartmentName(Department department);
    public JsonResult UpdateDepartmentNameById(Department department);
    public JsonResult DeleteDepartmentById(int id);

}