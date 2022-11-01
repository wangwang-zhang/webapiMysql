using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace WebMysql.Dao;

public class DepartmentDaoImpl : IDepartmentDao
{
    private readonly MySqlConnection _connection;
    public DepartmentDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    readonly DataTable _table = new();
    
    public JsonResult Get()
    {
        string query = @"
          select DepartmentId, DepartmentName
          from Department
        ";
        _connection.Open();
        using (MySqlCommand myCommand = new MySqlCommand(query, _connection))
        {
            var myReader = myCommand.ExecuteReader();
            _table.Load(myReader);
            myReader.Close();
            _connection.Close();
        }
        return new JsonResult(_table);
    }
}