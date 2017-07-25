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
    public partial class Traininig : Form
    {
        public static int DEF = 15;
        public int turn = 14;
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
       // public int[] neaparute = new int[15];
        public int v = 0, poz;
        public string var1, var2, var3, var4;
        MySqlDataReader dr;
        public Traininig()
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

        private void Traininig_Load(object sender, EventArgs e)
        {
            bagaIntrebari();
            label2.Hide();
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select id from Intrebari";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DEF = Convert.ToInt32(dr[0]);
                }
            }
            connection.Close();
            turn = DEF;
        }
        public int[] aparute = new int[DEF + 1];
        public int i = 15;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i--;
            label1.Text = i.ToString() + " secunde ramase";
            if (i == 0)
            {
                disarm();
                evalueaza();
            }
        }
        public void disarm()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            timer1.Enabled = false;
            i = 15;
        }

        public void blank()
        {
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
        }

        public void arm()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            timer1.Enabled = true;
            timer2.Enabled = false;
            button1.BackColor = SystemColors.Control;
            button2.BackColor = SystemColors.Control;
            button3.BackColor = SystemColors.Control;
            button4.BackColor = SystemColors.Control;
            label1.Show();
            label2.Hide();
            j = 5;
            bagaIntrebari();
        }

        public void evalueaza()
        {
            label1.Hide();
            label2.Show();
            timer2.Enabled = true;
            turn--;
            label3.Text = turn.ToString() + " intrebari ramase";
        }

        public int j = 5;
        private void timer2_Tick(object sender, EventArgs e)
        {
            j--;
            if (j == 0)
            {
                blank();
                arm();
            }
        }

        public bool Repeta(int z)
        {
            bool repetat = false;
            for (int k = 0; k < v; k++)
            {
                if (aparute[k] == z)
                    repetat = true;
            }
            return repetat;
        }

        public bool asezate()
        {
            if ((button1.Text != "") && (button2.Text != "") && (button3.Text != "") && (button4.Text != ""))
                return true;
            else return false;
        }

        public void aseaza(string alfa)
        {
            Random y = new Random();
            if (!asezate())
            {
                poz = y.Next(1, 5);
                while (this.Controls["button" + poz.ToString()].Text != "")
                {
                    poz = y.Next(1, 5);
                }
            }
            this.Controls["button" + poz.ToString()].Text = alfa;
        }

        public void bagaIntrebari()
        {
            if (turn >= 1)
            {
                Random x = new Random();
                int a = x.Next(1, DEF + 1);
                while (Repeta(a))
                {
                    x = new Random();
                    a = x.Next(1, DEF + 1);
                }
                if (!Repeta(a))
                {
                    aparute[v++] = a;
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select intrebare, g1, g2, g3, corect from Intrebari where id = '" + a + "'";
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            textBox1.Text = dr[0].ToString();
                            var1 = dr[1].ToString();
                            var2 = dr[2].ToString();
                            var3 = dr[3].ToString();
                            var4 = dr[4].ToString();
                        }
                    }
                    connection.Close();
                    aseaza(var1);
                    aseaza(var2);
                    aseaza(var3);
                    aseaza(var4);

                }
            }
            else
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                this.Hide();
                MessageBox.Show("Felicitari!!");
            }
        }

        private void raspuns(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            if (but.Text == var4)
            {
                evalueaza();
                disarm();
                but.BackColor = Color.Green;
            }
            else
            {
                evalueaza();
                disarm();
                but.BackColor = Color.Red;
            }
        }
    }
}
