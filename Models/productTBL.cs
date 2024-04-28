using System.Data.SqlClient;

namespace ST10256859_CLDV6211_POE.Models
{
    public class productTBL
    {
        public static string con_string = "Server=tcp:st10256859-server.database.windows.net,1433;Initial Catalog=st10256859-db;Persist Security Info=False;User ID=McCLUCKY;Password=Ruthenavelu2004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection con = new SqlConnection(con_string);

        public int ProductID { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string Quantity { get; set; }

        public string Availability { get; set; }

        public int InsertProduct(productTBL p)
        {
            try
            {
                string sql = "INSERT INTO ProductTBL (Name, ProductCategory, Description, Price, Quantity, Availability) VALUES (@Name, @Category, @Description, @Price, @Quantity, @Availability)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Category", p.Category);
                cmd.Parameters.AddWithValue("@Description", p.Description);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
                cmd.Parameters.AddWithValue("@Availability", p.Availability);
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

        public static List<productTBL> GetAllProducts()
        {
            List<productTBL> products = new List<productTBL>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT * FROM ProductTBL";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productTBL product = new productTBL();
                    product.ProductID = Convert.ToInt32(reader["productID"]);
                    product.Name = reader["productName"].ToString();
                    product.Category = reader["productCategory"].ToString();
                    product.Description = reader["productDescription"].ToString();
                    product.Price = reader["productPrice"].ToString();
                    product.Quantity = reader["productQuantity"].ToString();
                    product.Availability = reader["productAvailability"].ToString();
                    products.Add(product);
                }
            }
            return products;
        }
    }
}