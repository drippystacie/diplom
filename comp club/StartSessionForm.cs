using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comp_club
{
    public partial class StartSessionForm : Form
    {
        private string _username;

        public StartSessionForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = _username;
            MainForm mainForm = new MainForm(username);
            mainForm.Show();
            this.Close();
        }
    }
}
