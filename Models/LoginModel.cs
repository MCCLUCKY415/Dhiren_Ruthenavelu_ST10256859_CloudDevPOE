using System.Data.SqlClient;

namespace ST10256859_CLDV6211_POE.Models
{
    public class LoginModel
    {
        public static string con_string = "Server=tcp:st10256859-server.database.windows.net,1433;Initial Catalog=st10256859-db;Persist Security Info=False;User ID=McCLUCKY;Password=Ruthenavelu2004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int SelectUser(string email, string password)
        {
            int UserId = -1;
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT UserID FROM UserTBL WHERE Email = @Email AND UserPassword = @Password";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        UserId = Convert.ToInt32(result);
                    }
                    else
                    {
                        UserId = -1;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return UserId;
        }
    }
}