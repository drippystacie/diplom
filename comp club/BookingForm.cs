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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace comp_club
{
    public partial class BookingForm : Form
    {
        private string _username;
        public BookingForm(string username)
        {
            InitializeComponent();
            _username = username;
            LoadUserReservations();
        }
        private void LoadUserReservations()
        {
            string connectionString = "Server=DESKTOP-MNFNS5H; Database=compCLUB; User Id=DESKTOP-MNFNS5H\\настюха; Integrated Security=True;Encrypt=False";
            string query = @"SELECT Компьютеры.computer_id, Компьютеры.computer_name, Компьютеры.specifications, Компьютеры.status, Компьютеры.number_of_computer
                FROM Компьютеры
                JOIN Заказы ON Компьютеры.computer_id = Заказы.computer_id
                JOIN Пользователи ON Заказы.user_id = Пользователи.Id
                WHERE Пользователи.Username = @Username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Username", _username); // Передаем логин пользователя
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable); // Заполняем DataTable данными

                    // Привязываем DataTable к DataGridView
                    dataGridViewReservations.DataSource = dataTable;

                    // Настройка столбцов (опционально)
                    dataGridViewReservations.AutoGenerateColumns = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUserReservations();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(_username);
            mainForm.Show();
            this.Close();
        }
    }
}
