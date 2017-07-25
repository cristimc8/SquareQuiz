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
    public partial class userInfo : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public userInfo()
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
        public string rang, privi, guild, culoare, lit;

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

        private void puncte_Click(object sender, EventArgs e)
        {

        }

        private void Play_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            poza pz = new poza();
            pz.Show();
        }

        private void nume_Click(object sender, EventArgs e)
        {

        }

        public int x = 10;
        private void timer1_Tick(object sender, EventArgs e)
        {
            x--;
            if(x == 5)
            {
                schimbaPoza();
                x = 10;
            }
        }

        public static string profil;
        public void schimbaPoza()
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select Poza from Jucatori where Username = '" + Login.usr + "'";
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    profil = dr[0].ToString();
                }
            }
            connection.Close();
            try
            {
                pictureBox2.ImageLocation = main.backup + profil;
            }
            catch (Exception x)
            {
                pictureBox2.ImageLocation = main.cale + profil;
            }
        }

        public int points, wins, lost, patra;
        private void userInfo_Load(object sender, EventArgs e)
        {
            castigate.ForeColor = Color.FromArgb(83, 180, 247);
            pierdute.ForeColor = Color.FromArgb(83, 180, 247);
            rank.ForeColor = Color.FromArgb(83, 180, 247);
            patratele.ForeColor = Color.FromArgb(83, 180, 247);
            nume.ForeColor = Color.LimeGreen;
            privilegiu.ForeColor = Color.FromArgb(83, 180, 247);
            clan.ForeColor = Color.FromArgb(83, 180, 247);
            puncte.ForeColor = Color.FromArgb(83, 180, 247);
            label1.ForeColor = Color.FromArgb(83, 180, 247);
            timer1.Start();
            schimbaPoza();
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
            nume.Text = Login.usr;
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select Puncte, Rank, Castigate, Pierdute, Privilegiu, Clan, Patratele, Litera, Culoare from Jucatori where Username = '" + Login.usr + "'";
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
            rank.Text = "Rankul tau : " + rang;
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
        }

        private void personalizeaza_Click(object sender, EventArgs e)
        {
            square frm = new square();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clan frmclan = new Clan();
            frmclan.Show();
        }


    }
}
