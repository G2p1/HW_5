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

            foreach (var order in orders)
            {
                Console.WriteLine("ID: " + order.ord_id + "\t|Dat Time: " + order.ord_datetime + "\t|Analysis ID:" + order.ord_an);

            }


            context.Update(
                new Order()
                {
                    ord_id = 2,
                    ord_datetime = new DateTime(07, 8, 8),
                    ord_an = 2
                }
                );
            context.SaveChanges();

            context.Remove(
                new Order()
                {
                    ord_id = 8
                }
                );

            context.SaveChanges();


        }

    }
}
