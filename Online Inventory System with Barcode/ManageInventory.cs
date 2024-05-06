using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace NibsInventoryManagementSystem
{
    public partial class ManageInventory : UserControl
    {
        MySqlConnection connect = new MySqlConnection(Form1.gateway);

        public ManageInventory()
        {
            InitializeComponent();

            displayAllAvailableProducts();
            displayTotalItems();
            displayInventoryManagement();
            displayClient();
            setMode();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayAllAvailableProducts();
            displayTotalItems();
            displayInventoryManagement();
            displayClient();
        }
        public void displayAllAvailableProducts()
        {
            AddProductsData apData = new AddProductsData();
            List<AddProductsData> listData = apData.allAvailableProducts();


            dataGridView1.DataSource = listData;


        }

        public void displayInventoryManagement()
        {
            OrdersData oData = new OrdersData();
            List<OrdersData> listData = oData.allOrdersData();

            dataGridView2.DataSource = listData;


        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // not used
        }

        public bool checkConnection()
        {
            if (connect.State == ConnectionState.Closed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void displayClient()
        {
            if (checkConnection())
            {
                try
                {
                    connect.Open();

                    MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                    direct.ExecuteNonQuery();


                    string selectData = "SELECT * FROM client";

                    using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();

                        manageInventory_client.Items.Clear();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                manageInventory_client.Items.Add(reader["client_name"].ToString());
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

        public void retrieveProduct()
        {
            string selectedValue = manageInventory_prodIDtxt.Text as string;

            if (checkConnection())
            {
                try
                {
                    connect.Open();

                    MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                    direct.ExecuteNonQuery();

                    string checkRow = "SELECT COUNT(*) FROM products WHERE prod_id = @prodID";

                    using (MySqlCommand chk = new MySqlCommand(checkRow, connect))
                    {
                        chk.Parameters.AddWithValue("@prodID", manageInventory_prodIDtxt.Text.Trim());

                        int rowCount = (int)(long)(chk.ExecuteScalar());

                        if (rowCount > 0)
                        {
                            string selectData = $"SELECT * FROM products WHERE prod_id = '{selectedValue}' AND status = @status AND stock >0";

                            using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                            {
                                cmd.Parameters.AddWithValue("@status", "Available");

                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string prodName = reader["prod_name"].ToString();
                                        string prodLocation = reader["prod_location"].ToString();
                                        string prodPerson = reader["prod_personnel"].ToString();
                                        string prodCat = reader["category"].ToString();
                                        string prodSupp = reader["prod_supplier"].ToString();

                                        string unit = reader["unit"].ToString();
                                        float prodPrice = Convert.ToSingle(reader["price"]);

                                        manageInventory_prodName.Text = prodName;
                                        manageInventory_category.Text = prodCat;
                                        manageInventory_prodLocation.Text = prodLocation;
                                        manageInventory_pp.Text = prodPerson;
                                        manageInventory_price.Text = prodPrice.ToString("0.00");

                                        manageInventory_unit.Text = unit;
                                        manageInventory_supplier.Text = prodSupp;

                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Product ID: " + manageInventory_prodIDtxt.Text + " doesn't exist. Check the code or use a scanner."
                                , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect?.Close();
                }
            }
        }


        private void manageInventory_addbutton_Click(object sender, EventArgs e)
        {
            IDGenerator();

            if (manageInventory_category.Text == "" || manageInventory_prodIDtxt.Text == "" ||
                manageInventory_prodName.Text == "" || manageInventory_price.Text == "" || manageInventory_quantity.Value == 0)
            {
                MessageBox.Show("Select an item first.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (checkConnection())
                {
                    try
                    {
                        connect.Open();

                        MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                        direct.ExecuteNonQuery();



                        int Stock = 0;

                        string selectOrder = "SELECT * FROM products WHERE prod_id = @prodID";

                        using (MySqlCommand getOrder = new MySqlCommand(selectOrder, connect))
                        {
                            getOrder.Parameters.AddWithValue("@prodID", manageInventory_prodIDtxt.Text.Trim());
                        }

                        //Check if Stock is > QTY
                        string checkQty = "SELECT stock FROM  products WHERE prod_id = @prodID";

                        using (MySqlCommand cOrder = new MySqlCommand(checkQty, connect))
                        {
                            cOrder.Parameters.AddWithValue("@prodID", manageInventory_prodIDtxt.Text.Trim());

                            object result = cOrder.ExecuteScalar();


                            using (MySqlDataReader reader = cOrder.ExecuteReader())
                            {
                                if (result != DBNull.Value)
                                {
                                    int cStock = Convert.ToInt32(result);
                                    Stock = cStock;
                                }
                            }
                        }


                        if (Stock < manageInventory_quantity.Value && isPullout)
                        {
                            MessageBox.Show("The quantity of the item you want to add is more than what is stocked. ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string check = "SELECT COUNT(*) FROM orders WHERE prod_id = @prodId";

                            using (MySqlCommand cc = new MySqlCommand(check, connect))
                            {
                                cc.Parameters.AddWithValue("@prodID", manageInventory_prodIDtxt.Text.Trim());

                                int rowCount = (int)(long)(cc.ExecuteScalar());


                                if (rowCount > 0)
                                {
                                    MessageBox.Show("The product is already in the list. ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    OrderGenerator();
                                    string inserData = "INSERT INTO orders(transac_id, prod_id, prod_name, category, qty, order_date, unit) " +
                                            "VALUES(@tID, @prodId, @prodName, @cat, @qty, @date, @unit)";

                                    using (MySqlCommand cmd = new MySqlCommand(inserData, connect))
                                    {
                                        cmd.Parameters.AddWithValue("tID", OrdGen);
                                        cmd.Parameters.AddWithValue("@prodID", manageInventory_prodIDtxt.Text.Trim());
                                        cmd.Parameters.AddWithValue("@prodName", manageInventory_prodName.Text.Trim());
                                        cmd.Parameters.AddWithValue("@cat", manageInventory_category.Text);
                                        cmd.Parameters.AddWithValue("@qty", manageInventory_quantity.Value);

                                        DateTime today = DateTime.UtcNow.AddHours(8);

                                        cmd.Parameters.AddWithValue("@date", today);
                                        cmd.Parameters.AddWithValue("@unit", manageInventory_unit.Text.Trim());


                                        cmd.ExecuteNonQuery();


                                    }
                                }
                            }

                        }

                        displayTotalItems();
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
            displayInventoryManagement();
            clearFields();
        }

        public void displayTotalItems()
        {
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();



                string selectData = $"SELECT SUM(qty) FROM orders";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    object result = cmd.ExecuteScalar();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (result != DBNull.Value)
                        {
                            float totalitems = Convert.ToSingle(result);
                            manageInventory_totalItems.Text = totalitems.ToString("0.00");
                        }
                    }
                }
            }

        }

        private int OrdGen;

        public int OrderGenerator()
        {
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT MAX(transac_id) FROM orders";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        OrdGen = Convert.ToInt32(result);

                        OrdGen = OrdGen + 1;
                        return OrdGen;
                    }
                    else
                    {
                        OrdGen = 1;
                        return OrdGen;
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

        private void manageInventory_removebutton_Click(object sender, EventArgs e)
        {
            if (prodID == 0)
            {
                MessageBox.Show("Please select an item first on the summary table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Remove ID: " + prodID + " from the summary?", "Confirmation Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (checkConnection())
                    {
                        try
                        {
                            connect.Open();

                            MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                            direct.ExecuteNonQuery();



                            string deleteData = "DELETE FROM orders where id = @id";

                            using (MySqlCommand cmd = new MySqlCommand(deleteData, connect))
                            {
                                cmd.Parameters.AddWithValue("@id", prodID);

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Removed Successfully! ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                manageInventory_totalItems.Text = "0";
                            }

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

                displayTotalItems();
                displayInventoryManagement();
                clearFields();
            }
        }


        private int prodID = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];


                manageInventory_category.Text = row.Cells[3].Value.ToString();
                manageInventory_prodIDtxt.Text = row.Cells[1].Value.ToString();
                manageInventory_prodName.Text = row.Cells[2].Value.ToString();
                manageInventory_price.Text = row.Cells[4].Value.ToString();
                manageInventory_unit.Text = row.Cells[6].Value.ToString();
                manageInventory_prodLocation.Text = row.Cells[9].Value.ToString();
                manageInventory_pp.Text = row.Cells[10].Value.ToString();
                manageInventory_supplier.Text = row.Cells[11].Value.ToString();
            }

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
            prodID = (int)row.Cells[0].Value;
        }

        private void manageInventory_removebutton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Use to remove items from the summary.", manageInventory_removebutton);
            //toolTip1.OwnerDraw = true;
            //toolTip1.ForeColor = Color.White;
            //toolTip1.BackColor = Color.FromArgb(85, 113, 73);
        }

        private void manageInventory_addbutton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Use to add items to the summary.", manageInventory_addbutton);

        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        public void clearFields()
        {
            manageInventory_category.Text = "";
            manageInventory_category.Text = "";
            manageInventory_prodIDtxt.Text = "";
            manageInventory_prodName.Text = "";
            manageInventory_price.Text = "";
            manageInventory_quantity.Value = 0;
            manageInventory_destination.Text = "";
            manageInventory_personnel.Text = "";
            manageInventory_prodLocation.Text = "";
            manageInventory_pp.Text = "";
            manageInventory_unit.Text = "unit";
            manageInventory_supplier.Text = "";
            manageInventory_client.SelectedIndex = -1;
        }

        private void manageInventory_clearbutton_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        public void pulloutExec()
        {
            IDGenerator();


            if (checkConnection())
            {
                try
                {
                    connect.Open();

                    MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                    direct.ExecuteNonQuery();

                    string formdescription = "SELECT GROUP_CONCAT(CONCAT(qty, unit, ' ', prod_name)) AS 'combined_A' FROM orders ";

                    using (MySqlCommand cmd = new MySqlCommand(formdescription, connect))
                    {
                        object result = cmd.ExecuteScalar();


                        string insertData = "INSERT INTO changelog (log_id, prod_name, destination, person_incharge, log_date, log_user, client) " +
                            "VALUES (@tID, @prodName, @dest, @person, @date, @user, @client)";

                        string username = Form1.username.Substring(0, 1).ToUpper() + Form1.username.Substring(1);

                        using (MySqlCommand cmdd = new MySqlCommand(insertData, connect))
                        {
                            cmdd.Parameters.AddWithValue("@tID", idGen);
                            cmdd.Parameters.AddWithValue("@prodName", "Removed Item/s: " + result.ToString());
                            cmdd.Parameters.AddWithValue("@dest", manageInventory_destination.Text);
                            cmdd.Parameters.AddWithValue("@person", manageInventory_personnel.Text);

                            DateTime today = DateTime.UtcNow.AddHours(8);
                            cmdd.Parameters.AddWithValue("@date", today);
                            cmdd.Parameters.AddWithValue("@user", username);
                            cmdd.Parameters.AddWithValue("@client", manageInventory_client.SelectedItem.ToString().Trim());
                            cmdd.ExecuteNonQuery();


                            string updateProducts = "UPDATE products " +
                                "JOIN orders ON products.prod_id = orders.prod_id " +
                                "SET products.stock = products.stock - orders.qty";

                            using (MySqlCommand updateP = new MySqlCommand(updateProducts, connect))
                            {
                                updateP.ExecuteNonQuery();
                            }


                            string updateOrders = "DELETE FROM orders";

                            using (MySqlCommand updateO = new MySqlCommand(updateOrders, connect))
                            {
                                updateO.ExecuteNonQuery();
                            }

                            clearFields();
                            displayAllAvailableProducts();
                            displayInventoryManagement();


                            MessageBox.Show("Pullout Complete and Log Created!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        public void restockExec()
        {
            IDGenerator();


            if (checkConnection())
            {
                try
                {
                    connect.Open();

                    MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                    direct.ExecuteNonQuery();

                    string formdescription = "SELECT GROUP_CONCAT(CONCAT(qty, unit, ' ', prod_name)) AS 'combined_A' FROM orders ";

                    using (MySqlCommand cmd = new MySqlCommand(formdescription, connect))
                    {
                        object result = cmd.ExecuteScalar();


                        string insertData = "INSERT INTO changelog (log_id, prod_name, destination, person_incharge, log_date, log_user) " +
                            "VALUES (@tID, @prodName, @dest, @person, @date, @user)";

                        string username = Form1.username.Substring(0, 1).ToUpper() + Form1.username.Substring(1);

                        using (MySqlCommand cmdd = new MySqlCommand(insertData, connect))
                        {
                            cmdd.Parameters.AddWithValue("@tID", idGen);
                            cmdd.Parameters.AddWithValue("@prodName", "Restocked Item/s: " + result.ToString());
                            cmdd.Parameters.AddWithValue("@dest", manageInventory_destination.Text);
                            cmdd.Parameters.AddWithValue("@person", manageInventory_personnel.Text);

                            DateTime today = DateTime.UtcNow.AddHours(8);
                            cmdd.Parameters.AddWithValue("@date", today);
                            cmdd.Parameters.AddWithValue("@user", username);
                            cmdd.ExecuteNonQuery();


                            string updateProducts = "UPDATE products " +
                                "JOIN orders ON products.prod_id = orders.prod_id " +
                                "SET products.stock = products.stock + orders.qty";

                            using (MySqlCommand updateP = new MySqlCommand(updateProducts, connect))
                            {
                                updateP.ExecuteNonQuery();
                            }


                            string updateOrders = "DELETE FROM orders";

                            using (MySqlCommand updateO = new MySqlCommand(updateOrders, connect))
                            {
                                updateO.ExecuteNonQuery();
                            }

                            clearFields();
                            displayAllAvailableProducts();
                            displayInventoryManagement();


                            MessageBox.Show("Restock Complete and Log Created!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        private void manageInventory_proceed_Click(object sender, EventArgs e)
        {
            if (isPullout)
            {
                if (dataGridView2.Rows.Count <= 0 || manageInventory_destination.Text == "" || manageInventory_personnel.Text == "" ||
                manageInventory_client.SelectedIndex == -1)
                {
                    MessageBox.Show("Empty Fields/Summary Table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    if(MessageBox.Show("Are you sure you want to PULLOUT the following selected items?", "Pullout Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        createReceipt();
                    }
                }
            }
            else
            {
                if (dataGridView2.Rows.Count <= 0 )
                {
                    MessageBox.Show("Empty Fields/Summary Table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to RESTOCK the following selected items?", "Restock Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        createReceipt();
                    }
                }
            }
            

        }

        private void manageInventory_discard_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count < 0)
            {
                MessageBox.Show("Empty Fields/Summary Table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to discard the summary list?", "Confirmation Message", MessageBoxButtons.YesNo
                    , MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (checkConnection())
                    {
                        try
                        {
                            connect.Open();

                            MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                            direct.ExecuteNonQuery();



                            string updateOrders = "DELETE FROM orders";

                            using (MySqlCommand updateO = new MySqlCommand(updateOrders, connect))
                            {
                                updateO.ExecuteNonQuery();
                            }

                            manageInventory_destination.Text = "";
                            manageInventory_personnel.Text = "";

                            displayInventoryManagement();

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
        }

        private void manageInventory_prodIDtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (manageInventory_prodIDtxt.Text == "")
                {
                    MessageBox.Show("Product Id field is empty. ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    retrieveProduct();
                }
            }
        }

        private void manageInventory_inputCode_Click(object sender, EventArgs e)
        {


            if (Scanner.Scanned == null)
            {
                MessageBox.Show("Scan a code first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                inputScan();
                Scanner.Scanned = null;

                retrieveProduct();
            }

        }

        public void inputScan()
        {
            string conn = Scanner.Scanned;
            manageInventory_prodIDtxt.Text = conn;



        }

        private void manageInventory_scan_Click(object sender, EventArgs e)
        {
            Scanner scan = new Scanner();
            scan.Show();
            scan.startScan();
        }

        public void createReceipt()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel (.xlsx)|  *.xlsx";
                sfd.FileName = "Nibs_Removed_Products.xlsx";
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
                            worksheet.Name = "NibsRemovedProducts";

                            worksheet.Application.ActiveWindow.SplitRow = 1;
                            worksheet.Application.ActiveWindow.FreezePanes = true;

                            for (int i = 1; i < dataGridView2.Columns.Count + 1; i++)
                            {
                                worksheet.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Font.NAME = "Calibri";
                                worksheet.Cells[1, i].Font.Bold = true;
                                worksheet.Cells[1, i].Font.Color = Color.White;
                                worksheet.Cells[1, i].Interior.Color = Form1.appC;
                                worksheet.Cells[1, i].Font.Size = 12;
                            }

                            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                            {
                                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 2, j + 1] = "'" + dataGridView2.Rows[i].Cells[j].Value.ToString();
                                }
                            }

                            worksheet.Columns.AutoFit();
                            workbook.SaveAs(sfd.FileName);
                            XcelApp.Quit();

                            ReleaseObject(worksheet);
                            ReleaseObject(workbook);
                            ReleaseObject(XcelApp);



                            if (MessageBox.Show("Receipt has been created for the activity. Check the designated folder.", "Info") == DialogResult.OK)
                            {
                                if (isPullout)
                                {
                                    //Console.WriteLine("Application will pullout Items");
                                    pulloutExec();
                                }
                                else
                                {
                                    //Console.WriteLine("Application will restock Items");

                                    restockExec();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
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

        private void ManageInventory_Load(object sender, EventArgs e)
        {
            colorManagement();
        }

        public void colorManagement()
        {
            manageInventory_inputCode.BackColor = Form1.appC;
            manageInventory_addbutton.BackColor = Form1.appC;
            manageInventory_clearbutton.BackColor = Form1.appC;
            manageInventory_removebutton.BackColor = Form1.appC;
            manageInventory_proceed.BackColor = Form1.appC;
            manageInventory_discard.BackColor = Form1.appC;

            dataGridView1.DefaultCellStyle.BackColor = Form1.appC;
            dataGridView2.DefaultCellStyle.BackColor = Form1.appC;

            manageInventory_inputCode.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            manageInventory_inputCode.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            manageInventory_addbutton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            manageInventory_addbutton.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            manageInventory_clearbutton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            manageInventory_clearbutton.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            manageInventory_removebutton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            manageInventory_removebutton.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            manageInventory_proceed.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            manageInventory_proceed.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            manageInventory_discard.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            manageInventory_discard.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

        }

        private bool isPullout = false;

        private void manageInventory_mode_Click(object sender, EventArgs e)
        {
            if (isPullout)
            {
                manageInventory_mode.Text = "Restock Mode";
                manageInventory_mode.BackColor = Color.Green;
                manageInventory_mode.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Color.Green);
                manageInventory_mode.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Color.Green);

                isPullout = false;

                manageInventory_client.Visible = false;
                label13.Visible = false;
                label17.Visible = false;
                label18.Visible = false;
                manageInventory_destination.Visible = false;
                manageInventory_personnel.Visible = false;

                clearFields();
                //Console.WriteLine("Restock");



            }
            else
            {
                manageInventory_mode.Text = "Pullout Mode";
                manageInventory_mode.BackColor = Color.Maroon;
                manageInventory_mode.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Color.Maroon);
                manageInventory_mode.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Color.Maroon);

                isPullout = true;

                manageInventory_client.Visible = true;
                label13.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                manageInventory_destination.Visible = true;
                manageInventory_personnel.Visible = true;

                clearFields();
                //Console.WriteLine("Pullout");
            }


            connect.Open();

            MySqlCommand direct = new MySqlCommand(Form1.database, connect);
            direct.ExecuteNonQuery();

            string updateOrders = "DELETE FROM orders";

            using (MySqlCommand updateO = new MySqlCommand(updateOrders, connect))
            {
                updateO.ExecuteNonQuery();
            }

            displayInventoryManagement();

            connect.Close();
        }

        public void setMode()
        {
            if (isPullout)
            {
                manageInventory_mode.Text = "Pullout Mode";
                manageInventory_mode.BackColor = Color.Maroon;
                manageInventory_mode.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Color.Maroon);
                manageInventory_mode.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Color.Maroon);

                manageInventory_client.Visible = true;
                label13.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                manageInventory_destination.Visible = true;
                manageInventory_personnel.Visible = true;
            }
            else
            {
                manageInventory_mode.Text = "Restock Mode";
                manageInventory_mode.BackColor = Color.Green;
                manageInventory_mode.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Color.Green);
                manageInventory_mode.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Color.Green);

                manageInventory_client.Visible = false;
                label13.Visible = false;
                label17.Visible = false;
                label18.Visible = false;
                manageInventory_destination.Visible = false;
                manageInventory_personnel.Visible = false;
            }
        }


    }
}
