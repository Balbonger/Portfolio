using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;


namespace NibsInventoryManagementSystem
{
    public partial class AdminAddCategories : UserControl
    {
        MySqlConnection connect = new MySqlConnection(Form1.gateway);

        public AdminAddCategories()
        {
            InitializeComponent();

            displayCategoriesData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayCategoriesData();
        }

        public void displayCategoriesData()
        {
            CategoriesData cData = new CategoriesData();
            List<CategoriesData> listData = cData.AllCategoriesData();

            dataGridView1.DataSource = listData;
        }

        private void categoryAdd_button_Click(object sender, EventArgs e)
        {
            if (category_name.Text == "")
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


                        string checkCat = "SELECT * FROM categories WHERE category = @cat";

                        using (MySqlCommand cmd = new MySqlCommand(checkCat, connect))
                        {
                            cmd.Parameters.AddWithValue("@cat", category_name.Text.Trim());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show("Category named: " + category_name.Text.Trim() + " is already exist.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO categories (category, date) VALUES(@cat, @date)";

                                using (MySqlCommand insertD = new MySqlCommand(insertData, connect))
                                {
                                    insertD.Parameters.AddWithValue("@cat", category_name.Text.Trim());
                                    DateTime today = DateTime.UtcNow.AddHours(8);
                                    insertD.Parameters.AddWithValue("@date", today);

                                    insertD.ExecuteNonQuery();



                                }

                                string addDesc = "Category Added: " + category_name.Text.Trim();

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

                                MessageBox.Show("Category Addded Successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                clearFields();
                                displayCategoriesData();
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
            if (connect.State == ConnectionState.Closed)
                return true;

            else
                return false;

        }

        public void clearFields()
        {
            category_name.Text = "";
            getID = 0;

        }

        private void categoryClear_button_Click(object sender, EventArgs e)
        {
            clearFields();

        }

        private int getID = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                getID = (int)row.Cells[0].Value;

                category_name.Text = row.Cells[1].Value.ToString();

            }
        }

        private void categoryUpdate_button_Click(object sender, EventArgs e)
        {
            if (category_name.Text == "" || getID == 0)
            {
                MessageBox.Show("Empty Fields. Sealect a data in the table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearFields();
            }
            else
            {
                if (Form1.role == "Admin")
                {
                    catUpdate();
                }
                else
                {
                    AdminCredentials credentials = new AdminCredentials();
                    credentials.ShowDialog();

                    if (AdminCredentials.isTrue)
                    {
                        catUpdate();
                        AdminCredentials.isTrue = false;
                    }
                }

            }
        }

        public void categoryRemove()
        {

            if (MessageBox.Show("Are you sure you want to Remove Cat ID: " + getID + "?", "Information Message",
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

                        string removeData = "DELETE FROM categories WHERE id = @id";

                        using (MySqlCommand deleteD = new MySqlCommand(removeData, connect))
                        {
                            deleteD.Parameters.AddWithValue("@id", getID);

                            deleteD.ExecuteNonQuery();




                        }

                        string addDesc = "Category Removed: " + category_name.Text.Trim();

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
                            MessageBox.Show("Removed successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                        clearFields();
                        displayCategoriesData();


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

        public void catUpdate()
        {
            if (MessageBox.Show("Are you sure you want to Update Cat ID: " + getID + "?", "Information Message",
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

                        string checkCatUp = "SELECT * FROM categories WHERE category = @cat";

                        using (MySqlCommand cmd = new MySqlCommand(checkCatUp, connect))
                        {
                            cmd.Parameters.AddWithValue("@cat", category_name.Text.Trim());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show("You haven't change anything on the selected category", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {

                                string updateData = "UPDATE categories SET category = @cat WHERE id = @id";

                                using (MySqlCommand updateD = new MySqlCommand(updateData, connect))
                                {
                                    updateD.Parameters.AddWithValue("@cat", category_name.Text.Trim());
                                    updateD.Parameters.AddWithValue("@id", getID);

                                    updateD.ExecuteNonQuery();



                                }

                                string addDesc = "Category Updated: " + category_name.Text.Trim();

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

                                MessageBox.Show("Category Updated successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }




                        clearFields();
                        displayCategoriesData();

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


        private void categoryRemove_button_Click(object sender, EventArgs e)
        {
            if (category_name.Text == "" || getID == 0)
            {
                MessageBox.Show("Empty Fields. ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearFields();
            }
            else
            {
                if (Form1.role == "Admin")
                {
                    categoryRemove();
                }
                else
                {
                    AdminCredentials credentials = new AdminCredentials();
                    credentials.ShowDialog();

                    if (AdminCredentials.isTrue)
                    {
                        categoryRemove();
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

        private void AdminAddCategories_Load(object sender, EventArgs e)
        {
            colorManagement();
        }

        private void colorManagement()
        {
            categoryAdd_button.BackColor = Form1.appC;
            categoryUpdate_button.BackColor = Form1.appC;
            categoryRemove_button.BackColor = Form1.appC;
            categoryClear_button.BackColor = Form1.appC;
            category_filterLocation_button.BackColor = Form1.appC;

            dataGridView1.DefaultCellStyle.BackColor = Form1.appC;

            categoryAdd_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            categoryAdd_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            categoryUpdate_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            categoryUpdate_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            categoryRemove_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            categoryRemove_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            categoryClear_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            categoryClear_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            category_filterLocation_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            category_filterLocation_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            displayCategoriesData();
            category_filterLocation.Text = "";
        }


        public static string categoryfilter;
        private void category_filterLocation_button_Click(object sender, EventArgs e)
        {
            if (category_filterLocation.Text == "")
            {
                MessageBox.Show("Empty Fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                categoryfilter = category_filterLocation.Text;

                CategoriesData apData = new CategoriesData();
                List<CategoriesData> listData = apData.FilterCategoriesData();

                dataGridView1.DataSource = listData;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);


            }
        }

        private void category_filterLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                category_filterLocation_button_Click(sender, e);

            }
        }

        private void category_filterLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}

