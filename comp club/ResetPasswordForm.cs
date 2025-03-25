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
    public partial class ResetPasswordForm : Form
    {
        public ResetPasswordForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;

            try
            {
                // поле не пустое
                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Пожалуйста, введите ваш email");
                    return;
                }

                //в поле есть @
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Пожалуйста, введите корректный email. ");
                    return;
                }
                // пароль
                string password = GetPasswordByEmail(email);

                if(!string.IsNullOrEmpty(password))
                {
                    MessageBox.Show($"Ваш пароль: {password}", "Пароль восстановлен");
                }
                else
                {
                    MessageBox.Show("Пользователь с таким email не найден.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка");
            }
        }
        private bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }

        private string GetPasswordByEmail(string email)
        {
            string connectionString = "Server=DESKTOP-MNFNS5H; Database=compCLUB; User Id=DESKTOP-MNFNS5H\\настюха; Integrated Security=True;Encrypt=False";
            string query = "SELECT password FROM Пользователи WHERE email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    object result = command.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
        }
    }
}
