using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLCH
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        private void Login_Form_Load(object sender, EventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = MY_DB.GetConnection())
            {
                string query = @"SELECT NV.MaCV 
                        FROM TAIKHOANNHANVIEN TK 
                        INNER JOIN NHANVIEN NV ON TK.MaNV = NV.MaNV 
                        WHERE TK.TenTaiKhoan = @username AND TK.MatKhau = @password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int role = reader.GetInt32(0); // Lấy MaCV của người dùng
                        switch (role)
                        {
                            case 1:
                                MessageBox.Show("Đăng nhập thành công với vai trò Chủ tòa nhà!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Thực hiện hành động tương ứng với vai trò này
                                break;
                            case 2:
                                MessageBox.Show("Đăng nhập thành công với vai trò Thư ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Thực hiện hành động tương ứng với vai trò này
                                break;
                            // Xử lý các vai trò khác ở đây
                            default:
                                MessageBox.Show("Vai trò người dùng không xác định.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
