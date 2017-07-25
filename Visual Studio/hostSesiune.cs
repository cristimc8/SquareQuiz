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
    public partial class hostSesiune : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataReader dr;
        public hostSesiune()
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

        private void hostSesiune_Load(object sender, EventArgs e)
        {
            updateSesiune();
            pictureBox1.Hide();
            pictureBox2.Hide();
            pictureBox3.Hide();
            pictureBox4.Hide();
            preview.Hide();
            numePlayer.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            button1.Hide();
            textBox1.Hide();
            label3.Hide();
            label2.Hide();
            label4.Hide();
            if (!invite.host)
            {
                start.Hide();
                invite.p = listBox1.Items.Count;
            }
        }

        public void updateSesiune()
        {
            listBox1.Items.Clear();
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            if (invite.host)
                cmd.CommandText = "select User from " + Login.usr + "joc";
            else
                cmd.CommandText = "select User from " + invite.numeSesiune + "";
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

        public int i = 3;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i--;
            if (i == 0)
            {
                updateSesiune();
                i = 3;
            }
        }

        public int j = 1;
        private void timer2_Tick(object sender, EventArgs e)
        {
            j--;
            if (j == 0)
            {
                if (aInceput())
                {
                    timer1.Stop();
                    timer2.Stop();
                    startJoc();
                }
                j = 1;
            }
        }

        public bool varb;
        public bool aInceput()
        {
            varb = false;
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            if (!invite.host)
                cmd.CommandText = "select Inceput from " + invite.numeSesiune + "";
            else
                cmd.CommandText = "select Inceput from " + Login.usr + "joc";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (Convert.ToBoolean(dr[0]) == true)
                    {
                        varb = true;
                    }
                }
            }
            connection.Close();
            if (varb) return true;
            else return false;
        }

        public int jk;
        public string[] users = new string[4];
        public static int DEF, turn;
        public void preparaPentruJoc()
        {
            preview.Hide();
            numePlayer.Hide();
            label5.Hide();
            label4.Show();
            label3.Show();
            label1.Hide();
            listBox1.Hide();
            start.Hide();
            timer1.Stop();
            timer2.Stop();
            timer3.Start();
            textBox1.Show();
            button2.Show();
            button3.Show();
            button4.Show();
            button1.Show();
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
            turn = 10;

        }
        // JOCUL PROPRIU ZIS
        public int v = 0, poz;
        public string var1, var2, var3, var4;
        public int x = 15;
        public int y = 5;
        private void timer4_Tick(object sender, EventArgs e)
        {
            y--;
            if (y == 0)
            {
                blank();
                arm();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            x--;
            label3.Text = x + " secunde ramase";
            if (x == 0)
            {
                disarm();
                evalueaza();
            }
        }
        public int[] aparute = new int[DEF + 1];

        public void startJoc()
        {
            meeting();
        }

        public void meeting()
        {
            jk = listBox1.Items.Count;
            for (int i = 0; i < jk; i++)
            {
                users[i] = listBox1.Items[i].ToString();
            }
            preview.Show();
            numePlayer.Show();
            pictureBox1.Show();
            pictureBox2.Show();
            pictureBox3.Show();
            pictureBox4.Show();
            timer5.Start();
            listBox1.Hide();
            label1.Hide();
            label5.Hide();
        }

        public void disarm()
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button1.Enabled = false;
            timer3.Enabled = false;
            x = 15;
        }

        public void arm()
        {
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button1.Enabled = true;
            timer3.Enabled = true;
            timer4.Enabled = false;
            button1.BackColor = SystemColors.Control;
            button2.BackColor = SystemColors.Control;
            button3.BackColor = SystemColors.Control;
            button4.BackColor = SystemColors.Control;
            label3.Show();
            label2.Hide();
            y = 5;
            bagaIntrebariUser();
        }

        public void blank()
        {
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button1.Text = "";
        }

        public void evalueaza()
        {
            veziRaspunsuri();
            index++;
            label3.Hide();
            label2.Show();
            timer4.Enabled = true;
            turn--;
            label4.Text = turn.ToString() + " intrebari ramase";
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
            if ((button2.Text != "") && (button3.Text != "") && (button4.Text != "") && (button1.Text != ""))
                return true;
            else return false;
        }

        public int upP;
        public int apasat;
        private void actiune(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            for(int i = 1; i < 5; i++)
            {
                this.Controls["button" + i.ToString()].BackColor = SystemColors.Control;
            }
            but.BackColor = Color.DimGray;
            upP = Convert.ToInt16(but.Name.Substring(but.Name.Length - 1, 1));
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            if (!invite.host)
                cmd.CommandText = "update " + invite.numeIntrebari + " set p" + invite.p + " = " + upP + " where ID = " + index + "";
            else
                cmd.CommandText = "update " + Login.usr + "intrebari set p" + invite.p + " = " + upP + " where ID = " + index + "";
            cmd.ExecuteNonQuery();
            connection.Close();
            apasat = upP;
            upP = 0;
        }
        public int indexx, p1, p2, p3, p4;
        public void veziRaspunsuri()
        {
            indexx = index - 1;
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            if (!invite.host)
                cmd.CommandText = "select p1, p2, p3, p4 from " + invite.numeIntrebari + " where ID = '" + index + "'";
            else
                cmd.CommandText = "select p1, p2, p3, p4 from " + Login.usr + "intrebari where ID = '" + index + "'";
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    try
                    {
                        p1 = Convert.ToInt16(dr[0]);
                        p2 = Convert.ToInt16(dr[1]);
                        p3 = Convert.ToInt16(dr[2]);
                        p4 = Convert.ToInt16(dr[3]);
                    }
                    catch(Exception x)
                    {

                    }
                }
            }
            connection.Close();
            coloreaza(p1); coloreaza(p2); coloreaza(p3); coloreaza(p4);
            label6.Text = puncte.ToString() + " puncte";
        }

        public int poz1, poz2, poz3, poz4;

        private void start_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update " + Login.usr + "joc set Inceput = True";
                cmd.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Sesiuni where NumeSesiune = '" + Login.usr + "joc'";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception x)
            {
                connection.Close();
                MessageBox.Show(x.ToString());
            }
        }

        public int index = 1;
        public void bagaIntrebariUser()
        {
            if (turn >= 1)
            {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                if(!invite.host)
                    cmd.CommandText = "select intrebare, g1, g2, g3, corect, poz1, poz2, poz3, poz4 from " + invite.numeIntrebari + " where ID = '" + index + "'";
                else
                    cmd.CommandText = "select intrebare, g1, g2, g3, corect, poz1, poz2, poz3, poz4 from " + Login.usr + "intrebari where ID = '" + index + "'";
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
                            poz1 = Convert.ToInt16(dr[5]);
                            poz2 = Convert.ToInt16(dr[6]);
                            poz3 = Convert.ToInt16(dr[7]);
                            poz4 = Convert.ToInt16(dr[8]);
                        }
                    }
                    connection.Close();
                    gasesteLoc(var1, poz1);
                    gasesteLoc(var2, poz2);
                    gasesteLoc(var3, poz3);
                    gasesteLoc(var4, poz4);
            }
            else
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                if(!invite.host)
                    cmd.CommandText = "update " + invite.numeIntrebari + " set pp" + invite.p + " = " + puncte + "";
                else
                    cmd.CommandText = "update " + Login.usr + "intrebari set pp" + invite.p + " = " + puncte + "";
                cmd.ExecuteNonQuery();
                connection.Close();
                this.Hide();
                timer2.Stop();
                timer3.Stop();
                timer4.Stop();
                clasament();
            }
        }

        int gg = 0;
        public void faPreview(int k)
        {
            gg++;
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select Poza from Jucatori where Username = '" + users[k] + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    pic = dr[0].ToString();
                }
            }
            connection.Close();
            try
            {
                preview.ImageLocation = main.backup + pic;
                try
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(main.cale + "apar.wav");
                    player.Play();
                }
                catch (Exception y)
                {
                    //  MessageBox.Show(backup + "alert.wav");
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(main.backup + "apar.wav");
                    player.Play();
                }
                switch (gg)
                {
                    case 1:
                        pictureBox1.ImageLocation = main.backup + pic;
                        break;
                    case 2:
                        pictureBox2.ImageLocation = main.backup + pic;
                        break;
                    case 3:
                        pictureBox3.ImageLocation = main.backup + pic;
                        break;
                    case 4:
                        pictureBox4.ImageLocation = main.backup + pic;
                        break;
                }
            }
            catch
            {
                preview.ImageLocation = main.cale + pic;
            }
            numePlayer.Text = users[k];
        }
        public int h = 5, ord = 0;
        public string pic;

        private void timer5_Tick(object sender, EventArgs e)
        {
            if(h == 5)
            {
                faPreview(ord);
            }
            h--;
            if(h == 0)
            {
                ord++;
                h = 4;
                if(ord == jk)
                {
                    timer5.Stop();
                    preview.Hide();
                    numePlayer.Hide();
                    preparaPentruJoc();
                    bagaIntrebariUser();
                }
                else
                    faPreview(ord);
            }
        }

        public int xxx;
        public void clasament()
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select Puncte from Jucatori where Username = '" + Login.usr + "'";
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    xxx = Convert.ToInt16(dr[0]);
                }
            }
            connection.Close();
            xxx += 15;
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "update Jucatori set Puncte = '" + xxx + "' where Username = '" + Login.usr + "'";
            cmd.ExecuteNonQuery();
            connection.Close();
            connection.Open();
            cmd = connection.CreateCommand();
            if(!invite.host)
                cmd.CommandText = "select pp1, pp2, pp3, pp4 from " + invite.numeIntrebari + " where ID = '" + index + "'";
            else
                cmd.CommandText = "select pp1, pp2, pp3, pp4 from " + Login.usr + "intrebari where ID = '" + index + "'";
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    try
                    {
                        vectorPuncte[contorVector++] = Convert.ToInt16(dr[0]);
                        vectorPuncte[contorVector++] = Convert.ToInt16(dr[1]);
                        vectorPuncte[contorVector++] = Convert.ToInt16(dr[2]);
                        vectorPuncte[contorVector++] = Convert.ToInt16(dr[3]);
                    }
                    catch(Exception x)
                    {
                    }
                    contorVector = 0;
                }
            }
            connection.Close();
            do
            {
                aranjat = true;
                for (int i = 0; i < 3; i++)
                {
                    if (vectorPuncte[i] < vectorPuncte[i + 1])
                    {
                        aux = vectorPuncte[i];
                        vectorPuncte[i] = vectorPuncte[i + 1];
                        vectorPuncte[i + 1] = aux;
                        aranjat = false;
                    }
                }
            } while (!aranjat);
            if (puncte >= 20)
            {
                MessageBox.Show("Clasament :" + Environment.NewLine + "Locul 1 : " + vectorPuncte[contorVector++] + "" + Environment.NewLine + "Locul 2 : " + vectorPuncte[contorVector++] + "" + Environment.NewLine + "Locul 3 : " + vectorPuncte[contorVector++] + "" + Environment.NewLine + "Locul 4 : " + vectorPuncte[contorVector] + "" + Environment.NewLine + "Felicitari, acest meci a fost pus ca si castig !");
                aranjat = true;
            }
            else
            {
                MessageBox.Show("Clasament :" + Environment.NewLine + "Locul 1 : " + vectorPuncte[contorVector++] + "" + Environment.NewLine + "Locul 2 : " + vectorPuncte[contorVector++] + "" + Environment.NewLine + "Locul 3 : " + vectorPuncte[contorVector++] + "" + Environment.NewLine + "Locul 4 : " + vectorPuncte[contorVector] + "" + Environment.NewLine + "Din pacate, acest meci a fost incadrat ca si pierdere !");
                aranjat = false;
            }
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "select Castigate, Pierdute from Jucatori where Username = '" + Login.usr + "'";
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    cast = Convert.ToInt16(dr[0]);
                    pierd = Convert.ToInt16(dr[1]);
                }
            }
            cast++;
            pierd++;
            connection.Close();
            connection.Open();
            cmd = connection.CreateCommand();
            if (aranjat)
                cmd.CommandText = "update Jucatori set Castigate = '" + cast + "' where Username = '" + Login.usr + "'";
            else
                cmd.CommandText = "update Jucatori set Pierdute = '" + pierd + "' where Username = '" + Login.usr + "'";
            cmd.ExecuteNonQuery();
            connection.Close();
            if (invite.host)
            {
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "TRUNCATE Sesiuni";
                cmd.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "DROP TABLE " + Login.usr + "joc";
                cmd.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "DROP TABLE " + Login.usr + "intrebari";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            
        }
        public bool aranjat;
        public int cast, pierd;
        public int aux;
        public void gasesteLoc(string charlie, int numeBut)
        {
            this.Controls["button" + numeBut.ToString()].Text = charlie;
        }

        public int[] vectorPuncte = new int[4];
        public int contorVector = 0;
        public string corect;
        public static int puncte = 10;
        public int pozz4;
        public int pierderi = 0;
        public void coloreaza(int numeBut)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            if (!invite.host)
                cmd.CommandText = "select corect from " + invite.numeIntrebari + " where ID = " + index + "";
            else
                cmd.CommandText = "select corect from " + Login.usr + "intrebari where ID = " + index + "";
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    corect = dr[0].ToString();
                }
            }
            connection.Close();
            connection.Open();
            cmd = connection.CreateCommand();
            if (!invite.host)
                cmd.CommandText = "select poz4 from " + invite.numeIntrebari + " where ID = " + index + "";
            else
                cmd.CommandText = "select poz4 from " + Login.usr + "intrebari where ID = " + index + "";
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    pozz4 = Convert.ToInt16(dr[0]);
                }
            }
            connection.Close();
            if (apasat == pozz4)
                puncte += 11;
            else
            {
                puncte--;
                if (puncte < 0)
                {
                    pierderi++;
                    if(pierderi == 1)
                    {
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = "select Castigate, Pierdute from Jucatori where Username = '" + Login.usr + "'";
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                cast = Convert.ToInt16(dr[0]);
                                pierd = Convert.ToInt16(dr[1]);
                            }
                        }
                        cast++;
                        pierd++;
                        connection.Close();
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = "update Jucatori set Pierdute = '" + pierd + "' where Username = '" + Login.usr + "'";
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        this.Hide();
                        timer2.Stop();
                        timer3.Stop();
                        timer4.Stop();
                        MessageBox.Show("Punctele tale au ajuns negative !! Ai pierdut !");
                    }
                    
                }

            }
            pozz4 = 0;
            apasat = 0;
            try
            {
                if (this.Controls["button" + numeBut.ToString()].Text == corect)
                {
                    this.Controls["button" + numeBut.ToString()].BackColor = Color.Green;
                }
                else
                {
                    this.Controls["button" + numeBut.ToString()].BackColor = Color.Red;
                }
            }
            catch(Exception x)
            {
                 
            }
        }
    }
}
