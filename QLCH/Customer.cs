using QLCH;
using System.Data.SqlClient;
using System.Data;
using System;

internal class Customer
{
    MY_DB db = new MY_DB();

    public string Ho { get; set; }
    public string Ten { get; set; }
    public string GioiTinh { get; set; }
    public DateTime NgaySinh { get; set; }
    public string QueQuan { get; set; }
    public string CCCD { get; set; }
    public string SDT { get; set; }

    public bool insertCustomer(string lname, string fname, string gender, DateTime bdate, string htown, string id, string phone)
    {
        try
        {
            using (SqlConnection connection = MY_DB.OpenConnection())
            {
                SqlCommand command = new SqlCommand("INSERT INTO NGUOITHUE (Ho, Ten, GioiTinh, NgaySinh, QueQuan, CCCD, SDT) " +
                                                 "VALUES (@lname, @fname, @gender, @bdate, @htown, @id, @phone)", connection);

                command.Parameters.Add("@lname", SqlDbType.NVarChar).Value = lname;
                command.Parameters.Add("@fname", SqlDbType.NVarChar).Value = fname;
                command.Parameters.Add("@gender", SqlDbType.NVarChar).Value = gender;
                command.Parameters.Add("@bdate", SqlDbType.Date).Value = bdate;
                command.Parameters.Add("@htown", SqlDbType.NVarChar).Value = htown;
                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phone;

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    // Cập nhật thành công
                    return true;
                }
                else
                {
                    // Không có hàng nào được cập nhật
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
