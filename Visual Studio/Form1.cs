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

    public partial class main : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);
        public bool cumpara = false;
        public bool edit = false;
        public int contor = 0, x = 0;
        public static string id, idinfo;
        public static int puncte, patra;
        public string[] nume = new string[522];
        public string[] cul = new string[522];
        public string[] lite = new string[522];
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public main()
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

        private void Play_Click(object sender, EventArgs e)
        {
            invite frmplay = new invite();
            frmplay.Show();
        }

        private void welcome_Click(object sender, EventArgs e)
        {
            userInfo frm = new userInfo();
            frm.Show();
        }

        public static string cale, backup;
        public static string poza;
        private void main_Load(object sender, EventArgs e)
        {
            
            cale = Application.StartupPath;
            backup = cale;
            cale = cale.Substring(0, cale.Length - 9);
            cale += "Resources/";
            try
            {
                IntPtr handle = LoadCursorFromFile(cale + "Normal.cur");
                Cursor myCursor = new Cursor(handle);
                this.Cursor = myCursor;
            }
           catch(Exception x)
           {
                backup += "/Resources/";
                IntPtr handle = LoadCursorFromFile(backup + "Normal.cur");
                Cursor myCursor = new Cursor(handle);
                this.Cursor = myCursor;
            }
            updateConectati();
            updateChat();
            welcome.Text += " " + Login.usr;
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select Username, Patratele, Poza from Jucatori where Username = '" + Login.usr + "'";
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    id = dr[0].ToString();
                    patra = Convert.ToInt32(dr[1]);
                    poza = dr[2].ToString();
                }
            }
            connection.Close();
            try
            {
                pictureBox1.ImageLocation = backup + poza;
            }
            catch(Exception x)
            {
                pictureBox1.ImageLocation = cale + poza;
            }
            connection.Open();
            cmd.CommandText = "select NumeButon, Culoare, Litera from Butoane";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    nume[x] = dr[0].ToString();
                    lite[x] = dr[2].ToString();
                    cul[x++] = dr[1].ToString();
                }
            }
            connection.Close();
            for (int i = 1; i < 449; i++)
            {
                for(int j = 0; j < x; j++)
                    if ("button" + i.ToString() == nume[j])
                    {
                        if (cul[j] == "Blue")
                            this.Controls["button" + i.ToString()].BackColor = Color.Blue;
                        else if (cul[j] == "Red")
                            this.Controls["button" + i.ToString()].BackColor = Color.Red;
                        else if (cul[j] == "Orange")
                            this.Controls["button" + i.ToString()].BackColor = Color.Orange;
                        else if (cul[j] == "Yellow")
                            this.Controls["button" + i.ToString()].BackColor = Color.Yellow;
                        else if (cul[j] == "Pink")
                            this.Controls["button" + i.ToString()].BackColor = Color.Pink;
                        else if (cul[j] == "Purple")
                            this.Controls["button" + i.ToString()].BackColor = Color.Purple;
                        this.Controls["button" + i.ToString()].Text = lite[j];
                        break;
                    }
            }
        }
        public bool liber(string but)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandText = "select Culoare from Butoane where NumeButon = '" + but + "'";
                dr = cmd.ExecuteReader();
                connection.Close();
            }
            catch (Exception k)
            {
                connection.Close();
                return false;
            }
            return true;
        }
        public string culoare, lit;
        private void draw(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            if ((edit) && (but.BackColor == SystemColors.Control) && (liber(but.Name)))
            {
                if (checkPoints())
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select Litera, Culoare from Jucatori where Username = '" + Login.usr + "'";
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lit = dr[0].ToString();
                            culoare = dr[1].ToString();
                        }
                    }
                    connection.Close();
                    but.Text = lit;
                    if (culoare == "Blue")
                        but.BackColor = Color.Blue;
                    else if (culoare == "Red")
                        but.BackColor = Color.Red;
                    else if (culoare == "Orange")
                        but.BackColor = Color.Orange;
                    else if (culoare == "Yellow")
                        but.BackColor = Color.Yellow;
                    else if (culoare == "Pink")
                        but.BackColor = Color.Pink;
                    else if (culoare == "Purple")
                        but.BackColor = Color.Purple;
                    puncte -= 10;
                    patra++;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "update Jucatori set Puncte = '" + puncte + "', Patratele = '" + patra + "' where Username = '" + Login.usr + "'";
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "insert into Butoane values('" + id + "', '" + but.Name + "', '" + culoare + "', '" + lit + "')";
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Not enough points to buy this square !", "Buy");
                }
            }
            else if (but.BackColor != SystemColors.Control)
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select NumeUser from Butoane where NumeButon = '" + but.Name + "'";
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        idinfo = dr[0].ToString();
                    }
                }
                connection.Close();
                userInfo2 frminfo = new userInfo2();
                frminfo.Show();
            }
        }
        private bool checkPoints()
        {
            bool own = false;
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select Puncte from Jucatori where Username = '" + Login.usr + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    puncte = Convert.ToInt32(dr[0]);
                }
            }
            connection.Close();
            if (puncte >= 10)
                own = true;
            return own;
        }
        private void blank()
        {
            Play.Enabled = false;
            welcome.Enabled = false;
            Training.Enabled = false;
            boost.Enabled = false;
        }
        private void coloreaza()
        {
            Play.Enabled = true;
            welcome.Enabled = true;
            Training.Enabled = true;
            boost.Enabled = true;
        }

        private void buy_Click(object sender, EventArgs e)
        {
            contor++;
            if (contor % 2 == 1)
            {
                MessageBox.Show("Apasa pe patratelul pe care vrei sa-l cumperi !", "Buy");
                blank();
                edit = true;
                buy.Text = "Cancel";
            }
            else
            {
                coloreaza();
                edit = false;
                buy.Text = "Buy squares";
            }
        }

        private void button449_Click(object sender, EventArgs e)
        {
            intrebari qu = new intrebari();
            qu.Show();
        }

        private void Training_Click(object sender, EventArgs e)
        {
            Traininig train = new Traininig();
            train.Show();
        }

        private void send_Click(object sender, EventArgs e)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "insert into GlobalChat values('" + Login.usr + "', '" + textBox1.Text + "')";
            cmd.ExecuteNonQuery();
            connection.Close();
            textBox1.Clear();
        }

        public void updateChat()
        {
            int x = listBox2.Items.Count;
            listBox2.Items.Clear();
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select User, Mesaj from GlobalChat";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    listBox2.Items.Add(dr[0].ToString() + " : " + dr[1].ToString());
                    // listBox1.Text += dr[0].ToString() + " : " + dr[1].ToString() + Environment.NewLine;
                }
            }
            connection.Close();
            if(listBox2.Items.Count > x)
            {
                try
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(cale + "alert.wav");
                    player.Play();
                }
                catch (Exception y)
                {
                  //  MessageBox.Show(backup + "alert.wav");
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(backup + "alert.wav");
                    player.Play();
                }
            }
            if(listBox2.Items.Count == 15)
            {
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "TRUNCATE GlobalChat";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool existaChat()
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from " + Login.usr + "";
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

        public void updateConectati()
        {
            listBox1.Items.Clear();
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select User from Conectati";
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

        public void creazaChat()
        {
            chat frmc = new chat();
            frmc.Show();
        }

        public int i = 5;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i--;
            if ((i == 4) || (i == 2))
                updateChat();
            if (i == 0)
            {
                updateChat();
                updateConectati();
                schimbaPoza();
                i = 5;
            }
        }

        public void schimbaPoza()
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select Poza from Jucatori where Username = '" + Login.usr + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    userInfo.profil = dr[0].ToString();
                }
            }
            connection.Close();
            try
            {
                pictureBox1.ImageLocation = main.backup + userInfo.profil;
            }
            catch (Exception x)
            {
                pictureBox1.ImageLocation = main.cale + userInfo.profil;
            }
        }
        public int j = 5;
        private void timer2_Tick(object sender, EventArgs e)
        {
            j--;
            if (j == 0)
            {
                j = 5;
                if (existaChat())
                {
                    creazaChat();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void boost_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Optiunea 'cumpara grad' va fi valabila in curand !");
        }

        private void button451_Click(object sender, EventArgs e)
        {
            Help frm = new Help();
            frm.Show();
        }

        private void deconect_Click(object sender, EventArgs e)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "delete from Conectati where User = '" + Login.usr + "'";
            cmd.ExecuteNonQuery();
            connection.Close();
            Application.Exit();
        }

        public static string coleg;
        public static int Contor = 0;
        private void button450_Click(object sender, EventArgs e)
        {
            Contor++;
            coleg = textBox2.Text;
            // creazaChat();
            MessageBox.Show("Optiune PM va fi valabila in curand !");
        }
    }
}
