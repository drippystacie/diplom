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
    public partial class ComputersForm : Form
    {
        private string _username;
        public ComputersForm(string username)
        {
            InitializeComponent();
            _username = username;
            LoadComputers();
        }
        private void LoadComputers()
        {
            string connectionString = "Server=DESKTOP-MNFNS5H; Database=compCLUB; User Id=DESKTOP-MNFNS5H\\настюха; Integrated Security=True;Encrypt=False";
            string query = "SELECT computer_id, computer_name, specifications, status, number_of_computer FROM Компьютеры";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridViewComputers.DataSource = dataTable;
                    dataGridViewComputers.AutoGenerateColumns = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = _username;
            MainForm mainForm = new MainForm(username);
            mainForm.Show();
            this.Close();
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            if (dataGridViewComputers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите компьютер для бронирования. ");
                return;
            }
            DataGridViewRow selectedRow = dataGridViewComputers.SelectedRows[0];
            int computerId = Convert.ToInt32(selectedRow.Cells["computer_id"].Value);
            string status = selectedRow.Cells["status"].Value.ToString();
            if (status != "Доступен")
            {
                MessageBox.Show("Этот компьютер недоступен для бронирования.");
                return;
            }

            // Бронируем компьютер
            if (ReserveComputer(computerId))
            {
                MessageBox.Show("Компьютер успешно забронирован.");
                LoadComputers(); // Обновляем список компьютеров
            }
            else
            {
                MessageBox.Show("Ошибка при бронировании компьютера.");
            }
        }
        private bool ReserveComputer(int computerId)
        {
            string connectionString = "Server=DESKTOP-MNFNS5H; Database=compCLUB; User Id=DESKTOP-MNFNS5H\\настюха; Integrated Security=True;Encrypt=False";
            string query = "UPDATE Компьютеры SET status = 'Занят' WHERE computer_id = @ComputerId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ComputerId", computerId);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при бронировании компьютера: " + ex.Message);
                    return false;
                }
            }
        }
    }
}


