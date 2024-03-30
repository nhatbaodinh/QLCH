using System;
using System.Data;
using System.Windows.Forms;

namespace QLCH
{
    public partial class PrintCusForm : Form
    {
        private Customer customerManager;

        public PrintCusForm()
        {
            InitializeComponent();
            customerManager = new Customer();
        }

        private void buttonApplyFilter_Click(object sender, EventArgs e)
        {
            string gender = null;
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MaxValue;
            bool filterByGender = true;
            bool filterByDate = true;

            if (radioButtonMale.Checked)
            {
                gender = "Nam";
            }
            else if (radioButtonFemale.Checked)
            {
                gender = "Nữ";
            }
            else
            {
                filterByGender = false;
            }

            if (radioButtonUseDate.Checked)
            {
                fromDate = dateTimePickerFrom.Value.Date;
                toDate = dateTimePickerTo.Value.Date.AddDays(1).AddSeconds(-1);
            }
            else
            {
                filterByDate = false;
            }

            DataTable dtCustomers = customerManager.GetFilteredCustomers(gender, fromDate, toDate, filterByGender, filterByDate);

            dataGridViewCustomers.DataSource = dtCustomers;
        }
    }
}
