using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace comp_club
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetPasswordForm resetPasswordForm = new ResetPasswordForm();
            resetPasswordForm.Show();
            this.Hide();
        }

        public static class UserSession
        {
            public static string Username { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //поля заполнены
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            //от 8 до 50 символов
            if(password.Length < 8 || password.Length > 50)
            {
                MessageBox.Show("Пароль должен содержать от 8 до 50 символов.");
                return;
            }


            //пользователь существует и пароль верный
            if(AuthenticateUser(username, password))
            {
                

                MessageBox.Show("Вход выполнен успешно!");
                UserSession.Username = username;
                MainForm mainForm = new MainForm(username);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль.");
            }

            
        }
        private bool AuthenticateUser(string username, string password)
        {
            string connectionString = "Server=DESKTOP-MNFNS5H; Database=compCLUB; User Id=DESKTOP-MNFNS5H\\настюха; Integrated Security=True;Encrypt=False"; // Укажите вашу строку подключения
            string query = "SELECT COUNT(*) FROM Пользователи WHERE username = @Username AND password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password); // В реальном приложении пароль должен быть хэширован

                    int count = (int)command.ExecuteScalar();
                    return count > 0; 
                }
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        
    }
}
