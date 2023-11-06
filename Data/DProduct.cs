using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entity;
using System.Data;

namespace Data
{
    public class DProduct
    {
        public static string connectionString = "Data Source=localhost,1433;Initial Catalog=master;User ID=sa;Password=masterpassword!!23";
        public List<Product> Get()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ListActiveProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int productIdColumnIndex = reader.GetOrdinal("product_id");
                            int nameColumnIndex = reader.GetOrdinal("name");
                            int priceColumnIndex = reader.GetOrdinal("price");
                            int stockColumnIndex = reader.GetOrdinal("stock");
                            int descriptionColumnIndex = reader.GetOrdinal("description");
                            int activeColumnIndex = reader.GetOrdinal("active");

                            Product product = new Product
                            {
                                ProductId = reader.GetInt32(productIdColumnIndex),
                                Name = reader.IsDBNull(nameColumnIndex) ? null : reader.GetString(nameColumnIndex),
                                Price = reader.GetDecimal(priceColumnIndex),
                                Stock = reader.GetInt32(stockColumnIndex),
                                Description = reader.IsDBNull(descriptionColumnIndex) ? null : reader.GetString(descriptionColumnIndex),
                                Active = reader.GetBoolean(activeColumnIndex)
                            };
                            products.Add(product);
                        }
                    }

                }
                connection.Close();
            }
            return products;
        }

        public bool Add(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("insertProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@name", product.Name));
                    command.Parameters.Add(new SqlParameter("@description", product.Description));
                    command.Parameters.Add(new SqlParameter("@stock", product.Stock));
                    command.Parameters.Add(new SqlParameter("@price", product.Price));
                    command.Parameters.Add(new SqlParameter("@active", 1));

                    int affectedRows = command.ExecuteNonQuery();
                    connection.Close();

                    return affectedRows == 1;
                }
            }
        }

        public bool Edit(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@product_id", product.ProductId));
                    command.Parameters.Add(new SqlParameter("@name", product.Name));
                    command.Parameters.Add(new SqlParameter("@description", product.Description));
                    command.Parameters.Add(new SqlParameter("@stock", product.Stock));
                    command.Parameters.Add(new SqlParameter("@price", product.Price));

                    int affectedRows = command.ExecuteNonQuery();
                    connection.Close();

                    return affectedRows == 1;
                }
            }
        }

        public bool Delete(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SoftDeleteProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@productId", productId));

                    int affectedRows = command.ExecuteNonQuery();
                    connection.Close();

                    return affectedRows == 1;
                }
            }
        }


    }
}