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
using System.IO;

namespace Parking_Lot
{
    public partial class NhanVienForm : Form
    {
        public NhanVienForm()
        {
            InitializeComponent();
        }
        MY_DB mydb = new MY_DB();
        WORK work = new WORK();
        //string Id = Globals.GlobalUserId;
        DateTime Ngaylam = DateTime.Now.Date;
        DateTime timein1 = DateTime.Now;
        DateTime timeout1 = DateTime.Now;
        DateTime timein2= DateTime.Now;
        DateTime timeout2= DateTime.Now;
        int ca1=0;
        int ca2=0;
        int fullca=0;
         float Luong=0;
        
        private void LogOutLabel_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void getImageAndFullName()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            SqlCommand command = new SqlCommand("SELECT * FROM ELog_in WHERE id=@id", mydb.GetConnection);
            command.Parameters.Add("@id", SqlDbType.NChar).Value = Globals.GlobalUserId;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if ((table.Rows.Count > 0))
            {
                byte[] pic = (byte[])table.Rows[0]["picture"];
                MemoryStream picture = new MemoryStream(pic);
                EmployeePictureBox.Image = Image.FromStream(picture);
                EmployeePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                WelcomeLabel.Text = "Welcome " + table.Rows[0]["LastName"].ToString().Trim() + " " + table.Rows[0]["FirstName"].ToString() + "";
            }
        }

        private void NhanVienForm_Load(object sender, EventArgs e)
        {
            getImageAndFullName();
        }

        private void GuiXeButton_Click(object sender, EventArgs e)
        {
            QuanLyXeForm quanly = new QuanLyXeForm();
            quanly.Show();
        }

        private void ChuThueButton_Click(object sender, EventArgs e)
        {
            QuanLyThueXe quanly = new QuanLyThueXe();
            quanly.Show();
        }

        private void CheckInButton_Click(object sender, EventArgs e)
        {
            string Id = Globals.GlobalUserId;
            timein1 = DateTime.Now;
            if (work.addStaff(Id, Ngaylam, timein1, timeout1, timein2, timeout2, fullca, Luong))
            {
                MessageBox.Show("You have checked in successfully! Do your best", "Check In", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error", "Check In", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }    
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            string Id = Globals.GlobalUserId;
            timeout1 = DateTime.Now;
            ca1 = timeout1.Hour - timein1.Hour;
            if (work.updateStaff(Id, Ngaylam, timein1, timeout1, timein2, timeout2, ca1, Luong))
            {
                MessageBox.Show("Your first shift has completed", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error", "Check In", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            string Id = Globals.GlobalUserId;
            timein2 = DateTime.Now;
            if (work.updateStaff(Id,Ngaylam, timein1, timeout1, timein2, timeout2, ca1, Luong))
            {
                MessageBox.Show("You have checked in successfully! Do your best", "Check In", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error", "Check In", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CheckOutButton_Click(object sender, EventArgs e)
        {
            string Id = Globals.GlobalUserId;
            timeout2 = DateTime.Now;
            ca2 = timeout2.Hour - timein2.Hour;
            fullca = ca1 + ca2;
            Luong = 22000 * fullca;
            if (work.updateStaff(Id,Ngaylam, timein1, timeout1, timein2, timeout2, ca1, Luong))
            {
                MessageBox.Show("Your second shift has completed", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error", "Check In", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
