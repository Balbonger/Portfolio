using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NibsInventoryManagementSystem
{
    public partial class ActivityLog : UserControl
    {
        public ActivityLog()
        {
            InitializeComponent();
            displayActivityLog();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
        }

        public void displayActivityLog()
        {
            ChangelogData uData = new ChangelogData();

            List<ChangelogData> listData = uData.AllChangelogData();

            dataGridView1.DataSource = listData;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void exportExcel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to export the table to xslx file?"
                , "Export Products Table", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {


                if (dataGridView1.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Excel (.xlsx)|  *.xlsx";
                    sfd.FileName = "Nibs_Changelog.xlsx";
                    bool fileError = false;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(sfd.FileName))
                        {
                            try
                            {
                                File.Delete(sfd.FileName);
                            }
                            catch (IOException ex)
                            {
                                fileError = true;
                                MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                            }
                        }
                        if (!fileError)
                        {
                            try
                            {
                                Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                                Microsoft.Office.Interop.Excel._Workbook workbook = XcelApp.Workbooks.Add(Type.Missing);
                                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;



                                worksheet = workbook.Sheets["Sheet1"];
                                worksheet = workbook.ActiveSheet;
                                worksheet.Name = "NibsChangeLog";
                                worksheet.Application.ActiveWindow.SplitRow = 1;
                                worksheet.Application.ActiveWindow.FreezePanes = true;

                                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                                {
                                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                                    worksheet.Cells[1, i].Font.NAME = "Calibri";
                                    worksheet.Cells[1, i].Font.Bold = true;
                                    worksheet.Cells[1, i].Font.Color = Color.White;
                                    worksheet.Cells[1, i].Interior.Color = Form1.appC;
                                    worksheet.Cells[1, i].Font.Size = 12;
                                }

                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                    {
                                        worksheet.Cells[i + 2, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
                                    }
                                }

                                worksheet.Columns.AutoFit();
                                workbook.SaveAs(sfd.FileName);
                                XcelApp.Quit();

                                ReleaseObject(worksheet);
                                ReleaseObject(workbook);
                                ReleaseObject(XcelApp);

                                MessageBox.Show("Data Exported Successfully !!!", "Info");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error :" + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Record To Export !!!", "Info");
                }
            }
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.Message, "Error");
            }
            finally
            {
                GC.Collect();
            }
        }

        private void ActivityLog_Load(object sender, EventArgs e)
        {
            colorManagement();
        }

        private void colorManagement()
        {
            dataGridView1.RowsDefaultCellStyle.BackColor = Form1.appC;
        }
    }
}
