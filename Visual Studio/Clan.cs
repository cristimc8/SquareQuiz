using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM
{
    public partial class Clan : Form
    {
        public Clan()
        {
            InitializeComponent();
        }

        private void create_Click(object sender, EventArgs e)
        {
            creazaClan frm = new creazaClan();
            frm.Show();
        }

        private void Clan_Load(object sender, EventArgs e)
        {

        }

    }
}
