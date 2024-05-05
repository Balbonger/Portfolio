using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;


namespace NibsInventoryManagementSystem
{
    public partial class AdminAddUsers : UserControl
    {
        MySqlConnection connect = new MySqlConnection
            (Form1.gateway);

        public AdminAddUsers()
        {
            InitializeComponent();

            displayAllUsersData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayAllUsersData();
        }
        public void displayAllUsersData()
        {
            UsersData uData = new UsersData();

            List<UsersData> listData = uData.AllUsersData();

            dataGridView1.DataSource = listData;
            dataGridView1.DataSource = listData;
        }



        private void useradd_button_Click(object sender, EventArgs e)
        {
            if (addUser_username.Text == "" || addUser_password.Text == "" || addUser_role.SelectedIndex == -1 || addUser_status.SelectedIndex == -1)
            {
                MessageBox.Show("Empty fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


                        string checkUsername = "SELECT * FROM users WHERE username = @usern";

                        using (MySqlCommand cmd = new MySqlCommand(checkUsername, connect))
                        {
                            cmd.Parameters.AddWithValue("@usern", addUser_username.Text.Trim());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            System.Data.DataTable table = new System.Data.DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show(addUser_username.Text.Trim() + " is already taken.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else
                            {


                                string insertDate = "INSERT INTO users (username, password, role, status, date) " +
                                    "VALUES(@usern, @pass, @role, @status, @date)";

                                using (MySqlCommand insertD = new MySqlCommand(insertDate, connect))
                                {
                                    insertD.Parameters.AddWithValue("@usern", addUser_username.Text.Trim());
                                    insertD.Parameters.AddWithValue("@pass", addUser_password.Text.Trim());
                                    insertD.Parameters.AddWithValue("@role", addUser_role.SelectedItem.ToString());
                                    insertD.Parameters.AddWithValue("@status", addUser_status.SelectedItem.ToString());

                                    DateTime today = DateTime.UtcNow.AddHours(8);
                                    insertD.Parameters.AddWithValue("@date", today);

                                    insertD.ExecuteNonQuery();


                                }


                                string addDesc = "User Added: " + addUser_username.Text.Trim();

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
                                    MessageBox.Show(" User Added Successfully. ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }



                                clearFields();
                                displayAllUsersData();


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

        private void userupdate_button_Click(object sender, EventArgs e)
        {
            if (addUser_username.Text == "" || addUser_password.Text == "" || addUser_role.SelectedIndex == -1 || addUser_status.SelectedIndex == -1
                || getID == 0)
            {
                MessageBox.Show("Empty fields, Select a data in the table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearFields();
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Update User ID: " + getID + "?", "Confirmation Message",
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


                            string checkUserUp = "SELECT * FROM users WHERE username = @usern AND password = @pass AND role = @role AND status = @status";

                            using (MySqlCommand cmd = new MySqlCommand(checkUserUp, connect))
                            {
                                cmd.Parameters.AddWithValue("@usern", addUser_username.Text.Trim());
                                cmd.Parameters.AddWithValue("@pass", addUser_password.Text.Trim());
                                cmd.Parameters.AddWithValue("@role", addUser_role.SelectedItem);
                                cmd.Parameters.AddWithValue("@status", addUser_status.SelectedItem);

                                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                System.Data.DataTable table = new System.Data.DataTable();

                                adapter.Fill(table);

                                if (table.Rows.Count > 0)
                                {
                                    MessageBox.Show("You haven't change anything on the selected user.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {

                                    string updateData = "UPDATE users SET username = @usern, " +
                                    "password = @pass, role = @role, status = @status WHERE id = @id";

                                    using (MySqlCommand updateD = new MySqlCommand(updateData, connect))
                                    {
                                        updateD.Parameters.AddWithValue("@usern", addUser_username.Text.Trim());
                                        updateD.Parameters.AddWithValue("@pass", addUser_password.Text.Trim());
                                        updateD.Parameters.AddWithValue("@role", addUser_role.SelectedItem);
                                        updateD.Parameters.AddWithValue("@status", addUser_status.SelectedItem);
                                        updateD.Parameters.AddWithValue("@id", getID);

                                        updateD.ExecuteNonQuery();



                                    }

                                    string addDesc = "User Updated: " + addUser_username.Text.Trim();

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

                                    MessageBox.Show("User Updated Successfully! ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                            }


                            clearFields();
                            displayAllUsersData();

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

        private void userremove_button_Click(object sender, EventArgs e)
        {

        }

        private void userclear_button_Click(object sender, EventArgs e)
        {

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

        private void userclear_button_Click_1(object sender, EventArgs e)
        {
            clearFields();
        }

        public void clearFields()
        {
            addUser_username.Text = "";
            addUser_password.Text = "";
            addUser_role.SelectedIndex = -1;
            addUser_status.SelectedIndex = -1;

            getID = 0;
        }

        private int getID = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                getID = (int)row.Cells[0].Value;
                string username = row.Cells[1].Value.ToString();
                string password = row.Cells[2].Value.ToString();
                string role = row.Cells[3].Value.ToString();
                string status = row.Cells[4].Value.ToString();

                addUser_username.Text = username;
                addUser_password.Text = password;
                addUser_role.Text = role;
                addUser_status.Text = status;
            }
        }

        private void userremove_button_Click_1(object sender, EventArgs e)
        {
            if (addUser_username.Text == "" || addUser_password.Text == "" || addUser_role.SelectedIndex == -1 || addUser_status.SelectedIndex == -1
                || getID == 0)
            {
                MessageBox.Show("Empty fields, Select a data in the table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearFields();
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Remove User ID: " + getID + "?", "Confirmation Message",
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


                            string updateData = "DELETE FROM users WHERE id = @id";

                            using (MySqlCommand updateD = new MySqlCommand(updateData, connect))
                            {
                                updateD.Parameters.AddWithValue("@id", getID);
                                updateD.ExecuteNonQuery();


                            }

                            string addDesc = "User Removed: " + addUser_username.Text.Trim();

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

                            MessageBox.Show("User Removed Successfully! ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            clearFields();
                            displayAllUsersData();


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

        private void AdminAddUsers_Load(object sender, EventArgs e)
        {
            colorManagement();
        }

        private void colorManagement()
        {
            useradd_button.BackColor = Form1.appC;
            userupdate_button.BackColor = Form1.appC;
            userremove_button.BackColor = Form1.appC;
            userclear_button.BackColor = Form1.appC;
            users_filterLocation_button.BackColor= Form1.appC;

            dataGridView1.DefaultCellStyle.BackColor = Form1.appC;

            useradd_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            useradd_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            userupdate_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            userupdate_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            userremove_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            userremove_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            userclear_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            userclear_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            users_filterLocation_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            users_filterLocation_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            displayAllUsersData();
            users_filterLocation.Text = "";
        }

        public static string userfilter;
        private void users_filterLocation_button_Click(object sender, EventArgs e)
        {
            if (users_filterLocation.Text == "")
            {
                MessageBox.Show("Empty Fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                userfilter = users_filterLocation.Text;

                UsersData apData = new UsersData();
                List<UsersData> listData = apData.FilterUsersData();

                dataGridView1.DataSource = listData;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);


            }
        }

        private void users_filterLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                users_filterLocation_button_Click(sender, e);
            }
        }
    }

}
