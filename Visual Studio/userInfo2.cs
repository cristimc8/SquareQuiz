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
    public partial class userInfo2 : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public userInfo2()
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
            GoFullscreen(true);
        }


        public void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }
        }
        public string rang, privi, guild, culoare, lit, user, nm, prnm;

        private void Play_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public int points, wins, lost, patra;
        public string pic;

        private void userInfo2_Load(object sender, EventArgs e)
        {
            try
            {
                IntPtr handle = LoadCursorFromFile(main.cale + "Normal.cur");
                Cursor myCursor = new Cursor(handle);
                this.Cursor = myCursor;
            }
            catch (Exception x)
            {
                IntPtr handle = LoadCursorFromFile(main.backup + "Normal.cur");
                Cursor myCursor = new Cursor(handle);
                this.Cursor = myCursor;
            }
            this.BackColor = Color.FromArgb(14, 23, 33);
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select Nume, Prenume, Username from Jucatori where Username = '" + main.idinfo + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    nm = dr[0].ToString();
                    prnm = dr[1].ToString();
                    user = dr[2].ToString();
                }
            }
            connection.Close();
            nume.Text = user;
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "select Puncte, Rank, Castigate, Pierdute, Privilegiu, Clan, Patratele, Litera, Culoare, Poza from Jucatori where Username = '" + user + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    points = Convert.ToInt32(dr[0]);
                    wins = Convert.ToInt32(dr[2]);
                    lost = Convert.ToInt32(dr[3]);
                    rang = dr[1].ToString();
                    privi = dr[4].ToString();
                    guild = dr[5].ToString();
                    patra = Convert.ToInt32(dr[6]);
                    lit = dr[7].ToString();
                    culoare = dr[8].ToString();
                    pic = dr[9].ToString();
                }
            }
            connection.Close();
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
            preview.Text = lit;
            rank.Text = "Rank : " + rang;
            castigate.Text = "Meciuri castigate : " + wins.ToString();
            if (lost % 2 == 0) lost /= 2;
            else
            {
                lost /= 2;
                lost++;
            }
            pierdute.Text = "Meciuri pierdute : " + lost.ToString();
            puncte.Text = "Total puncte : " + points.ToString();
            clan.Text = "Clan : " + guild;
            privilegiu.Text = "Privilegiu : " + privi;
            patratele.Text = "Patratele : " + patra.ToString();
            label2.Text = nm;
            label3.Text = prnm;
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "select Tag from Clan where Nume = '" + guild + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    clan.Text += ", " + dr[0].ToString();
                }
            }
            connection.Close();
            try
            {
                pictureBox2.ImageLocation = main.backup + pic;
            }
            catch(Exception x)
            {
                pictureBox2.ImageLocation = main.cale + pic;
            }
        }
    }
}
