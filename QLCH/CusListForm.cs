using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCH
{
    public partial class CusListForm : Form
    {
        public CusListForm()
        {
            InitializeComponent();
        }

        private void CusListForm_Load(object sender, EventArgs e)
        {
            // Gọi phương thức để tải dữ liệu vào DataGridView
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            // Tạo DataTable để lưu dữ liệu
            DataTable dt = new DataTable();

            // Thêm các cột vào DataTable tương ứng với các trường của khách hàng
            dt.Columns.Add("Ho");
            dt.Columns.Add("Ten");
            dt.Columns.Add("GioiTinh");
            dt.Columns.Add("NgaySinh");
            dt.Columns.Add("QueQuan");
            dt.Columns.Add("CCCD");
            dt.Columns.Add("SDT");

            // Tạo đối tượng Customer để truy cập vào phương thức lấy dữ liệu khách hàng
            Customer customer = new Customer();

            // Lấy dữ liệu từ cơ sở dữ liệu và thêm vào DataTable
            foreach (DataRow row in customer.GetCustomers().Rows)
            {
                dt.Rows.Add(row["Ho"], row["Ten"], row["GioiTinh"], row["NgaySinh"], row["QueQuan"], row["CCCD"], row["SDT"]);
            }

            // Gán DataTable làm nguồn dữ liệu cho DataGridView
            dataGridView1.DataSource = dt;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomerData();
        }
    }
}
