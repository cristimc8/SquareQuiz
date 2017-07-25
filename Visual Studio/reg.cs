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
    public partial class reg : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public reg()
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
            bool exista = false;
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
                        exista = true;
                        break;
                    }
                }
            }
            connection.Close();
            if ((user.Text != "") && (pass.Text != "") && (nume.Text != "") && (prenume.Text != "") && (!exista))
            {
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "insert into Jucatori values(NULL, '" + nume.Text + "', '" + prenume.Text + "', '" + user.Text + "', '" + pass.Text + "', '" + 50 + "', '" + "Incepator" + "', '" + 0 + "', '" + 0 + "', '" + "Utilizator" + "', '" + "Niciunul" + "', '" + 0 + "', '" + user.Text[0].ToString() + "', '" + "Blue" + "', '" + "standard.jpg" + "')";
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Cont creat cu succes !", "Register");
            }
            else
            {
                MessageBox.Show("Username-ul este luat !", "Register");
            }
           
        }

        private void reg_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
