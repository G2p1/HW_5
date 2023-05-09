using System;
using System.Data;
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
            //var orders = SelectAllOrders(2023);

            var orders = SelectAllOrdersDataSet();
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

        static List<Order> SelectAllOrdersDataSet()
        {
            var connection = new SqlConnection(CONNECTION_STRIGN);
            string select = "select * from Orders where YEAR(ord_datetime)=2023";

            connection.Open();


            SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(select, connection);
            DataSet ds = new DataSet();
            sqlDataAdapter2.Fill(ds);

            List<Order> orders = new List<Order>();
            foreach (DataTable thisTable in ds.Tables)
            {
                foreach (DataRow row in thisTable.Rows)
                {

                    orders.Add(new Order()
                    {
                        orderId = (int)row[0],
                        dateTime = (DateTime)row[1],
                        analysisId = (int)row[2]
                    }) ;

                }
            }

            connection.Close();
            return orders;
        }

    }
}
