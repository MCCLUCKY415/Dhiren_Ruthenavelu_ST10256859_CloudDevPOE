﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

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

        [Range(0.0, double.MaxValue, ErrorMessage = "Please enter a valid decimal Number")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid integer Number")]
        public int Quantity { get; set; }

        public bool Availability { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile ImageFile { get; set; }

        public int InsertProduct(productTBL p, IWebHostEnvironment webHostEnvironment)
        {
            try
            {
                string sql = "INSERT INTO ProductTBL (Name, ProductCategory, Description, Price, Quantity, Availability, ImageUrl) " +
                             "VALUES (@Name, @Category, @Description, @Price, @Quantity, @Availability, @ImageUrl)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Category", p.Category);
                cmd.Parameters.AddWithValue("@Description", p.Description);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
                p.Availability = p.Quantity > 0;
                cmd.Parameters.AddWithValue("@Availability", p.Availability);
                p.ImageUrl = SaveImageToFile(p.ImageFile, webHostEnvironment); 
                cmd.Parameters.AddWithValue("@ImageUrl", p.ImageUrl); 

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

        public string SaveImageToFile(IFormFile image, IWebHostEnvironment webHostEnvironment)
        {
            string uniqueFileName = null;

            if (image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/productImages");
                var fileExtension = Path.GetExtension(image.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            string FullFilePath = "/images/productImages/" + uniqueFileName;
            return FullFilePath;
        }

        public List<productTBL> GetAllProducts()
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
                    product.ProductID = Convert.ToInt32(reader["ProductID"]);
                    product.Name = reader["Name"].ToString();
                    product.Category = reader["ProductCategory"].ToString();
                    product.Description = reader["Description"].ToString();
                    product.Price = Convert.ToDecimal(reader["Price"]);
                    product.Quantity = Convert.ToInt32(reader["Quantity"]);
                    product.Availability = Convert.ToBoolean(reader["Availability"]);
                    product.ImageUrl = reader["ImageUrl"].ToString();

                    products.Add(product);
                }
            }
            return products;
        }

        public productTBL GetProductById(int productId)
        {
            productTBL product = null;

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT * FROM ProductTBL WHERE ProductID = @ProductId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ProductId", productId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new productTBL();
                    product.ProductID = Convert.ToInt32(reader["ProductID"]);
                    product.Name = reader["Name"].ToString();
                    product.Category = reader["ProductCategory"].ToString();
                    product.Description = reader["Description"].ToString();
                    product.Price = Convert.ToDecimal(reader["Price"]);
                    product.Quantity = Convert.ToInt32(reader["Quantity"]);
                    product.Availability = Convert.ToBoolean(reader["Availability"]);
                    product.ImageUrl = reader["ImageUrl"].ToString();
                }
            }
            return product;
        }

        public void UpdateProduct(productTBL product)
        {
            try
            {
                string sql = "UPDATE ProductTBL SET Quantity = @Quantity, Availability = @Availability WHERE ProductID = @ProductId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@Availability", product.Quantity > 0);
                cmd.Parameters.AddWithValue("@ProductId", product.ProductID);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
