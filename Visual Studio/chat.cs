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
    public partial class chat : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public chat()
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

        private void send_Click(object sender, EventArgs e)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            if (main.Contor % 2 == 1)
                cmd.CommandText = "insert into " + main.coleg + " values('" + "NULL" + "', '" + Login.usr + "', '" + textBox2.Text + "', true)";
            else
                cmd.CommandText = "insert into " + Login.usr + " values('" + "NULL" + "', '" + Login.usr + "', '" + textBox2.Text + "', true)";
            cmd.ExecuteNonQuery();
            connection.Close();
            textBox2.Clear();
        }

        private void deconect_Click(object sender, EventArgs e)
        {
            
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            if (main.Contor % 2 == 1)
                cmd.CommandText = "update " + main.coleg + " set Activ = false";
            else
                cmd.CommandText = "update " + Login.usr + " set Activ = false";
            connection.Open();
            if (main.Contor % 2 == 1)
                cmd.CommandText = "DROP TABLE " + main.coleg + "";
            else
                cmd.CommandText = "DROP TABLE " + Login.usr + "";
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void updateChat()
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            if (main.Contor % 2 == 1)
                cmd.CommandText = "select Activ from " + main.coleg + "";
            else
                cmd.CommandText = "select Activ from " + Login.usr + "";
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    if(Convert.ToBoolean(dr[0]) == false)
                    {
                        timer1.Stop();
                        textBox2.Hide();
                        listBox1.Items.Add("Partenerul s-a deconectat de la chat.");
                    }
                }
            }
            connection.Close();
            listBox1.Items.Clear();
            connection.Open();
            cmd = connection.CreateCommand();
            if (main.Contor % 2 == 1)
                cmd.CommandText = "select ID, User, Mesaj from " + main.coleg + "";
            else
                cmd.CommandText = "select ID, User, Mesaj from " + Login.usr + "";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    listBox1.Items.Add(dr[1].ToString() + " : " + dr[2].ToString());
                }
            }
            connection.Close();

        }


        private void chat_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.BackColor = Color.FromArgb(13, 34, 66);
            if (main.Contor % 2 == 1)
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "CREATE TABLE " + main.coleg + " (ID INT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY, User VARCHAR(256), Mesaj VARCHAR(256), Activ BOOLEAN )";
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch(Exception x)
                {
                    MessageBox.Show("Utilizatorul este angajat intr-un alt chat deja.");
                    this.Hide();
                    timer1.Stop();
                    connection.Close();
                }
            }
        }

        public int i = 2;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i--;
            if (i == 0)
            {
                updateChat();
                i = 2;
            }
        }
    }
}
