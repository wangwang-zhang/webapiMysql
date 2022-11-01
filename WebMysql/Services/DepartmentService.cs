using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using WebMysql.Dao;
using WebMysql.Models;

namespace WebMysql.Services;

public class DepartmentService
{
    private readonly IDepartmentDao _departmentDao;

    public DepartmentService(IDepartmentDao departmentDao)
    {
        _departmentDao = departmentDao;
    }

    public JsonResult GetDepartments()
    {
        return _departmentDao.Get();
    }

    public JsonResult InsertDepartmentName(Department department)
    {
        if(_departmentDao.Post(department)) 
            return new JsonResult("Added successfully!");
        return new JsonResult("Added failed");
    }

    public JsonResult UpdateDepartmentNameById(Department department)
    {
        if (_departmentDao.Put(department))
            return new JsonResult("Updated successfully!");
        return new JsonResult("Updated failed");
    }

    public JsonResult DeleteDepartmentById(int id)
    {
        if (_departmentDao.Delete(id))
            return new JsonResult("Deleted successfully!");
        return new JsonResult("Deleted failed");
    }
}