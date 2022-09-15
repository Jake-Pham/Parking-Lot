using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Parking_Lot
{
    class WORK
    {
        MY_DB mydb = new MY_DB();
        public bool addStaff(string Id, DateTime NgayLam, DateTime TimeIn1, DateTime TimeOut1, DateTime TimeIn2, DateTime TimeOut2, int SumHours, double Luong)
        {
            SqlCommand command = new SqlCommand("INSERT INTO NhanVIen (Id, NgayLam, TimeIn1, TimeOut1, TimeIn2, TimeOut2, SumHours, Luong)" + "VALUES(@Id, @NgayLam, @TimeIn1, @TimeOut1, @TimeIn2, @TimeOut2, @Sum, @Luong)", mydb.GetConnection);
            command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = Id;
            command.Parameters.Add("@NgayLam", SqlDbType.DateTime).Value = NgayLam;
            command.Parameters.Add("@TimeIn1", SqlDbType.DateTime).Value = TimeIn1;
            command.Parameters.Add("@TimeOut1", SqlDbType.DateTime).Value = TimeOut1;
            command.Parameters.Add("@TimeIn2", SqlDbType.DateTime).Value = TimeIn2;
            command.Parameters.Add("@TimeOut2", SqlDbType.DateTime).Value = TimeOut2;
            command.Parameters.Add("@Sum", SqlDbType.Int).Value = SumHours;
            command.Parameters.Add("@Luong", SqlDbType.Float).Value = Luong;

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
        public bool updateStaff(string Id, DateTime NgayLam, DateTime TimeIn1, DateTime TimeOut1, DateTime TimeIn2, DateTime TimeOut2, int SumHours, double Luong)
        {
            SqlCommand command = new SqlCommand("UPDATE NhanVIen SET TimeIn1=@TimeIn1, TimeOut1=@TimeOut1, TimeIn2=@TimeIn2, TimeOut2=@TimeOut2, SumHours=@Sum WHERE Id=@Id AND NgayLam=@NgayLam", mydb.GetConnection);
            command.Parameters.Add("@Id", SqlDbType.NChar).Value = Id;
            command.Parameters.Add("@NgayLam", SqlDbType.DateTime).Value = NgayLam;
            command.Parameters.Add("@TimeIn1", SqlDbType.DateTime).Value = TimeIn1;
            command.Parameters.Add("@TimeOut1", SqlDbType.DateTime).Value = TimeOut1;
            command.Parameters.Add("@TimeIn2", SqlDbType.DateTime).Value = TimeIn2;
            command.Parameters.Add("@TimeOut2", SqlDbType.DateTime).Value = TimeOut2;
            command.Parameters.Add("@Sum", SqlDbType.Int).Value = SumHours;
            command.Parameters.Add("@Luong", SqlDbType.Float).Value = Luong;
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
        public DataTable getWork(SqlCommand command)
        {
            command.Connection = mydb.GetConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
