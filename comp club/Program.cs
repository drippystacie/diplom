using comp_club;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace comp_club
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());


                string connectionString = "Server=DESKTOP-MNFNS5H;Database=compCLUB;Trusted_Connection=True;";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open(); // Пытаемся открыть соединение
                        Console.WriteLine("Подключение к базе данных успешно установлено!");
                        MessageBox.Show("12567");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Ошибка при регистрации: {ex.Message}\nПожалуйста, попробуйте снова или обратитесь в службу поддержки.", "Ошибка");
                    Console.WriteLine(ex.Message); // Выводим сообщение об ошибке
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла неизвестная ошибка:");
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}