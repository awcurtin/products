using PetaPoco;
using System;
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
    public class PocoRepository
    {
        public static IEnumerable<Products> GetProducts()
        {
            
            using (var db =
            new PetaPoco.Database("primary")) //just needs name of connection string
            {
                var products = db.Query<Products>(";EXEC spGetProducts");
               
                return products;

            }
        }

        public static  Products ChangePrice(Products p)
        {
            using (var db =
            new PetaPoco.Database("primary"))
            {
                
                var sql = new Sql().Append("; EXEC dbo.spUpdate @@id = @0", p.Id).
                    Append(", @@price = @0", p.Price);

                var product = db.Query<Products>(sql);

                return product.Single<Products>();
            }
        }

        
        public static IEnumerable<Products> AddAll(List<Products> products)
        {
            using (var db =
            new PetaPoco.Database("primary"))
            {

                DataTable data = new DataTable("data");
                data.Columns.Add("proName", typeof(string));
                data.Columns.Add("proPrice", typeof(decimal));

                foreach (var p in products)
                {
                    data.Rows.Add(p.Name, p.Price);
                }


                SqlParameter param = new SqlParameter();
                param.SqlDbType = SqlDbType.Structured;
                param.ParameterName = "@data";
                param.TypeName = "dbo.ProductsIn";
                param.SqlValue = data;

                var sql = new Sql().Append("; EXEC dbo.spAddAll @@data = @0", param);
                var output = db.Query<Products>(sql);

                return output;
            }
        }
        




    }
}