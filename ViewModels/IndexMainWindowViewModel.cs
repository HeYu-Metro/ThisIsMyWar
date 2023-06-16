using MySql.Data.MySqlClient;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThisIsMyWar.COM;
using ThisIsMyWar.Models;

namespace ThisIsMyWar.ViewModels
{
    /// <summary>
    /// 数据库操作参考
    /// </summary>
    internal class IndexMainWindowViewModel : BindableBase
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private string code;
        public string Code
        {
            get { return code; }
            set { SetProperty(ref code, value); }
        }
        private ModelData dbConnection;
        public IndexMainWindowViewModel()
        {
            dbConnection = new ModelData();

            //加解密
            string plainText = "Hello, World!";

            byte[] encrypted = Encryption.Encrypt(plainText);
            Title = Convert.ToBase64String(encrypted);

            string decrypted = Encryption.Decrypt(encrypted);
            Name = decrypted;
            //验证码
            var var1 = new Message();
            Code = var1.Code();
            //接收人
            string email = "1799508868@qq.com";
            string subject = "验证码";
            string body = $"验证码是{Code}";
            //发送人
            string smtpServer = "smtp.qq.com";
            int smtpPort = 587;
            string senderEmail = "3069448871@qq.com";
            string senderPassword = "frjfhvhzfarsdgej";
            //发送邮件
            var var = new EmailSender();
            var.SendEmail(email, subject, body);
        }
        //查询
        public void Get()
        {
            using var connection = dbConnection.GetConnection();
            connection.Open();
            string ID = "2";
            string query = $"SELECT * FROM User WHERE ID={ID}";
            string name = "";
            MySqlCommand command = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    name = reader.GetString("Name");
                }
            }
            Title = name;
            connection.Close();
        }
        //添加
        public void Add()
        {
            using var connection = dbConnection.GetConnection();
            connection.Open();
            string ID = "3";
            string Name = "WZJ";
            string query = $"INSERT INTO `thisismywar`.`user` (`ID`, `Name`) VALUES ({ID}, '{Name}')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        //删除
        public void Delete()
        {
            using var connection = dbConnection.GetConnection();
            connection.Open();
            string ID = "3";
            string query = $"DELETE FROM `thisismywar`.`user` WHERE `ID` = {ID}";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        //修改
        public void Update()
        {
            using var connection = dbConnection.GetConnection();
            connection.Open();
            string ID = "3";
            string Name = "hexiancheng";
            string query = $"UPDATE `thisismywar`.`user` SET `Name` = '{Name}' WHERE `ID` = {ID}";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
