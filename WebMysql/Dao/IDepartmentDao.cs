using Microsoft.AspNetCore.Mvc;

namespace WebMysql.Dao;

public interface IDepartmentDao
{
    public JsonResult Get();
}