using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CM
{
    public partial class Login : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public Login()
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
        public static string usr;
        private void button1_Click(object sender, EventArgs e)
        {
             bool gasit = false;
             bool conectat = false;
             connection.Open();
             MySqlCommand cmd = connection.CreateCommand();
             cmd.CommandText = "select Username from Jucatori";
             dr = cmd.ExecuteReader();
             if (dr.HasRows)
             {
                 while (dr.Read())
                 {
                     if (user.Text == dr[0].ToString())
                     {
                         gasit = true;
                         break;
                     }
                 }
             }
             connection.Close();
             if (gasit)
             {
                 connection.Open();
                 cmd = connection.CreateCommand();
                 cmd.CommandText = "select Parola from Jucatori where Username = '" + user.Text + "'";
                 dr = cmd.ExecuteReader();
                 if (dr.HasRows)
                 {
                     while (dr.Read())
                     {
                         if (pass.Text == dr[0].ToString())
                         {
                             conectat = true;
                             break;
                         }
                     }
                 }
                 connection.Close();
             }
             if (conectat)
             {
                 this.Hide();
                 main frm1 = new main();
                 usr = user.Text;
                 connection.Open();
                 cmd = connection.CreateCommand();
                 cmd.CommandText = "Insert into Conectati values ('" + usr + "')";
                 cmd.ExecuteNonQuery();
                 connection.Close();
                 frm1.Show();
             }
             if ((!gasit) || (!conectat))
             {
                 MessageBox.Show("Username sau parola gresite !", "Login");
             }
           /* connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "CREATE TABLE joc" + " ( ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY, User VARCHAR(256), Inceput BOOLEAN )";
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("creat");*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reg frm = new reg();
            frm.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
