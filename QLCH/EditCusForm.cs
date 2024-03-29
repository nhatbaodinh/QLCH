using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLCH
{
    public partial class EditCusForm : Form
    {
        public EditCusForm()
        {
            InitializeComponent();
        }

        // Phương thức này được sử dụng để hiển thị thông tin của khách hàng trong các ô nhập liệu
        public void DisplayCustomerInfo(int maKH, string ho, string ten, string gioiTinh, DateTime ngaySinh, string queQuan, string cccd, string sdt)
        {
            textBoxMaKH.Text = maKH.ToString();
            textBoxLName.Text = ho;
            textBoxFName.Text = ten;
            if (gioiTinh == "Nam")
                radioButtonMale.Checked = true;
            else if (gioiTinh == "Nữ")
                radioButtonFemale.Checked = true;
            dateTimePicker1.Value = ngaySinh;
            textBoxHTown.Text = queQuan;
            textBoxID.Text = cccd;
            textBoxPhone.Text = sdt;
        }

        // Sự kiện click nút lưu
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Lưu thông tin mới vào cơ sở dữ liệu
            SaveCustomerInfo();
        }

        // Sự kiện click nút tìm
        private void btnFind_Click(object sender, EventArgs e)
        {
            // Lấy thông tin khách hàng theo MaKH từ cơ sở dữ liệu
            FindCustomer();
        }

        // Sự kiện click nút xóa
        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Xóa thông tin khách hàng và các thông tin liên quan
            if (int.TryParse(textBoxMaKH.Text, out int maKH))
            {
                RemoveCustomerAndRelatedInfo(maKH);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập MaKH là một số nguyên dương.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Phương thức này được sử dụng để lưu thông tin khách hàng sau khi chỉnh sửa
        private void SaveCustomerInfo()
        {
            // Lấy thông tin từ các ô nhập liệu
            int maKH = int.Parse(textBoxMaKH.Text);
            string ho = textBoxLName.Text;
            string ten = textBoxFName.Text;
            string gioiTinh = radioButtonMale.Checked ? "Nam" : "Nữ";
            DateTime ngaySinh = dateTimePicker1.Value;
            string queQuan = textBoxHTown.Text;
            string cccd = textBoxID.Text;
            string sdt = textBoxPhone.Text;

            // Tạo chuỗi truy vấn SQL
            string query = "UPDATE NGUOITHUE SET Ho=@ho, Ten=@ten, GioiTinh=@gioiTinh, NgaySinh=@ngaySinh, QueQuan=@queQuan, CCCD=@cccd, SDT=@sdt WHERE MaKH=@maKH";

            // Tạo và mở kết nối
            using (SqlConnection connection = MY_DB.OpenConnection())
            {
                // Tạo đối tượng SqlCommand
                SqlCommand command = new SqlCommand(query, connection);

                // Thêm các tham số vào SqlCommand
                command.Parameters.AddWithValue("@ho", ho);
                command.Parameters.AddWithValue("@ten", ten);
                command.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                command.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                command.Parameters.AddWithValue("@queQuan", queQuan);
                command.Parameters.AddWithValue("@cccd", cccd);
                command.Parameters.AddWithValue("@sdt", sdt);
                command.Parameters.AddWithValue("@maKH", maKH);

                // Thực thi truy vấn
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thông tin khách hàng đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật thông tin khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Đóng form
            this.Close();
        }

        // Phương thức này được sử dụng để tìm thông tin khách hàng theo MaKH
        private void FindCustomer()
        {
            int maKH;
            if (int.TryParse(textBoxMaKH.Text, out maKH))
            {
                // Tạo chuỗi truy vấn SQL
                string query = "SELECT * FROM NGUOITHUE WHERE MaKH=@maKH";

                // Tạo và mở kết nối
                using (SqlConnection connection = MY_DB.OpenConnection())
                {
                    // Tạo đối tượng SqlCommand
                    SqlCommand command = new SqlCommand(query, connection);

                    // Thêm tham số vào SqlCommand
                    command.Parameters.AddWithValue("@maKH", maKH);

                    // Thực thi truy vấn và lấy dữ liệu
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Lấy dữ liệu từ reader và hiển thị lên giao diện
                        DisplayCustomerInfo(
                            reader.GetInt32(reader.GetOrdinal("MaKH")),
                            reader.GetString(reader.GetOrdinal("Ho")),
                            reader.GetString(reader.GetOrdinal("Ten")),
                            reader.GetString(reader.GetOrdinal("GioiTinh")),
                            reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                            reader.GetString(reader.GetOrdinal("QueQuan")),
                            reader.GetString(reader.GetOrdinal("CCCD")),
                            reader.GetString(reader.GetOrdinal("SDT"))
                        );
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập MaKH là một số nguyên dương.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Phương thức này được sử dụng để xóa thông tin khách hàng
        private void RemoveCustomerAndRelatedInfo(int maKH)
        {
            // Tạo và mở kết nối
            using (SqlConnection connection = MY_DB.OpenConnection())
            {
                // Bắt đầu một giao dịch để đảm bảo tính toàn vẹn dữ liệu
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Xóa thông tin khách hàng từ bảng NGUOITHUE
                        string deleteNguoiThueQuery = "DELETE FROM NGUOITHUE WHERE MaKH=@maKH";
                        SqlCommand deleteNguoiThueCommand = new SqlCommand(deleteNguoiThueQuery, connection, transaction);
                        deleteNguoiThueCommand.Parameters.AddWithValue("@maKH", maKH);
                        deleteNguoiThueCommand.ExecuteNonQuery();

                        // Xóa thông tin căn hộ thuộc khách hàng từ bảng CANHO
                        string deleteCanHoQuery = "DELETE FROM CANHO WHERE MaKH=@maKH";
                        SqlCommand deleteCanHoCommand = new SqlCommand(deleteCanHoQuery, connection, transaction);
                        deleteCanHoCommand.Parameters.AddWithValue("@maKH", maKH);
                        deleteCanHoCommand.ExecuteNonQuery();

                        // Commit giao dịch nếu mọi thứ thành công
                        transaction.Commit();
                        MessageBox.Show("Thông tin khách hàng và các thông tin liên quan đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Nếu xảy ra lỗi, rollback giao dịch và hiển thị thông báo lỗi
                        transaction.Rollback();
                        MessageBox.Show("Không thể xóa thông tin khách hàng và các thông tin liên quan!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
