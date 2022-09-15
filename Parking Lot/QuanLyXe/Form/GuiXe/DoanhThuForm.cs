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
using Word = Microsoft.Office.Interop.Word;
using System.IO;


namespace Parking_Lot
{
    public partial class DoanhThuForm : Form
    {
        public DoanhThuForm()
        {
            InitializeComponent();
        }
        VEHICLE vehicle = new VEHICLE();
        MY_DB mydb = new MY_DB();
        private void fillgrid(SqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = vehicle.getBike(command);
            dataGridView1.AllowUserToAddRows = false;
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            double tong = 0;
            SqlCommand command = new SqlCommand("SELECT MaXe as'Mã Xe', LoaiXe as'Loại Xe', BienSo as 'Biển Số', NgayGui as 'Ngày Gửi', NgayLay as 'Ngày Lấy', GiaTien as 'Giá Tiền' FROM  HoaDon");
            fillgrid(command);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                tong = tong + double.Parse(dataGridView1.Rows[i].Cells["Giá Tiền"].Value.ToString());
            }
            SumLabel.Text = ("Tong Tien:" + tong.ToString() + "VND");
        }

        private void DoanhThuForm_Load(object sender, EventArgs e)
        {
            //Tính doanh thu
            SqlCommand command = new SqlCommand("SELECT MaXe as'Mã Xe', LoaiXe as'Loại Xe', BienSo as 'Biển Số', NgayGui as 'Ngày Gửi', NgayLay as 'Ngày Lấy', GiaTien as 'Giá Tiền' FROM  HoaDon");
            fillgrid(command);
            double tong = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                tong = tong + double.Parse(dataGridView1.Rows[i].Cells["Giá Tiền"].Value.ToString());
            }
            SumLabel.Text = ("Tong Tien:" + tong.ToString() + "VND");
            //Vẽ biểu đồ
            double total = Convert.ToDouble(totalVehicle());
            double xedap = Convert.ToDouble(totalBycicle());
            double xemay = Convert.ToDouble(totalBike());
            double oto = Convert.ToDouble(totalCar());
            double xedapPercentage = (xedap * (100 / total));
            double xemayPercentage = (xemay * (100 / total));
            double otoPercentage = (oto * (100 / total));
            BicycleLabel.Text = ("Xe Dap:" + (xedapPercentage.ToString("0.00") + "%"));
            BikeLabel.Text = ("Xe May:" + (xemayPercentage.ToString("0.00") + "%"));
            CarLabel.Text = ("O To:" + (otoPercentage.ToString("0.00") + "%" ));
            fillChart();
        }
        public int totalVehicle()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM HoaDon");
            command.Connection = mydb.GetConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table.Rows.Count;
        }
        public int totalBycicle()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM HoaDon WHERE LoaiXe=@XeDap");
            command.Parameters.Add("@XeDap", SqlDbType.NChar).Value = "Xe Dap";
            command.Connection = mydb.GetConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table.Rows.Count;
        }
        public int totalBike()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM HoaDon WHERE LoaiXe=@XeMay");
            command.Parameters.Add("@XeMay", SqlDbType.NChar).Value = "Xe May";
            command.Connection = mydb.GetConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table.Rows.Count;
        }
        public int totalCar()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM HoaDon WHERE LoaiXe=@OTo");
            command.Parameters.Add("@OTo", SqlDbType.NChar).Value = "O To";
            command.Connection = mydb.GetConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table.Rows.Count;
        }
        private void fillChart()
        {
            chart1.Series["LoaiXe"].Points.AddXY("Xe Dap",totalBycicle().ToString());
            chart1.Series["LoaiXe"].Points.AddXY("Xe May",totalBike().ToString());
            chart1.Series["LoaiXe"].Points.AddXY("O To", totalCar().ToString());
            chart1.Titles.Add("Vehicles");
        }

        private void ToDocFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Word Documents (*.docx)|*.docx";
            sfd.FileName = "export.docx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Export_Data_To_Word(dataGridView1, sfd.FileName);
            }
        }
        public void Export_Data_To_Word(DataGridView DGV, string filename)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new Object[RowCount + 1, ColumnCount + 1];
                // add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    }// end row loop
                }//end column loop

                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;
                //Page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";
                    }
                }
                //table format
                oRange.Text = oTemp;

                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                     Type.Missing, Type.Missing, ref ApplyBorders,
                                     Type.Missing, Type.Missing, Type.Missing,
                                     Type.Missing, Type.Missing, Type.Missing,
                                     Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Times New Roman";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;
                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                }
                //table style
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //header text
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "Doanh Thu";
                    headerRange.Font.Size = 16;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }
                //save image
                /*for (r = 0; r <= RowCount - 1; r++)
                {
                    byte[] imgbyte = (byte[])dataGridView1.Rows[r].Cells[7].Value;
                    MemoryStream ms = new MemoryStream(imgbyte);
                    Image finalPic = (Image)(new Bitmap(Image.FromStream(ms), new Size(70, 70)));
                    Clipboard.SetDataObject(finalPic);
                    oDoc.Application.Selection.Tables[1].Cell(r + 2, 8).Range.Paste();
                    oDoc.Application.Selection.Tables[1].Cell(r + 2, 8).Range.InsertParagraph();
                }*/
                
                //save the file
                oDoc.SaveAs(filename);
            }

        }
    }
}
