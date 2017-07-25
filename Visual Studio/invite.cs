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
    public partial class invite : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public invite()
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

        private void invite_Load(object sender, EventArgs e)
        {
            updateSesiuni();
        }

        public static int DEF = 12; //eventual functia pt automatizare DEF
        public int[] aparute = new int[DEF + 1];
        public int v = 0;
        public string var1, var2, var3, var4, que;
        public int p1, p2, p3, p4;


        public int i = 5;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i--;
            if (i == 0)
            {
                updateSesiuni();
                i = 5;
            }
        }

        public void updateSesiuni()
        {
            listBox1.Items.Clear();
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select NumeSesiune from Sesiuni";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    listBox1.Items.Add(dr[0].ToString());
                }
            }
            connection.Close();
        }

        public static int p;
        public static bool host = false;
        public string ses = Login.usr + "joc";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                p = 1;
                host = true;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "CREATE TABLE " + Login.usr + "joc" + " ( ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY, User VARCHAR(256), Inceput BOOLEAN )";
                cmd.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "CREATE TABLE " + Login.usr + "intrebari" + " ( ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY, intrebare VARCHAR(256), g1 VARCHAR(256), g2 VARCHAR(256), g3 VARCHAR(256), corect VARCHAR(256), poz1 INT(255), poz2 INT(255), poz3 INT(255), poz4 INT(255), p1 INT(255), p2 INT(255), p3 INT(255), p4 INT(255), pp1 INT(32), pp2 INT(32), pp3 INT(32), pp4 INT(32))";
                cmd.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "insert into " + Login.usr + "joc values(NULL, '" + Login.usr + "', '" + "NULL" + "')";
                cmd.ExecuteNonQuery();
                connection.Close();
                hostSesiune hostGame = new hostSesiune();
                hostGame.Show();
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "insert into Sesiuni values('" + ses + "')";
                cmd.ExecuteNonQuery();
                connection.Close();
                for (int i = 0; i < DEF; i++)
                    download();
            }
            catch
            {
                for (int i = 0; i < DEF; i++)
                    aparute[i] = -1;
                v = 0;
                var1 = null; var2 = null; var3 = null; var4 = null;
                try
                {
                    connection.Close();
                }
                   catch(Exception x)
                {

                }
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DROP TABLE " + Login.usr + "joc";
                cmd.ExecuteNonQuery();
                cmd = connection.CreateCommand();
                cmd.CommandText = "DROP TABLE " + Login.usr + "intrebari";
                cmd.ExecuteNonQuery();
                cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Sesiuni where NumeSesiune = '" + ses + "'";
                cmd.ExecuteNonQuery();
                connection.Close();
                p = 1;
                host = true;
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "CREATE TABLE " + Login.usr + "joc" + " ( ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY, User VARCHAR(256), Inceput BOOLEAN )";
                cmd.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "CREATE TABLE " + Login.usr + "intrebari" + " ( ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY, intrebare VARCHAR(256), g1 VARCHAR(256), g2 VARCHAR(256), g3 VARCHAR(256), corect VARCHAR(256), poz1 INT(255), poz2 INT(255), poz3 INT(255), poz4 INT(255), p1 INT(255), p2 INT(255), p3 INT(255), p4 INT(255), pp1 INT(32), pp2 INT(32), pp3 INT(32), pp4 INT(32))";
                cmd.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "insert into " + Login.usr + "joc values(NULL, '" + Login.usr + "', '" + "NULL" + "')";
                cmd.ExecuteNonQuery();
                connection.Close();
                hostSesiune hostGame = new hostSesiune();
                hostGame.Show();
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "insert into Sesiuni values('" + ses + "')";
                cmd.ExecuteNonQuery();
                connection.Close();
                for (int i = 0; i < DEF; i++)
                    download();
            }
            
        }

        public static string numeIntrebari;
        public static string numeSesiune;
        private void button2_Click(object sender, EventArgs e)
        {
            host = false;
            numeSesiune = textBox1.Text;
            numeIntrebari = numeSesiune.Substring(0, numeSesiune.Length - 3);
            numeIntrebari += "intrebari";
            if (existaSesiune())
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into " + numeSesiune + " values(NULL, '" + Login.usr + "', '" + "NULL" + "')";
                cmd.ExecuteNonQuery();
                connection.Close();
                hostSesiune gameRoom = new hostSesiune();
                gameRoom.Show();
            }
            else
            {
                MessageBox.Show("Sesiunea nu exista !");
            }
        }

        public bool existaSesiune()
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from " + textBox1.Text + "";
                dr = cmd.ExecuteReader();
                connection.Close();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void download()
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
                        que = dr[0].ToString();
                        var1 = dr[1].ToString();
                        var2 = dr[2].ToString();
                        var3 = dr[3].ToString();
                        var4 = dr[4].ToString();
                    }
                }
                connection.Close();
                p1 = shuffle(p1); p2 = shuffle(p2); p3 = shuffle(p3); p4 = shuffle(p4);
                upload(que, var1, var2, var3, var4, p1, p2, p3, p4);
                for (int i = 0; i < 4; i++)
                    pozitii[i] = 0;
            }
        }
        public int[] pozitii = new int[4];
        public int n = 0;
        public void upload(string a, string b, string c, string d, string e, int f, int g, int h, int i)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "insert into " + Login.usr + "intrebari values( NULL, '" + a + "', '" + b + "', '" + c + "', '" + d + "', '" + e + "', '" + f + "', '" + g + "', '" + h + "', '" + i + "', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)";
            cmd.ExecuteNonQuery();
            connection.Close();
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

        public int shuffle(int z)
        {
            Random k = new Random();
            int x = k.Next(1, 5);
            while (wasShuffled(x))
            {
                k = new Random();
                x = k.Next(1, 5);
            }
            pozitii[n++] = x;
            if (n == 4) n = 0;
            return x;
        }

        public bool wasShuffled(int z)
        {
            bool repetat = false;
            for (int k = 0; k < n; k++)
            {
                if (pozitii[k] == z)
                    repetat = true;
            }
            return repetat;
        }
    }
}
