using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ThisIsMyWar.Models
{
    /// <summary>
    /// 登录注册页面数据库操作
    /// </summary>
    public class LoginModel
    {
        private ModelData dbConnection;
        public LoginModel()
        {
            dbConnection = new ModelData();
        }
        /// <summary>
        /// 用户名是否已存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool GetUserName(string UserName)
        {
            using var connection = dbConnection.GetConnection();
            connection.Open();
            string query = $"SELECT * FROM UserInfo WHERE UserName='{UserName}'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            connection.Close();
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <param name="Email"></param>
        /// <param name="UserRights"></param>
        /// <param name="CreateTime"></param>
        public void AddUser(string UserName, string PassWord, string Email, int UserRights, string CreateTime)
        {
            using var connection = dbConnection.GetConnection();
            connection.Open();
            string query = $"INSERT INTO `thisismywar`.`userinfo` (`UserName`, `PassWord`, `Email`, `UserRights`, `CreateTime`) VALUES ('{UserName}', '{PassWord}', '{Email}', {UserRights}, '{CreateTime}')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        /// <summary>
        /// 密码查询
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public string PassWord(string UserName)
        {
            using var connection = dbConnection.GetConnection();
            connection.Open();
            string query = $"SELECT PassWord FROM UserInfo WHERE UserName='{UserName}'";
            string password = "";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    password = reader.GetString("PassWord");
                }
            }
            connection.Close();
            return password;
        }
    }
}
