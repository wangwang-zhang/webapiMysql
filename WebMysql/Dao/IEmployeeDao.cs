using Microsoft.AspNetCore.Mvc;
using WebMysql.Models;

namespace WebMysql.Dao;

public interface IEmployeeDao
{
    public JsonResult Get();
    public bool Post(Employee employee);
}