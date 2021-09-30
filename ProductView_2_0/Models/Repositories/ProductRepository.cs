using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Product_View_v_2.Models
{
    public class ProductRep
    {
        static string connectionString = "Data Source=SQL5080.site4now.net;Initial Catalog=db_a7920a_regdb;User Id=db_a7920a_regdb_admin;Password=qwerty009";
        public static List<Product> GetProducts()
        {
            List<Product> coll = new List<Product>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    var sql = "EXEC [dbo].[GetProducts]";
                    coll = db.Query<Product>(sql).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return coll;
        }


        public static void AddProducts(Product value)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var sql = "EXEC [dbo].[AddProducts] @category, @salesman, @product, @model, @description ,@price,@img";
                        var values = new
                        {
                            category = value.category,
                            salesman = value.salesman,
                            product = value.product,
                            model = value.model,
                            description = value.description,
                            price = value.price,
                            img = value.img
                        };
                        db.Query(sql, values, transaction);
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static void UpdateProducts(Product value)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var sql = "EXEC [dbo].[UpdateProducts] @id, @category, @salesman, @product, @model, @description ,@price, @img";
                        var values = new
                        {
                            id = value.id,
                            category = value.category,
                            salesman = value.salesman,
                            product = value.product,
                            model = value.model,
                            description = value.description,
                            price = value.price,
                            img = value.img
                        };
                        db.Query(sql, values, transaction);
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var sql = "EXEC [dbo].[DellProducts] @id";
                        var values = new { id = id };
                        db.Query(sql, values, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
