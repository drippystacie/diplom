using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace comp_club
{
    public partial class MainForm : Form
    {
        private string _username;
        public MainForm(string username)
        {
            InitializeComponent();
            _username = username;
            LoadUserBalance();
        }

        private void LoadUserBalance()
        {
            string connectionString = "Server=DESKTOP-MNFNS5H; Database=compCLUB; User Id=DESKTOP-MNFNS5H\\настюха; Integrated Security=True;Encrypt=False"; 
            string query = "SELECT Balance FROM Пользователи WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                       
                        command.Parameters.AddWithValue("@Username", _username);


                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            decimal balance = Convert.ToDecimal(result);
                            label2.Text = $"Баланс: {balance:N2}"; 
                        }
                        else
                        {
                            MessageBox.Show("Не удалось загрузить баланс.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке баланса: " + ex.Message);
                }
            }
        }

        private void входToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void выйтиИзАккаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void регистрацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void просмотрСпискаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GamesForm gamesForm = new GamesForm();
            gamesForm.Show();
            this.Hide();
        }

        private void начатьСессиюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartSessionForm startSessionForm = new StartSessionForm();
            startSessionForm.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void просмотрСпискаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComputersForm computersForm = new ComputersForm(_username);
            computersForm.Show();
            this.Hide();
        }

        private void бронированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BookingForm bookingForm = new BookingForm(_username);
            bookingForm.Show();
            this.Hide();
        }
    }
}
