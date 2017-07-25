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
    public partial class square : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public string culoare;
        public square()
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
            preview.Text = textBox1.Text;
            culoare = comboBox1.Text;
            if (culoare == "Blue")
                preview.BackColor = Color.Blue;
            else if (culoare == "Red")
                preview.BackColor = Color.Red;
            else if (culoare == "Orange")
                preview.BackColor = Color.Orange;
            else if (culoare == "Yellow")
                preview.BackColor = Color.Yellow;
            else if (culoare == "Pink")
                preview.BackColor = Color.Pink;
            else if (culoare == "Purple")
                preview.BackColor = Color.Purple;
            else MessageBox.Show("Culoarea nu este disponibila !", "Color");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "update Jucatori set Litera = '" + textBox1.Text + "', Culoare = '" + comboBox1.Text + "' where Username = '" + Login.usr + "'";
            cmd.ExecuteNonQuery();
            connection.Close();
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "update Butoane set Culoare = '" + comboBox1.Text + "', Litera = '" + textBox1.Text + "' where NumeUser = '" + Login.usr + "'";
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Butonul a fost personalizat !", "Personalize");
        }

        private void preview_Click(object sender, EventArgs e)
        {

        }

        private void square_Load(object sender, EventArgs e)
        {

        }
    }
}
