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
    public partial class poza : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public poza()
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

        private void poza_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(36, 53, 71);
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select Puncte from Jucatori where Username = '" + Login.usr + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    pp = Convert.ToInt16(dr[0]);
                }
            }
            connection.Close();
            label14.Text = pp.ToString() + " puncte";
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        public int pp, x = 0;
        private void cumpara(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            switch (btn.Name)
            {
                case "standard":
                    x = 0;
                    MessageBox.Show("Poza a fost schimbata !");
                    cmd.CommandText = "update Jucatori set Poza = '" + btn.Name + ".jpg' where Username = '" + Login.usr + "'";
                    cmd.ExecuteNonQuery();
                    break;
                case "spider":
                    x = 0;
                    MessageBox.Show("Poza a fost schimbata !");
                    cmd.CommandText = "update Jucatori set Poza = '" + btn.Name + ".jpg' where Username = '" + Login.usr + "'";
                    cmd.ExecuteNonQuery();
                    break;
                case "banana":
                    x = 0;
                    MessageBox.Show("Poza a fost schimbata !");
                    cmd.CommandText = "update Jucatori set Poza = '" + btn.Name + ".jpg' where Username = '" + Login.usr + "'";
                    cmd.ExecuteNonQuery();
                    break;
                case "ghost":
                    if (pp >= 50)
                    {
                        x = 50;
                        cmd.CommandText = "update Jucatori set Poza = '" + btn.Name + ".jpg' where Username = '" + Login.usr + "'";
                        MessageBox.Show("Poza a fost schimbata !");
                        cmd.ExecuteNonQuery();
                    }
                    else MessageBox.Show("Nu ai destule puncte !");
                    break;
                case "cool1":
                    if (pp >= 50)
                    {
                        x = 50;
                        cmd.CommandText = "update Jucatori set Poza = '" + btn.Name + ".jpg' where Username = '" + Login.usr + "'";
                        MessageBox.Show("Poza a fost schimbata !");
                        cmd.ExecuteNonQuery();
                    }
                    else MessageBox.Show("Nu ai destule puncte !");
                    break;
                case "bean":
                    if (pp >= 100)
                    {
                        x = 100;
                        cmd.CommandText = "update Jucatori set Poza = '" + btn.Name + ".jpg' where Username = '" + Login.usr + "'";
                        MessageBox.Show("Poza a fost schimbata !");
                        cmd.ExecuteNonQuery();
                    }
                    else MessageBox.Show("Nu ai destule puncte !");
                    break;
                case "joffrey":
                    if (pp >= 150)
                    {
                        x = 150;
                        cmd.CommandText = "update Jucatori set Poza = '" + btn.Name + ".jpg' where Username = '" + Login.usr + "'";
                        MessageBox.Show("Poza a fost schimbata !");
                        cmd.ExecuteNonQuery();
                    }
                    else MessageBox.Show("Nu ai destule puncte !");
                    break;
                case "jonsnow":
                    if (pp >= 150)
                    {
                        x = 150;
                        cmd.CommandText = "update Jucatori set Poza = '" + btn.Name + ".jpg' where Username = '" + Login.usr + "'";
                        MessageBox.Show("Poza a fost schimbata !");
                        cmd.ExecuteNonQuery();
                    }
                    else MessageBox.Show("Nu ai destule puncte !");
                    break;
                case "dany":
                    if (pp >= 150)
                    {
                        x = 150;
                        cmd.CommandText = "update Jucatori set Poza = '" + btn.Name + ".jpg' where Username = '" + Login.usr + "'";
                        MessageBox.Show("Poza a fost schimbata !");
                        cmd.ExecuteNonQuery();
                    }
                    else MessageBox.Show("Nu ai destule puncte !");
                    break;
                case "arya":
                    if (pp >= 150)
                    {
                        x = 150;
                        cmd.CommandText = "update Jucatori set Poza = '" + btn.Name + ".jpg' where Username = '" + Login.usr + "'";
                        MessageBox.Show("Poza a fost schimbata !");
                        cmd.ExecuteNonQuery();
                    }
                    else MessageBox.Show("Nu ai destule puncte !");
                    break;
            }
            connection.Close();
            connection.Open();
            cmd = connection.CreateCommand();
            pp -= x;
            cmd.CommandText = "update Jucatori set Puncte = '" + pp + "' where Username = '" + Login.usr + "'";
            cmd.ExecuteNonQuery();
            connection.Close();
            label14.Text = pp.ToString() + " puncte";
        }
    }
}
