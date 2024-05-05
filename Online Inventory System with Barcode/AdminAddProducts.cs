using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;




namespace NibsInventoryManagementSystem
{
    public partial class AdminAddProducts : UserControl
    {
        MySqlConnection connect = new MySqlConnection(Form1.gateway);

        public AdminAddProducts()
        {
            InitializeComponent();
            displayCategories();
            displayAllProducts();
            displaySupplier();
        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayCategories();
            displayAllProducts();
            displaySupplier();
        }

        public void displayAllProducts()
        {
            AddProductsData apData = new AddProductsData();
            List<AddProductsData> listData = apData.AllProductsData();

            dataGridView1.DataSource = listData;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        public bool emptyFields()
        {
            if (addProduct_prodID.Text == "" || addProduct_prodName.Text == "" || addProduct_category.SelectedIndex == -1
                || addProduct_stock.Text == "" || addProduct_unittxt.Text == "" || addProduct_status.SelectedIndex == -1
                || addProduct_prodLocation.Text == "" || addProduct_personC.Text == "" || addProduct_supplier.SelectedIndex == -1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public void displayCategories()
        {
            if (checkConnection())
            {
                try
                {
                    connect.Open();

                    MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                    direct.ExecuteNonQuery();


                    string selectData = "SELECT * FROM categories";

                    using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();

                        addProduct_category.Items.Clear();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                addProduct_category.Items.Add(reader["category"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displaySupplier()
        {
            if (checkConnection())
            {
                try
                {
                    connect.Open();

                    MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                    direct.ExecuteNonQuery();


                    string selectData = "SELECT * FROM supplier";

                    using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();

                        addProduct_supplier.Items.Clear();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                addProduct_supplier.Items.Add(reader["supplier_name"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void addProduct_addbutton_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Empty Fields. ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (checkConnection())
                {
                    IDGenerator();
                    try
                    {
                        connect.Open();

                        MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                        direct.ExecuteNonQuery();


                        //Check ID
                        string selectData = "SELECT * FROM products WHERE prod_id = @prodID";

                        using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@prodID", addProduct_prodID.Text.Trim());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show("Product ID: " + addProduct_prodID.Text.Trim() + " exists already", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                //Check Name
                                string selectNameData = "SELECT * FROM products WHERE prod_name = @prodName";

                                using (MySqlCommand cmd1 = new MySqlCommand(selectNameData, connect))
                                {
                                    cmd1.Parameters.AddWithValue("@prodName", addProduct_prodName.Text.Trim());

                                    MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
                                    DataTable nameT = new DataTable();

                                    adapter1.Fill(nameT);

                                    if (nameT.Rows.Count > 0)
                                    {
                                        MessageBox.Show("Product Name: " + addProduct_prodName.Text + " exists already", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {

                                        string insertData = "INSERT INTO products " +
                                        "(prod_id, prod_name, category, price, stock, unit, prod_supplier, status, date_insert, prod_location, prod_personnel) " +
                                        "VALUES(@prodID, @prodName, @cat, @price, @stock, @unit, @supp, @status, @date, @loc, @person)";

                                        using (MySqlCommand insertD = new MySqlCommand(insertData, connect))
                                        {
                                            insertD.Parameters.AddWithValue("@prodID", addProduct_prodID.Text.Trim());
                                            insertD.Parameters.AddWithValue("@prodName", addProduct_prodName.Text.Trim());
                                            insertD.Parameters.AddWithValue("@cat", addProduct_category.SelectedItem);
                                            insertD.Parameters.AddWithValue("@price", addProduct_price.Text.Trim());
                                            insertD.Parameters.AddWithValue("@stock", addProduct_stock.Text.Trim());
                                            insertD.Parameters.AddWithValue("@unit", addProduct_unittxt.Text.Trim());
                                            insertD.Parameters.AddWithValue("@supp", addProduct_supplier.SelectedItem);
                                            insertD.Parameters.AddWithValue("@status", addProduct_status.SelectedItem);

                                            DateTime today = DateTime.UtcNow.AddHours(8);

                                            insertD.Parameters.AddWithValue("@date", today);
                                            insertD.Parameters.AddWithValue("@loc", addProduct_prodLocation.Text.Trim());
                                            insertD.Parameters.AddWithValue("@person", addProduct_personC.Text.Trim());

                                            insertD.ExecuteNonQuery();



                                            MessageBox.Show("Added Successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        }

                                        string addDesc = "Product Added: " + addProduct_prodName.Text.Trim();

                                        string insertProduct = "INSERT INTO changelog (log_id, prod_name, destination, person_incharge, log_date, log_user) " +
                                            "VALUES (@tID, @prodName, @dest, @person, @date, @user)";

                                        string username = Form1.username.Substring(0, 1).ToUpper() + Form1.username.Substring(1);

                                        using (MySqlCommand cmdd = new MySqlCommand(insertProduct, connect))
                                        {
                                            cmdd.Parameters.AddWithValue("@tID", idGen);
                                            cmdd.Parameters.AddWithValue("@prodName", addDesc.ToString());
                                            cmdd.Parameters.AddWithValue("@dest", "");
                                            cmdd.Parameters.AddWithValue("@person", "");

                                            DateTime today = DateTime.UtcNow.AddHours(8);
                                            cmdd.Parameters.AddWithValue("@date", today);
                                            cmdd.Parameters.AddWithValue("@user", username);

                                            cmdd.ExecuteNonQuery();

                                        }

                                        clearFields();
                                        displayAllProducts();
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }


        }

        public bool checkConnection()
        {
            if (connect.State != ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void clearFields()
        {
            addProduct_prodID.Text = "";
            addProduct_prodName.Text = "";
            addProduct_category.SelectedIndex = -1;
            addProduct_price.Text = "";
            addProduct_stock.Text = "";
            addProduct_unittxt.Text = "";
            addProduct_status.SelectedIndex = -1;
            addProduct_prodLocation.Text = "";
            addProduct_personC.Text = "";
            addProduct_supplier.SelectedIndex = -1;

        }

        private void addProduct_clearbutton_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void addProduct_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void addProduct_stock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private int getID = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                getID = (int)row.Cells[0].Value;

                addProduct_prodID.Text = row.Cells[1].Value.ToString();
                addProduct_prodName.Text = row.Cells[2].Value.ToString();
                addProduct_category.Text = row.Cells[3].Value.ToString();
                addProduct_price.Text = row.Cells[4].Value.ToString();
                addProduct_stock.Text = row.Cells[5].Value.ToString();
                addProduct_unittxt.Text = row.Cells[6].Value.ToString();
                addProduct_status.Text = row.Cells[7].Value.ToString();

                addProduct_prodLocation.Text = row.Cells[9].Value.ToString();
                addProduct_personC.Text = row.Cells[10].Value.ToString();
                addProduct_supplier.Text = row.Cells[11].Value.ToString();

            }
        }

        private void addProduct_updatebutton_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Empty Fields. ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Form1.role == "Admin")
                {
                    productUpdate();
                }
                else
                {
                    AdminCredentials credentials = new AdminCredentials();
                    credentials.ShowDialog();

                    if (AdminCredentials.isTrue)
                    {
                        productUpdate();
                        AdminCredentials.isTrue = false;
                    }
                }
            }
        }

        public void productUpdate()
        {
            if (MessageBox.Show("Are you sure you want to Update product ID: " + addProduct_prodID.Text.Trim() + 
                "? Every details aside from 'stock' can be updated in this tab.",
                    "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    IDGenerator();
                    try
                    {
                        connect.Open();

                        MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                        direct.ExecuteNonQuery();


                        string checkUpdate = "SELECT * FROM products WHERE prod_id = @prodID AND prod_name = @prodName AND category = @cat " +
                            "AND price = @price " /*AND stock = @stock */ + "AND unit = @unit AND status = @status AND prod_location = @location " +
                            "AND prod_personnel = @person AND prod_supplier = @supp";

                        using (MySqlCommand cmdCU = new MySqlCommand(checkUpdate, connect))
                        {
                            cmdCU.Parameters.AddWithValue("@prodID", addProduct_prodID.Text.Trim());
                            cmdCU.Parameters.AddWithValue("@prodName", addProduct_prodName.Text.Trim());
                            cmdCU.Parameters.AddWithValue("@cat", addProduct_category.SelectedItem);
                            cmdCU.Parameters.AddWithValue("@price", addProduct_price.Text.Trim());
                            //cmdCU.Parameters.AddWithValue("@stock", addProduct_stock.Text.Trim());
                            cmdCU.Parameters.AddWithValue("@unit", addProduct_unittxt.Text.Trim());

                            cmdCU.Parameters.AddWithValue("@status", addProduct_status.SelectedItem);
                            cmdCU.Parameters.AddWithValue("@location", addProduct_prodLocation.Text.Trim());
                            cmdCU.Parameters.AddWithValue("@person", addProduct_personC.Text.Trim());
                            cmdCU.Parameters.AddWithValue("@supp", addProduct_supplier.SelectedItem);

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdCU);
                            DataTable updateT = new DataTable();

                            adapter.Fill(updateT);

                            if (updateT.Rows.Count > 0)
                            {
                                MessageBox.Show("You haven't updated any of the fields. ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string updateData = "UPDATE products SET prod_id = @prodID, prod_name = @prodName" +
                                ", category = @cat, price = @price, " /*stock = @stock,*/  + " unit = @unit, status = @status, prod_location = @location " +
                                ", prod_personnel = @person, prod_supplier = @supp WHERE id = @id";

                                using (MySqlCommand updateD = new MySqlCommand(updateData, connect))
                                {
                                    updateD.Parameters.AddWithValue("@prodID", addProduct_prodID.Text.Trim());
                                    updateD.Parameters.AddWithValue("@prodName", addProduct_prodName.Text.Trim());
                                    updateD.Parameters.AddWithValue("@cat", addProduct_category.SelectedItem);
                                    updateD.Parameters.AddWithValue("@price", addProduct_price.Text.Trim());
                                    //updateD.Parameters.AddWithValue("@stock", addProduct_stock.Text.Trim());
                                    updateD.Parameters.AddWithValue("@unit", addProduct_unittxt.Text.Trim());

                                    updateD.Parameters.AddWithValue("@id", getID);
                                    updateD.Parameters.AddWithValue("@status", addProduct_status.SelectedItem);
                                    updateD.Parameters.AddWithValue("@location", addProduct_prodLocation.Text.Trim());
                                    updateD.Parameters.AddWithValue("@person", addProduct_personC.Text.Trim());
                                    updateD.Parameters.AddWithValue("@supp", addProduct_supplier.SelectedItem);

                                    updateD.ExecuteNonQuery();


                                }

                                string addDesc = "Product Updated: " + addProduct_prodName.Text.Trim();

                                string insertData = "INSERT INTO changelog (log_id, prod_name, destination, person_incharge, log_date, log_user) " +
                                    "VALUES (@tID, @prodName, @dest, @person, @date, @user)";

                                string username = Form1.username.Substring(0, 1).ToUpper() + Form1.username.Substring(1);

                                using (MySqlCommand cmdd = new MySqlCommand(insertData, connect))
                                {
                                    cmdd.Parameters.AddWithValue("@tID", idGen);
                                    cmdd.Parameters.AddWithValue("@prodName", addDesc.ToString());
                                    cmdd.Parameters.AddWithValue("@dest", "");
                                    cmdd.Parameters.AddWithValue("@person", "");

                                    DateTime today = DateTime.UtcNow.AddHours(8);
                                    cmdd.Parameters.AddWithValue("@date", today);
                                    cmdd.Parameters.AddWithValue("@user", username);

                                    cmdd.ExecuteNonQuery();

                                }
                                MessageBox.Show("Updated Successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                

                            }
                            clearFields();
                            displayAllProducts();
                        }

                         

                        

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Update Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        public void productRemove()
        {
            if (MessageBox.Show("Are you sure you want to Delete product ID: " + addProduct_prodID.Text.Trim() + "?",
                        "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    IDGenerator();
                    try
                    {
                        connect.Open();

                        MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                        direct.ExecuteNonQuery();



                        string deleteData = "DELETE FROM products WHERE id = @id";

                        using (MySqlCommand deleteD = new MySqlCommand(deleteData, connect))
                        {

                            deleteD.Parameters.AddWithValue("@id", getID);


                            deleteD.ExecuteNonQuery();




                        }

                        string addDesc = "Product Removed: " + addProduct_prodName.Text.Trim();

                        string insertData = "INSERT INTO changelog (log_id, prod_name, destination, person_incharge, log_date, log_user) " +
                            "VALUES (@tID, @prodName, @dest, @person, @date, @user)";

                        string username = Form1.username.Substring(0, 1).ToUpper() + Form1.username.Substring(1);

                        using (MySqlCommand cmdd = new MySqlCommand(insertData, connect))
                        {
                            cmdd.Parameters.AddWithValue("@tID", idGen);
                            cmdd.Parameters.AddWithValue("@prodName", addDesc.ToString());
                            cmdd.Parameters.AddWithValue("@dest", "");
                            cmdd.Parameters.AddWithValue("@person", "");

                            DateTime today = DateTime.UtcNow.AddHours(8);
                            cmdd.Parameters.AddWithValue("@date", today);
                            cmdd.Parameters.AddWithValue("@user", username);

                            cmdd.ExecuteNonQuery();


                        }
                        MessageBox.Show("Deleted Successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        clearFields();
                        displayAllProducts();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Deletion Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void addProduct_removebutton_Click(object sender, EventArgs e)
        {


            if (emptyFields())
            {
                MessageBox.Show("Empty Fields. Select a data on the table first. ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Form1.role == "Admin")
                {
                    productRemove();
                }
                else
                {
                    AdminCredentials credentials = new AdminCredentials();
                    credentials.ShowDialog();

                    if (AdminCredentials.isTrue)
                    {
                        productRemove();
                        AdminCredentials.isTrue = false;
                    }
                }

            }



        }

        private int idGen;

        public int IDGenerator()
        {
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT MAX(log_id) FROM changelog";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        idGen = Convert.ToInt32(result);

                        idGen = idGen + 1;
                        return idGen;
                    }
                    else
                    {
                        idGen = 1;
                        return idGen;
                    }
                }
            }
        }

        public static string filter;
        private void addProduct_filterLocation_button_Click(object sender, EventArgs e)
        {
            if (addProduct_filterLocation.Text == "")
            {
                MessageBox.Show("Empty Fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                filter = addProduct_filterLocation.Text;

                AddProductsData apData = new AddProductsData();
                List<AddProductsData> listData = apData.FilterProductsData();

                dataGridView1.DataSource = listData;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);


            }
        }

        private void addProduct_filterLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (addProduct_filterLocation.Text == "")
                {
                    MessageBox.Show("Empty Fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    filter = addProduct_filterLocation.Text;

                    AddProductsData apData = new AddProductsData();
                    List<AddProductsData> listData = apData.FilterProductsData();

                    dataGridView1.DataSource = listData;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);



                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            displayAllProducts();
            addProduct_filterLocation.Text = "";
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
                    sfd.FileName = "Nibs_Products.xlsx";
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
                                worksheet.Name = "NibsProducts";
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

        private void scan_Click(object sender, EventArgs e)
        {
            Scanner scan = new Scanner();
            scan.Show();
            scan.startScan();
        }


        public void inputScan()
        {
            if (Scanner.Scanned == null)
            {
                MessageBox.Show("Scan a code first before proceeding.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string conn = Scanner.Scanned;
                addProduct_prodID.Text = conn;

            }



        }

        private void addProduct_inputCode_Click(object sender, EventArgs e)
        {
            inputScan();
            Scanner.Scanned = null;

            checkProductId();

        }

        private void addProduct_prodID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (addProduct_prodID.Text == "")
                {
                    MessageBox.Show("Product ID textbox is empty.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    checkProductId();
                }
            }
        }

        public void checkProductId()
        {
            try
            {
                connect.Open();
                string checkRow = "SELECT COUNT(*) FROM products WHERE prod_id = @prodID";

                using (MySqlCommand chk = new MySqlCommand(checkRow, connect))
                {
                    chk.Parameters.AddWithValue("@prodID", addProduct_prodID.Text.Trim());

                    int rowCount = (int)(long)(chk.ExecuteScalar());

                    if (rowCount > 0)
                    {
                        string selectp = $"SELECT * FROM products WHERE prod_id = @prodID";


                        using (MySqlCommand cmd = new MySqlCommand(selectp, connect))
                        {
                            cmd.Parameters.AddWithValue("@prodID", addProduct_prodID.Text.Trim());

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string prodName = reader["prod_name"].ToString();
                                    string prodLocation = reader["prod_location"].ToString();
                                    string prodPerson = reader["prod_personnel"].ToString();
                                    string cat = reader["category"].ToString();
                                    string avlbl = reader["status"].ToString();

                                    string stck = reader["stock"].ToString();

                                    string unit = reader["unit"].ToString();
                                    float prodPrice = Convert.ToSingle(reader["price"]);
                                    string supplier = reader["prod_supplier"].ToString();


                                    addProduct_prodName.Text = prodName;
                                    addProduct_category.SelectedItem = cat;
                                    addProduct_prodLocation.Text = prodLocation;
                                    addProduct_personC.Text = prodPerson;
                                    addProduct_price.Text = prodPrice.ToString();
                                    addProduct_stock.Text = stck;
                                    addProduct_unittxt.Text = unit;
                                    addProduct_status.SelectedItem = avlbl;
                                    addProduct_supplier.SelectedItem = supplier;

                                }
                            }

                        }
                    }
                    else
                    {

                    }
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }

        }

        private void AdminAddProducts_Load(object sender, EventArgs e)
        {
            colorManagement();
        }

        private void colorManagement()
        {
            addProduct_inputCode.BackColor = Form1.appC;
            addProduct_addbutton.BackColor = Form1.appC;
            addProduct_updatebutton.BackColor = Form1.appC;
            addProduct_removebutton.BackColor = Form1.appC;
            addProduct_clearbutton.BackColor = Form1.appC;
            addProduct_filterLocation_button.BackColor = Form1.appC;

            dataGridView1.DefaultCellStyle.BackColor = Form1.appC;

            addProduct_inputCode.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            addProduct_inputCode.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            addProduct_addbutton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            addProduct_addbutton.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            addProduct_updatebutton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            addProduct_updatebutton.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            addProduct_removebutton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            addProduct_removebutton.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            addProduct_clearbutton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            addProduct_clearbutton.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            addProduct_filterLocation_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            addProduct_filterLocation_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

        }
    }
}

