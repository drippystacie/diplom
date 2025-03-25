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
    public partial class GamesForm : Form
    {
        private string _username;
        public GamesForm()
        {
            InitializeComponent();
            LoadGames();
        }
        private void LoadGames()
        {
            string connectionString = "Server=DESKTOP-MNFNS5H; Database=compCLUB; User Id=DESKTOP-MNFNS5H\\настюха; Integrated Security=True;Encrypt=False";
            string query = "SELECT game_id, game_name, genre, release_date FROM Игры"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable); 

                    
                    dataGridViewGames.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
                }
            }
        }

        public static class UserSession
        {
            public static string Username { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = UserSession.Username;
            MainForm mainForm = new MainForm(username);
            mainForm.Show();
            this.Close();
            
        }
    }
}
