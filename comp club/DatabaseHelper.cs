using Microsoft.Data.SqlClient;
using System.Configuration;

public class DatabaseHelper
{
    private string connectionString;

    public DatabaseHelper()
    {
        connectionString = ConfigurationManager.ConnectionStrings["compCLUB"].ConnectionString;
    }

    // Метод для проверки пользователя (логин)
    public bool AuthenticateUser(string username, string password)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM Пользователи WHERE username = @Username AND password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            int count = (int)command.ExecuteScalar();
            return count > 0;
        }
    }

    // Метод для регистрации нового пользователя
    public bool RegisterUser(string username, string password, string email)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Пользователи (username, password, email) VALUES (@Username, @Password, @Email)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Email", email);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}