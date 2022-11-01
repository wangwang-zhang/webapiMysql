using Microsoft.AspNetCore.Mvc;
using WebMysql.Dao;

namespace WebMysql.Services;

public class DepartmentServiceImpl : IDepartmentService
{
    private readonly DepartmentDaoImpl _departmentDaoImpl;

    public DepartmentServiceImpl(DepartmentDaoImpl departmentDaoImpl)
    {
        _departmentDaoImpl = departmentDaoImpl;
    }

    public JsonResult GetDepartments()
    {
        return _departmentDaoImpl.Get();
    }
}