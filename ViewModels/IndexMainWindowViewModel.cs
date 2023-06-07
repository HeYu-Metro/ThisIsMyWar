﻿using MySql.Data.MySqlClient;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        private ModelData dbConnection;
        public IndexMainWindowViewModel()
        {
            dbConnection = new ModelData();
            Add();
            Get();
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
