using MySql.Data.MySqlClient;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ThisIsMyWar.ViewModels
{


    internal class IndexMainWindowViewModel : BindableBase
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        string connectionString = "server=rm-cn-pe3390ehc0013txo.rwlb.rds.aliyuncs.com;user id=root;password=ZAQ12wsx;persistsecurityinfo=True;database=ThisIsMyWar;charset=utf8;";
        public IndexMainWindowViewModel()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM User WHERE ID=2;";
                    string name="";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name = reader.GetString("Name");
                        }
                    }
                    Title = name;
                }
                catch (Exception ex)
                {
                    // 处理连接错误
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
