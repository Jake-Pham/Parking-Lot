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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        MY_DB mydb = new MY_DB();
        public bool insertAccount(string id, string firstname, string lastname, string username, string password, MemoryStream pic)
        {
            SqlCommand command = new SqlCommand("INSERT INTO ELog_in(Id, FirstName, LastName, UserName, Password, Picture)" + "VALUES (@Id, @f_name, @l_name, @uname, @pwd, @fig)", mydb.GetConnection);
            command.Parameters.Add("@Id", SqlDbType.NChar).Value = id;
            command.Parameters.Add("f_name", SqlDbType.VarChar).Value = firstname;
            command.Parameters.Add("l_name", SqlDbType.VarChar).Value = lastname;
            command.Parameters.Add("@uname", SqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pwd", SqlDbType.VarChar).Value = password;
            command.Parameters.Add("@fig", SqlDbType.Image).Value = pic.ToArray();
            mydb.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            string id = IDTextBox.Text;
            string fname = FirstNameTextBox.Text;
            string lname = LastNameTextBox.Text;
            string uname = UserNameTextBox.Text;
            string pwd = PasswordTextBox.Text;
            MemoryStream pic = new MemoryStream();
            SqlCommand comm = new SqlCommand("Select * from ELog_in where Id =@id", mydb.GetConnection);
            comm.Parameters.Add("@id", SqlDbType.NChar).Value = id;
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("This User Already Exsist", "Add new user", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif())
            {

                pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);
                if (insertAccount(id, fname, lname, uname, pwd, pic))
                {
                    MessageBox.Show("New user added", "Add new user", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Add user", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Fields", "Add new user", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool verif()
        {
            if ((IDTextBox.Text.Trim() == "") || (FirstNameTextBox.Text.Trim() == "") || (LastNameLabel.Text.Trim() == "") || (UserNameTextBox.Text.Trim() == "") || (PasswordTextBox.Text.Trim() == ""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void UploadPicButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg; *.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void HaveAnAccountLabel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
