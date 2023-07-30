//Класс платеж
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Project_2
{
    public class Payment
    {
        static SqlConnection connection;
        static SqlDataAdapter dataAdapter;
        static DataTable paymentTable = new DataTable();
        static Payment()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
        public static void newConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open) connection.Open();
        }

        public int PaymentId { get; set; }
        public string PaymentName { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Price { get; set; }
        public static DataTable ViewAll()
        {
            newConnection();
            string SQLresponse = "SELECT * FROM Payment";
            dataAdapter = new SqlDataAdapter(SQLresponse, connection);
            dataAdapter.Fill(paymentTable);
            connection.Close();
            return paymentTable;
        }

        public static void Update()
        {
            newConnection();
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Update(paymentTable);
            connection.Close();
        }

        public string Find()
        {
            string result = "";
            foreach(DataRow row in paymentTable.Rows)
            {
                bool flag = true;
                if (PaymentName != null && row[1].ToString() != PaymentName) flag = false;
                if (PaymentDate != null && (DateTime)(row[2]) != PaymentDate) flag = false;
                if (Price != 0 && (decimal)(row[3]) != Price) flag = false;
                if (flag)
                {
                    foreach(object cell in row.ItemArray)
                    {
                        result += $"\t{cell}";
                    }
                    result += "\n";
                }
            }
            return result;
        }

        public override string ToString() => $" № {PaymentId} Имя {PaymentName} Дата {PaymentDate.ToString("d")} Сумма {Price}";

    }
}
