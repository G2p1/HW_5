using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using DB.EFCore;
using Microsoft.EntityFrameworkCore;

namespace HW_5
{
    class Program
    {
        static string CONNECTION_STRIGN = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        static void Main(string[] args)
        {
            var builder = new DbContextOptionsBuilder<OrderDbContext>();

            builder.UseSqlServer(CONNECTION_STRIGN, s => s.EnableRetryOnFailure(5));

            var context = new OrderDbContext(builder.Options);

            var orders = context.Orders.Where(s => s.ord_datetime.Year == 2023);

            foreach(var order in orders)
            {
                Console.WriteLine("ID: " + order.ord_id + "\t|Dat Time: " + order.ord_datetime + "\t|Analysis ID:" + order.ord_an);

            }

            //context.Add(
            //    new Order()
            //    {
            //        ord_id = 7,
            //        ord_datetime=new DateTime(23, 1 ,1),
            //        ord_an = 2
            //    }
            //    );

            //context.SaveChanges();

            context.Update(
                new Order()
                {
                    ord_id = 2,
                    ord_datetime = new DateTime(07, 8, 8),
                    ord_an=2
                }
                );
            context.SaveChanges();

            context.Remove(
                new Order()
                {
                    ord_id=8
                }
                );

            context.SaveChanges();

            //var ordersC = SelectAllOrders(2023);
            //foreach (Order order in ordersC)
            //{
            //    Console.WriteLine("ID: " + order.orderId + "\t|Dat Time: " + order.dateTime + "\t|Analysis ID:" + order.analysisId);
            //}
            
            //var ordersD = SelectAllOrdersDataSet();
            //foreach (Order order in ordersD)
            //{
            //    Console.WriteLine("ID: " + order.orderId + "\t|Dat Time: " + order.dateTime + "\t|Analysis ID:" + order.analysisId);
            //}

            //Order orderCreate = new Order()
            //{
            //    orderId = 7,
            //    dateTime = new DateTime(2023, 4, 26),
            //    analysisId = 2
            //};

            ////Create(orderCreate);

            //var count = Update(orderCreate);
            //Console.WriteLine(count);

            //var count2 = Delete(orderCreate);
            //Console.WriteLine(count);
        }

        //static List<Order> SelectAllOrders(int currentYear)
        //{
        //    var connection = new SqlConnection(CONNECTION_STRIGN);
        //    var commandCount = connection.CreateCommand();
        //    var commandSelect = connection.CreateCommand();

        //    commandCount.CommandText = "select count(ord_id) from Orders where YEAR(ord_datetime)=@currentYear";
        //    commandCount.Parameters.Add(new SqlParameter("@currentYear", currentYear));

        //    commandSelect.CommandText = "select * from Orders where YEAR(ord_datetime)=@currentYear";
        //    commandSelect.Parameters.Add(new SqlParameter("@currentYear", currentYear));

        //    connection.Open();

            

        //    var reader = commandCount.ExecuteReader();
        //    if (!reader.Read()) return null;
            
        //    int count = reader.GetInt32(0);

        //    reader = commandSelect.ExecuteReader();
        //    if (!reader.Read()) return null;
        //    List<Order> orders = new List<Order>();
        //    for (int i = 0; i < count; i++)
        //    {
        //        orders.Add(new Order()
        //        {
        //            orderId = reader.GetInt32(0),
        //            dateTime = reader.GetDateTime(1),
        //            analysisId = reader.GetInt32(2)
        //        });
        //        reader.Read();
        //    }

        //    connection.Close();

        //    return orders;
        //}
        //static void Create(Order order)
        //{
        //    var connection = new SqlConnection(CONNECTION_STRIGN);
        //    var command = connection.CreateCommand();

        //    command.CommandText = "INSERT INTO Orders(ord_id, ord_datetime, ord_an) values (@id, @datetime, @anid); select SCOPE_IDENTITY();";
        //    command.Parameters.Add(new SqlParameter("@id", order.orderId));
        //    command.Parameters.Add(new SqlParameter("@datetime", order.dateTime));
        //    command.Parameters.Add(new SqlParameter("@anid", order.analysisId));

        //    connection.Open();

        //    command.ExecuteScalar();

        //    connection.Close();
            
        //}

        //static int Update(Order order)
        //{
        //    var connection = new SqlConnection(CONNECTION_STRIGN);
        //    var command = connection.CreateCommand();

        //    command.CommandText = "UPDATE Orders SET ord_id=@id, ord_datetime = @datetime, ord_an=@anid where ord_id=@id;";
        //    command.Parameters.Add(new SqlParameter("@id", order.orderId));
        //    command.Parameters.Add(new SqlParameter("@datetime", order.dateTime));
        //    command.Parameters.Add(new SqlParameter("@anid", order.analysisId));

        //    connection.Open();
        //    var count = command.ExecuteNonQuery();
        //    connection.Close();
        //    return count;

        //}
        //static int Delete(Order order)
        //{
        //    var connection = new SqlConnection(CONNECTION_STRIGN);
        //    var command = connection.CreateCommand();

        //    command.CommandText = "delete from Orders where ord_id=@id;";
        //    command.Parameters.Add(new SqlParameter("@id", order.orderId));

        //    connection.Open();
        //    var count = command.ExecuteNonQuery();
        //    connection.Close();
        //    return count;

        //}

        //static List<Order> SelectAllOrdersDataSet()
        //{
        //    var connection = new SqlConnection(CONNECTION_STRIGN);
        //    string select = "select * from Orders where YEAR(ord_datetime)=2023";

        //    connection.Open();


        //    SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(select, connection);
        //    DataSet ds = new DataSet();
        //    sqlDataAdapter2.Fill(ds);

        //    List<Order> orders = new List<Order>();
        //    foreach (DataTable thisTable in ds.Tables)
        //    {
        //        foreach (DataRow row in thisTable.Rows)
        //        {

        //            orders.Add(new Order()
        //            {
        //                orderId = (int)row[0],
        //                dateTime = (DateTime)row[1],
        //                analysisId = (int)row[2]
        //            }) ;

        //        }
        //    }

        //    connection.Close();
        //    return orders;
        //}

    }
}
