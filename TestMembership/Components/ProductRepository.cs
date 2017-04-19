using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TestMembership.Models;

namespace TestMembership.Components
{
    public class ProductRepository
    {
        public static IEnumerable<Products> GetProducts()
        {
            using (var sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["primary"].ConnectionString))
            {
                sqlConnection.Open();

                //begin stored proc
                //make this a stored procedure instead
                var products =
                    sqlConnection.Query<Products>("spGetProducts", CommandType.StoredProcedure);

                sqlConnection.Close();
                return products;
                
            }
        }

        public static Products GetProduct(int id)
        {
            using (var sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["primary"].ConnectionString))
            {
                sqlConnection.Open();

                var product =
                    sqlConnection.Query<Products>("SELECT * FROM Products " +
                        "WHERE id = @id", new { id }).SingleOrDefault();

                sqlConnection.Close();
                return product;

            }
        }

        public static async Task<Products> AddProduct(Products p)
        {
            using (var sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["primary"].ConnectionString))
            {
                sqlConnection.Open();

                var product = (await sqlConnection.QueryAsync<Products>("spInsert", new { p.Name, p.Price }, commandType: CommandType.StoredProcedure)).Single();

                sqlConnection.Close();
                return product;

            }
        }

        //WORKING ON UPDATE
        public static async Task<Products> ChangePrice(int id, decimal Price)
        {
            using (var sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["primary"].ConnectionString))
            {
                sqlConnection.Open();

                var product = (await sqlConnection.QueryAsync<Products>("spUpdate", new { id, Price }, commandType: CommandType.StoredProcedure)).Single();

                sqlConnection.Close();
                return product;
            }
        }

        public static List<Products> AddAll(List<Tuple<string, decimal>> items)
        {
            using (var sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["primary"].ConnectionString))
            {
                sqlConnection.Open();

                string sql = "INSERT INTO Products (name, price) VALUES (@name, @price);" +
                    "SELECT CAST (SCOPE_IDENTITY() as int)";

                foreach (var pair in items) 
                {
                    sqlConnection.Query<int>(sql, new { pair.Item1, pair.Item2 });
                }

                //int id = sqlConnection.Query<int>(sql, new { name, price }).Single();

                /*var product = sqlConnection.Query<Products>("INSERT INTO Products (name, price)" +
                        "VALUES (@name, @price)", new { name, price });*/
                //var product = getProduct(id);

                sqlConnection.Close();
                return GetProducts().ToList();

            }
        }

        public static bool RemoveProduct(int id)
        {
            using (var sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["primary"].ConnectionString))
            {
                sqlConnection.Open();

                int deleted = sqlConnection.Query<int>("SELECT COUNT (*) FROM Products " +
                        "WHERE id = @id", new { id }).Single();

                var product =
                    sqlConnection.Query<Products>("DELETE FROM Products " +
                        "WHERE id = @id", new { id }).SingleOrDefault();

                

                sqlConnection.Close();
                return deleted == 1;

            }
        }
    }
}