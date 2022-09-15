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

namespace Parking_Lot
{
    public partial class Log_in : Form
    {
        public Log_in()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            MY_DB db = new MY_DB();
            if (QuanLyRadioButton.Checked)
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                SqlCommand command = new SqlCommand("SELECT * FROM Log_in WHERE Username=@User AND Password=@Pass", db.GetConnection);
                command.Parameters.Add("@User", SqlDbType.VarChar).Value = UserTextBox.Text;
                command.Parameters.Add("@Pass", SqlDbType.VarChar).Value = PasswordTextBox.Text;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    QuanLyForm quanly = new QuanLyForm();
                    quanly.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else if (NhanVienRadioButton.Checked)
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                SqlCommand command = new SqlCommand("SELECT * FROM ELog_in WHERE UserName=@User AND Password=@Pass", db.GetConnection);
                command.Parameters.Add("@User", SqlDbType.VarChar).Value = UserTextBox.Text;
                command.Parameters.Add("@Pass", SqlDbType.VarChar).Value = PasswordTextBox.Text;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    NhanVienForm staff = new NhanVienForm();
                    string userid = table.Rows[0][0].ToString();
                    //dùng 1 lớp static Global class, lớp này đung để lấy giá trị id
                    Globals.SetGlobalUserIId(userid);
                    staff.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LogInButton.PerformClick();
            }
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            if (QuanLyRadioButton.Checked)
            {
                //RegisterForm res = new RegisterForm();
                //res.Show();
            }
            if (NhanVienRadioButton.Checked)
            {
                RegisterForm res = new RegisterForm();
                res.Show();
            }
        }
    }
}
