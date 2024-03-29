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
    public partial class AddCusForm : Form
    {
        public AddCusForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Trích xuất thông tin từ các điều khiển nhập liệu
            string lname = textBoxLName.Text;
            string fname = textBoxFName.Text;
            string gender = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked)?.Text; // Giả sử cmbGender là một ComboBox chứa các giá trị giới tính
            DateTime bdate = dateTimePicker1.Value; // Giả sử dtpBirthDate là một DateTimePicker để chọn ngày sinh
            string htown = textBoxHTown.Text;
            string id = textBoxID.Text;
            string phone = textBoxPhone.Text;

            // Gọi phương thức insertCustomer từ lớp Customer hoặc một lớp tương ứng
            Customer customer = new Customer();
            bool result = customer.insertCustomer(lname, fname, gender, bdate, htown, id, phone);

            // Xử lý kết quả từ phương thức insertCustomer
            if (result)
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Xóa dữ liệu trên các điều khiển nhập liệu sau khi thêm thành công
                ClearInputControls();
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputControls()
        {
            // Trích xuất thông tin từ các điều khiển nhập liệu
            string lname = textBoxLName.Text;
            string fname = textBoxFName.Text;
            string gender = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked)?.Text;
            DateTime bdate = dateTimePicker1.Value;
            string htown = textBoxHTown.Text;
            string id = textBoxID.Text;
            string phone = textBoxPhone.Text;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
