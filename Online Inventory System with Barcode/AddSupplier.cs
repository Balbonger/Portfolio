using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NibsInventoryManagementSystem
{
    public partial class AddSupplier : UserControl
    {
        MySqlConnection connect = new MySqlConnection(Form1.gateway);


        public AddSupplier()
        {
            InitializeComponent();

            displaySupplierData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displaySupplierData();
        }

        public void displaySupplierData()
        {
            SupplierData cData = new SupplierData();
            List<SupplierData> listData = cData.AllSuppliersData();

            dataGridView1.DataSource = listData;
        }

        private void supplierAdd_btn_Click(object sender, EventArgs e)
        {
            if (supplier_name.Text == "" || supplier_contact.Text == "" || supplier_cperson.Text == "" || supplier_address.Text == "")
            {
                MessageBox.Show("Empty Fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


                        string checkCat = "SELECT * FROM supplier WHERE supplier_name = @supp";

                        using (MySqlCommand cmd = new MySqlCommand(checkCat, connect))
                        {
                            cmd.Parameters.AddWithValue("@supp", supplier_name.Text.Trim());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show("Supplier named: '" + supplier_name.Text.Trim() + "' already exist.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO supplier (supplier_name,contact_info, contact_person, address, date) " +
                                    "VALUES(@supp, @contact, @contactp, @address, @date)";

                                using (MySqlCommand insertD = new MySqlCommand(insertData, connect))
                                {
                                    insertD.Parameters.AddWithValue("@supp", supplier_name.Text.Trim());
                                    insertD.Parameters.AddWithValue("@contact", supplier_contact.Text.Trim());
                                    insertD.Parameters.AddWithValue("@contactp", supplier_cperson.Text.Trim());
                                    insertD.Parameters.AddWithValue("@address", supplier_address.Text.Trim());

                                    DateTime today = DateTime.UtcNow.AddHours(8);
                                    insertD.Parameters.AddWithValue("@date", today);

                                    insertD.ExecuteNonQuery();



                                }

                                string addDesc = "Supplier Added: " + supplier_name.Text.Trim();

                                string insertChange = "INSERT INTO changelog (log_id, prod_name, destination, person_incharge, log_date, log_user) " +
                                    "VALUES (@tID, @prodName, @dest, @person, @date, @user)";

                                string username = Form1.username.Substring(0, 1).ToUpper() + Form1.username.Substring(1);

                                using (MySqlCommand cmdd = new MySqlCommand(insertChange, connect))
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

                                    MessageBox.Show("Supplier addded to the directory successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            clearFields();
                            displaySupplierData();

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
            if (connect.State == ConnectionState.Closed)
                return true;

            else
                return false;

        }

        public void clearFields()
        {
            supplier_name.Text = "";
            supplier_contact.Text = "";
            supplier_cperson.Text = "";
            supplier_address.Text = "";
            getID = 0;

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

        private int getID = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                getID = (int)row.Cells[0].Value;

                supplier_name.Text = row.Cells[1].Value.ToString();
                supplier_contact.Text = row.Cells[2].Value.ToString();
                supplier_cperson.Text = row.Cells[3].Value.ToString();
                supplier_address.Text = row.Cells[4].Value.ToString();

            }
        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            colorManagement();
        }

        private void colorManagement()
        {
            supplierAdd_btn.BackColor = Form1.appC;
            supplierUpdate_btn.BackColor = Form1.appC;
            supplierRemove_btn.BackColor = Form1.appC;
            supplierClear_btn.BackColor = Form1.appC;
            supplier_filterLocation_button.BackColor = Form1.appC;

            dataGridView1.DefaultCellStyle.BackColor = Form1.appC;



            supplierAdd_btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            supplierAdd_btn.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            supplierUpdate_btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            supplierUpdate_btn.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            supplierRemove_btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            supplierRemove_btn.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            supplierClear_btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            supplierClear_btn.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            supplier_filterLocation_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            supplier_filterLocation_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

        }

        private void supplierClear_btn_Click(object sender, EventArgs e)
        {
            clearFields();
            getID = 0;
        }

        private void supplierRemove_btn_Click(object sender, EventArgs e)
        {
            if (supplier_name.Text == "" || supplier_contact.Text == "" || getID == 0)
            {
                MessageBox.Show("Empty Fields. Select a data in the table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearFields();
            }
            else
            {
                if (Form1.role == "Admin")
                {
                    supplierRemove();
                }
                else
                {
                    AdminCredentials credentials = new AdminCredentials();
                    credentials.ShowDialog();

                    if (AdminCredentials.isTrue)
                    {
                        supplierRemove();
                        AdminCredentials.isTrue = false;
                    }
                }

            }
        }

        public void supplierRemove()
        {
            if (MessageBox.Show("Are you sure you want to Remove Supplier ID: " + getID + "?", "Information Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    IDGenerator();
                    try
                    {
                        connect.Open();

                        MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                        direct.ExecuteNonQuery();

                        string removeData = "DELETE FROM supplier WHERE id = @id";

                        using (MySqlCommand deleteD = new MySqlCommand(removeData, connect))
                        {
                            deleteD.Parameters.AddWithValue("@id", getID);

                            deleteD.ExecuteNonQuery();


                        }

                        string addDesc = "Supplier Removed: " + supplier_name.Text.Trim();

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

                        MessageBox.Show("Supplier removed successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        clearFields();
                        displaySupplierData();


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

        private void supplierUpdate_btn_Click(object sender, EventArgs e)
        {
            if (supplier_name.Text == "" || supplier_contact.Text == "" || getID == 0)
            {
                MessageBox.Show("Empty Fields. Select a data in the table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearFields();
            }
            else
            {
                if (Form1.role == "Admin")
                {
                    supplierUpdate();
                }
                else
                {
                    AdminCredentials credentials = new AdminCredentials();
                    credentials.ShowDialog();

                    if (AdminCredentials.isTrue)
                    {
                        supplierUpdate();
                        AdminCredentials.isTrue = false;
                    }
                }

            }
        }

        public void supplierUpdate()
        {
            if (MessageBox.Show("Are you sure you want to Update Supplier ID: " + getID + "?", "Information Message",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    IDGenerator();
                    try
                    {
                        connect.Open();

                        MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                        direct.ExecuteNonQuery();

                        string checkSuppUp = "SELECT * FROM supplier WHERE supplier_name = @supp AND contact_info = @contact" +
                            " AND contact_person = @contactp AND address = @address ";

                        using (MySqlCommand cmd = new MySqlCommand(checkSuppUp, connect))
                        {
                            cmd.Parameters.AddWithValue("@supp", supplier_name.Text.Trim());
                            cmd.Parameters.AddWithValue("@contact", supplier_contact.Text.Trim());
                            cmd.Parameters.AddWithValue("@contactp", supplier_cperson.Text.Trim());
                            cmd.Parameters.AddWithValue("@address", supplier_address.Text.Trim());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show("You haven't change anything on the selected Supplier.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {

                                string updateData = "UPDATE supplier SET supplier_name = @supp, contact_info = @contact " +
                                    ", contact_person = @contactp, address = @address WHERE id = @id";

                                using (MySqlCommand updateD = new MySqlCommand(updateData, connect))
                                {
                                    updateD.Parameters.AddWithValue("@id", getID);
                                    updateD.Parameters.AddWithValue("@supp", supplier_name.Text.Trim());
                                    updateD.Parameters.AddWithValue("@contact", supplier_contact.Text.Trim());
                                    updateD.Parameters.AddWithValue("@contactp", supplier_cperson.Text.Trim());
                                    updateD.Parameters.AddWithValue("@address", supplier_address.Text.Trim());



                                    updateD.ExecuteNonQuery();



                                }

                                string addDesc = "Supplier Updated: " + supplier_name.Text.Trim();

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

                                MessageBox.Show("Supplier Updated successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }




                        clearFields();
                        displaySupplierData();

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            supplier_filterLocation.Text = "";
            displaySupplierData();
        }


        public static string supplierfilter;
        private void supplier_filterLocation_button_Click(object sender, EventArgs e)
        {
            if (supplier_filterLocation.Text == "")
            {
                MessageBox.Show("Empty Fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                supplierfilter = supplier_filterLocation.Text;

                SupplierData apData = new SupplierData();
                List<SupplierData> listData = apData.FilterSupplierData();

                dataGridView1.DataSource = listData;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);


            }
        }

        private void supplier_filterLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                supplier_filterLocation_button_Click(sender, e);
            }
        }
    }
}
