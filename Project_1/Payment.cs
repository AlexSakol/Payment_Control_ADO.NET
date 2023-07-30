// КЛАСС "Платеж"
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Project_1
{
    public class Payment
    {
        static SqlConnection connection;
        static Payment()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
        public int PaymentId { get; set; }
        public string PaymentName { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Price { get; set; }

        public static IEnumerable<Payment> GetPayments()
        {
            if (connection.State != System.Data.ConnectionState.Open) connection.Open();
            string SQLresponse = "SELECT * FROM Payment";
            SqlCommand command = new SqlCommand(SQLresponse, connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var date = reader.GetDateTime(2);
                    var price = reader.GetDecimal(3);
                    yield return new Payment
                    {
                        PaymentId = id,
                        PaymentName = name,
                        PaymentDate = date,
                        Price = price
                    };
                }
            }
            connection.Close();
        }

        public void Insert()
        {
            if (connection.State != System.Data.ConnectionState.Open) connection.Open();
            string SQLresponse = "INSERT INTO Payment (PaymentName, PaymentDate, Price) VALUES (@name, @date, @price)";
            SqlCommand command = new SqlCommand(SQLresponse, connection);
            command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("name", PaymentName),
                new SqlParameter("date", PaymentDate),
                new SqlParameter("price", Price)
            });
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update()
        {
            if (connection.State != System.Data.ConnectionState.Open) connection.Open();
            string SQLresponse = "UPDATE Payment SET PaymentName=@name, PaymentDate=@date, Price=@price WHERE(PaymentId=@id)";
            SqlCommand command = new SqlCommand(SQLresponse, connection);
            command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("id", PaymentId),
                new SqlParameter("name", PaymentName),
                new SqlParameter("date", PaymentDate),
                new SqlParameter("price", Price)
            });
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void Delete(int id)
        {
            if (connection.State != System.Data.ConnectionState.Open) connection.Open();
            string SQLresponse = "DELETE FROM Payment WHERE(PaymentId=@id)";
            SqlCommand command = new SqlCommand(SQLresponse, connection);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static Payment Find(string name)
        {
            foreach (var payment in GetPayments())
            {
                if (payment.PaymentName == name) return payment;
            }
            return null;
        }

        public override string? ToString() => $" № {PaymentId} Имя {PaymentName} Дата {PaymentDate.ToString("d")} Сумма {Price}";
    
    }
            
}
