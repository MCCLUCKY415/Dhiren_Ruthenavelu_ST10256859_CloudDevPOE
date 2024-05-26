using System.Data.SqlClient;

namespace ST10256859_CLDV6211_POE.Models
{
    public class transactionTBL
    {
        public int UserID { get; set; }

        public int ProductID { get; set; }

        public string? ProductName { get; set; }

        public decimal? Price { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime Date { get; set; }

        public static string con_string = "Server=tcp:st10256859-server.database.windows.net,1433;Initial Catalog=st10256859-db;Persist Security Info=False;User ID=McCLUCKY;Password=Ruthenavelu2004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection con = new SqlConnection(con_string);

        public int InsertOrder(int UserID, int ProductID)
        {
            try
            {
                string sql = "INSERT INTO TransactionTBL (UserID, ProductID, Date)" +
                             "VALUES (@UserID, @ProductID, @Date)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);

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

        public List<transactionTBL> GetOrders(int UserID)
        {
            List<transactionTBL> orders = new List<transactionTBL>();
            try
            {
                string sql = "SELECT t.ProductID, t.UserID, t.Date, p.Name AS ProductName, p.ImageUrl, p.Price FROM TransactionTBL t JOIN ProductTBL p ON t.ProductID = p.ProductID WHERE t.UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    transactionTBL order = new transactionTBL();
                    order.ImageUrl = (string)reader["ImageUrl"];
                    order.ProductName = (string)reader["ProductName"];
                    order.ProductID = (int)reader["ProductID"];
                    order.UserID = (int)reader["UserID"];
                    if (reader["Price"] != DBNull.Value)
                    {
                        order.Price = (decimal)reader["Price"];
                    }
                    order.Date = (DateTime)reader["Date"];
                    orders.Add(order);
                }
                con.Close();
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}