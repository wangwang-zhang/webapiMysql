using Microsoft.AspNetCore.Mvc;
using WebMysql.Models;

namespace WebMysql.Dao;

public interface IDepartmentDao
{
    public JsonResult Get();
    public bool Post(Department department);
    public bool Put(Department department);
    public bool Delete(int id);
}