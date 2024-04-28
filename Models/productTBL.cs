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

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool Availability { get; set; }

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
                p.Availability = p.Quantity > 0;
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
                    product.Name = reader["Name"].ToString();
                    product.Category = reader["ProductCategory"].ToString();
                    product.Description = reader["Description"].ToString();
                    product.Price = Convert.ToDecimal(reader["Price"]);
                    product.Quantity = Convert.ToInt32(reader["Quantity"]);
                    product.Availability = Convert.ToBoolean(reader["Availability"]);
                    products.Add(product);
                }
            }
            return products;
        }
    }
}