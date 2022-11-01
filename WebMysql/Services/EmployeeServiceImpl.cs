using Microsoft.AspNetCore.Mvc;
using WebMysql.Dao;
using WebMysql.Models;

namespace WebMysql.Services;

public class EmployeeServiceImpl : IEmployeeService
{
    private readonly IEmployeeDao _employeeDao;

    public EmployeeServiceImpl(IEmployeeDao employeeDao)
    {
        _employeeDao = employeeDao;
    }

    public JsonResult GetEmployees()
    {
        return _employeeDao.Get();
    }

    public JsonResult InsertEmployee(Employee employee)
    {
        if (_employeeDao.Post(employee))
            return new JsonResult("Added successful!");
        return new JsonResult("Added failed!");
    }

    public JsonResult UpdateEmployeeById(Employee employee)
    {
        if (_employeeDao.Put(employee))
            return new JsonResult("Updated successfully!");
        return new JsonResult("updated failed!");
    }

    public JsonResult DeleteEmployeeById(int id)
    {
        if (_employeeDao.Delete(id))
            return new JsonResult("Deleted successfully!");
        return new JsonResult("Deleted failed");
    }
}