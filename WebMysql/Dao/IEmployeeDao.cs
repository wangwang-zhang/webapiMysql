using Microsoft.AspNetCore.Mvc;

namespace WebMysql.Dao;

public interface IEmployeeDao
{
    public JsonResult Get();
}