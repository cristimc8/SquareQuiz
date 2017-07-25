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
    public partial class creazaClan : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public creazaClan()
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
        public bool exista = false;
        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select Nume from Clan";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr[0].ToString() == name.Text)
                    {
                        exista = true;
                        break;
                    }
                }
            }
            connection.Close();
            if (!exista)
            {
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "insert into Clan values('" + name.Text + "', '" + Login.usr + "', '" + 1 + "', '" + 1 + "', '" + 10 + "', '" + desc.Text + "', '" + "<" + tag.Text + ">" + "')";
                cmd.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "update Jucatori set Clan = '" + name.Text + "' where Username = '" + Login.usr + "'";
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Clan succesfully created !", "Clan");
            }
            else
            {
                MessageBox.Show("Clan name already existing !", "Clan");
            }
        }

        private void creazaClan_Load(object sender, EventArgs e)
        {

        }

    }
}
