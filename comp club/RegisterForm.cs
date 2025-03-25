using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace comp_club
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtPassword2.Text;

            //поля пустые
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Пожалуйста, заполните все поля. ");
                return;
            }

            // пароль и подтверждение совпадает
            if (password != confirmPassword)

            {
                MessageBox.Show("Пароль и подтверждение пароля не совпадают. ");
                return;

            }
            // пароль соответствует требованиям
            if (!IsPasswordValid(password))
            {
                MessageBox.Show("Пароль должен содержать:\n" +
                                "- Не менее 8 символов и не более 50\n" +
                                "- Хотя бы одну цифру\n" +
                                "- Хотя бы одну заглавную букву\n" +
                                "- Хотя бы одну строчную букву\n" +
                                "- Хотя бы один специальный символ");
                return;
            }
            //такой логин уже существ
            if (IsUsernameTaken(username))
            {
                MessageBox.Show("Пользователь с таким логином уже существует.");
                return;
            }
            if (RegisterUser(username, email, password))
            {
                MessageBox.Show("Регистрация прошла успешно!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Произошла ошибка при регистрации.");
            }
        }
        //корректность пароля

        private bool IsPasswordValid(string password)
        {
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,50}$";
            return Regex.IsMatch(password, passwordPattern);
        }
        //занят ли логин
        private bool IsUsernameTaken(string username)
        {
            string connectionString = "Server=DESKTOP-MNFNS5H; Database=compCLUB; User Id=DESKTOP-MNFNS5H\\настюха; Integrated Security=True;Encrypt=False";
            string query = "SELECT COUNT(*) FROM Пользователи WHERE username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        // рега пользователя
        private bool RegisterUser(string username, string email, string password)
        {
            string connectionString = "Server=DESKTOP-MNFNS5H; Database=compCLUB; User Id=DESKTOP-MNFNS5H\\настюха; Integrated Security=True;Encrypt=False";
            string query = "INSERT INTO Users (Username, Email, Password, RegistrationDate, Balance) " +
                           "VALUES (@Username, @Email, @Password, @RegistrationDate, @Balance)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
                    command.Parameters.AddWithValue("@Balance", 0);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }

            
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}

