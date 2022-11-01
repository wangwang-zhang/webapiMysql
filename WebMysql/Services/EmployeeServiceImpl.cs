using Microsoft.AspNetCore.Mvc;
using WebMysql.Dao;

namespace WebMysql.Services;

public class EmployeeServiceImpl : IEmployeeService
{
    private IEmployeeDao _employeeDao;

    public EmployeeServiceImpl(IEmployeeDao employeeDao)
    {
        _employeeDao = employeeDao;
    }

    public JsonResult GetEmployees()
    {
        return _employeeDao.Get();
    }
}