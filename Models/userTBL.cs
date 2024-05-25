using System.Data.SqlClient;

namespace ST10256859_CLDV6211_POE.Models
{
    public class userTBL
    {
        public static string con_string = "Server=tcp:st10256859-server.database.windows.net,1433;Initial Catalog=st10256859-db;Persist Security Info=False;User ID=McCLUCKY;Password=Ruthenavelu2004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection con = new SqlConnection(con_string);

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int InsertUser(userTBL u)
        {
            try
            {
                string sql = "INSERT INTO UserTBL (FirstName, LastName, Email, UserPassword) VALUES (@Name, @Surname, @Email, @Password)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", u.FirstName);
                cmd.Parameters.AddWithValue("@Surname", u.LastName);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Password", u.Password);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetFullName(int userID)
        {
            SqlConnection testCon = new SqlConnection(con_string);

            try
            {
                string sql = "SELECT FirstName, LastName FROM UserTBL WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, testCon);
                cmd.Parameters.AddWithValue("@UserID", userID);
                testCon.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    FirstName = reader["FirstName"].ToString();
                    LastName = reader["LastName"].ToString();
                }
                testCon.Close();
                return FirstName + " " + LastName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public userTBL UserDetails(int userID)
        {
            SqlConnection testCon = new SqlConnection(con_string);

            try
            {
                string sql = "SELECT FirstName, LastName, Email FROM UserTBL WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, testCon);
                cmd.Parameters.AddWithValue("@UserID", userID);
                testCon.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    FirstName = reader["FirstName"].ToString();
                    LastName = reader["LastName"].ToString();
                    Email = reader["Email"].ToString();
                }
                testCon.Close();
                return this;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}