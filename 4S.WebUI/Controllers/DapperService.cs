using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace _4S.WebUI.Controllers
{
    public class DapperService
    {
        public static MySqlConnection MySqlConnection()
        {
            string mysqlConnectionStr = ConfigurationManager.ConnectionStrings["MySqlConnString"].ToString();
            var connection = new MySqlConnection(mysqlConnectionStr);
            connection.Open();
            return connection;
        }

    }
}