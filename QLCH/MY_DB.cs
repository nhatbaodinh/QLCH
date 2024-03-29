using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QLCH
{
    internal class MY_DB
    {
        // Chuỗi kết nối cơ sở dữ liệu
        public static string connectionString = @"Data Source=PC\SQLEXPRESS;Initial Catalog=QLCH;Integrated Security=True;Encrypt=False";

        // Phương thức để mở kết nối đến cơ sở dữ liệu
        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            return connection;
        }

        // Phương thức để đóng kết nối
        public static void CloseConnection(SqlConnection connection)
        {
            if (connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }

    }

}
