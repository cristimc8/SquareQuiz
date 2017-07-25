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
    public partial class gameRoom : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public gameRoom()
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

        private void gameRoom_Load(object sender, EventArgs e)
        {

        }

        private void deconect_Click(object sender, EventArgs e)
        {

        }

        private void send_Click(object sender, EventArgs e)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            if (main.Contor % 2 == 1)
                cmd.CommandText = "insert into " + main.coleg + "joc values('" + "NULL" + "', '" + Login.usr + "', '" + textBox2.Text + "')";
            else
                cmd.CommandText = "insert into " + Login.usr + "joc values('" + "NULL" + "', '" + Login.usr + "', '" + textBox2.Text + "')";
            cmd.ExecuteNonQuery();
            connection.Close();
            textBox2.Clear();
        }
    }
}
