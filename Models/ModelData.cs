using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThisIsMyWar.Models
{
    /// <summary>
    /// 数据库连接信息
    /// </summary>
    internal class ModelData : DbContext
    {
        private readonly string connectionString;
        public ModelData()
        {
            var builder = new MySqlConnectionStringBuilder();
            builder.Server = "rm-cn-pe3390ehc0013txo.rwlb.rds.aliyuncs.com";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "ZAQ12wsx";
            builder.Database = "ThisIsMyWar";
            connectionString = builder.ToString();
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
