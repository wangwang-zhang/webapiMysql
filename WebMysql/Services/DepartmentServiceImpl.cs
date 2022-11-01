using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using WebMysql.Dao;
using WebMysql.Models;

namespace WebMysql.Services;

public class DepartmentServiceImpl : IDepartmentService
{
    private readonly IDepartmentDao _departmentDao;

    public DepartmentServiceImpl(DepartmentDaoImpl departmentDaoImpl)
    {
        _departmentDao = departmentDaoImpl;
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
}