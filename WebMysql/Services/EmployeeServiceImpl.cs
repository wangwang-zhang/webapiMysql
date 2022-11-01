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
}