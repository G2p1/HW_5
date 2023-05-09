using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace HW_5
{
    class Program
    {
        static string CONNECTION_STRIGN = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        static void Main(string[] args)
        {
            var orders = SelectAllOrders(2023);
            foreach (Order order in orders)
            {
                Console.WriteLine("ID: " + order.orderId + "\t|Dat Time: " + order.dateTime + "\t|Analysis ID:" + order.analysisId);
            }

        }

        static List<Order> SelectAllOrders(int currentYear)
        {
            var connection = new SqlConnection(CONNECTION_STRIGN);
            var commandCount = connection.CreateCommand();
            var commandSelect = connection.CreateCommand();

            commandCount.CommandText = "select count(ord_id) from Orders where YEAR(ord_datetime)=@currentYear";
            commandCount.Parameters.Add(new SqlParameter("@currentYear", currentYear));

            commandSelect.CommandText = "select * from Orders where YEAR(ord_datetime)=@currentYear";
            commandSelect.Parameters.Add(new SqlParameter("@currentYear", currentYear));

            connection.Open();

            

            var reader = commandCount.ExecuteReader();
            if (!reader.Read()) return null;
            
            int count = reader.GetInt32(0);

            reader = commandSelect.ExecuteReader();
            if (!reader.Read()) return null;
            List<Order> orders = new List<Order>();
            for (int i = 0; i < count; i++)
            {
                orders.Add(new Order()
                {
                    orderId = reader.GetInt32(0),
                    dateTime = reader.GetDateTime(1),
                    analysisId = reader.GetInt32(2)
                });
                reader.Read();
            }

            connection.Close();

            return orders;
        }

    }
}
