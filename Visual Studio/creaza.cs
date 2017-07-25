using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CM
{
    public partial class creaza : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public creaza()
        {
            InitializeComponent();
            server = "sql11.freemysqlhosting.net";
            database = "sql11185523";
            uid = "sql11185523";
            password = "CXGg96jxzv"; // parola
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
             connection.Open();
             MySqlCommand cmd = connection.CreateCommand();
             cmd.CommandText = "CREATE TABLE Intrebari ( id INT NOT NULL AUTO_INCREMENT PRIMARY KEY, intrebare VARCHAR(256), g1 VARCHAR(256), g2 VARCHAR(256), g3 VARCHAR(256), corect VARCHAR(256) )";
             cmd.ExecuteNonQuery();
             connection.Close();
             connection.Open();
             cmd = connection.CreateCommand();
             cmd.CommandText = "CREATE TABLE Butoane ( NumeUser VARCHAR(256), NumeButon VARCHAR(256), Culoare VARCHAR(256), Litera VARCHAR(256) )";
             cmd.ExecuteNonQuery();
             connection.Close();
             connection.Open();
             cmd = connection.CreateCommand();
             cmd.CommandText = "CREATE TABLE Clan ( Nume VARCHAR(256), Leader VARCHAR(256), Level VARCHAR(256), Playeri VARCHAR(256), Max VARCHAR(256), Descriere VARCHAR(256), Tag VARCHAR(256) )";
             cmd.ExecuteNonQuery();
             connection.Close();
             connection.Open();
             cmd = connection.CreateCommand();
             cmd.CommandText = "CREATE TABLE GlobalChat ( User VARCHAR(256), Mesaj VARCHAR(256) )";
             cmd.ExecuteNonQuery();
             connection.Close();
             connection.Open();
             cmd = connection.CreateCommand();
             cmd.CommandText = "CREATE TABLE Conectati ( User VARCHAR(256) )";
             cmd.ExecuteNonQuery();
             connection.Close();
             connection.Open();
             cmd = connection.CreateCommand();
             cmd.CommandText = "CREATE TABLE Sesiuni ( NumeSesiune VARCHAR(256) )";
             cmd.ExecuteNonQuery();
             connection.Close();
            /*  try
             {
                 connection.Open();
                 MySqlCommand cmd = connection.CreateCommand();
                 cmd.CommandText = "TRUNCATE Sesiuni";
                 cmd.ExecuteNonQuery();
                 connection.Close();
                 connection.Open();
                 cmd = connection.CreateCommand();
                 cmd.CommandText = "DROP TABLE cristimc8joc";
                 cmd.ExecuteNonQuery();
                 connection.Close();
                 connection.Open();
                 cmd = connection.CreateCommand();
                 cmd.CommandText = "DROP TABLE cristimc8intrebari";
                 cmd.ExecuteNonQuery();
                 connection.Close();
                 connection.Open();
                 cmd = connection.CreateCommand();
                 cmd.CommandText = "DROP TABLE ajoc";
                 cmd.ExecuteNonQuery();
                 connection.Close();
                 connection.Open();
                 cmd = connection.CreateCommand();
                 cmd.CommandText = "DROP TABLE aintrebari";
                 cmd.ExecuteNonQuery();
                 connection.Close();
             }
             catch(Exception x)
             {

             }
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "TRUNCATE Sesiuni";
            cmd.ExecuteNonQuery();
            connection.Close();
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "DROP TABLE cristimc8joc";
            cmd.ExecuteNonQuery();
            connection.Close();
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "DROP TABLE cristimc8intrebari";
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("creat");
            */
            Login frm = new Login();
            frm.Show();
            this.Hide();
        }

        private void creaza_Load(object sender, EventArgs e)
        {

        }
    }
}
