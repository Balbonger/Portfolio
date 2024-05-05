using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MySql.Data.MySqlClient;

namespace NibsInventoryManagementSystem
{
    public partial class AddClient : UserControl
    {
        MySqlConnection connect = new MySqlConnection(Form1.gateway);

        public AddClient()
        {
            InitializeComponent();

            displayClientData();
        }

        public void displayClientData()
        {
            ClientsData cData = new ClientsData();
            List<ClientsData> listData = cData.AllClientsData();

            dataGridView1.DataSource = listData;
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayClientData();
        }


        private void clientAdd_btn_Click(object sender, EventArgs e)
        {
            if (client_name.Text == "" || client_contact.Text == "" || client_cperson.Text == "" || client_address.Text == "")
            {
                MessageBox.Show("Fill up all the fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


                        string checkCat = "SELECT * FROM client WHERE client_name = @client";

                        using (MySqlCommand cmd = new MySqlCommand(checkCat, connect))
                        {
                            cmd.Parameters.AddWithValue("@client", client_name.Text.Trim());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show("Client named: '" + client_name.Text.Trim() + "' already exist.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO client (client_name, contact_info, contact_person, address, date) " +
                                    "VALUES(@client, @contact, @contactp, @address, @date)";

                                using (MySqlCommand insertD = new MySqlCommand(insertData, connect))
                                {
                                    insertD.Parameters.AddWithValue("@client", client_name.Text.Trim());
                                    insertD.Parameters.AddWithValue("@contact", client_contact.Text.Trim());
                                    insertD.Parameters.AddWithValue("@contactp", client_cperson.Text.Trim());
                                    insertD.Parameters.AddWithValue("@address", client_address.Text.Trim());

                                    DateTime today = DateTime.UtcNow.AddHours(8);
                                    insertD.Parameters.AddWithValue("@date", today);

                                    insertD.ExecuteNonQuery();



                                }

                                string addDesc = "Client Added: " + client_name.Text.Trim();

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

                                MessageBox.Show("Client addded to the directory successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
 
                            }
                            clearFields();
                            displayClientData();
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
            client_name.Text = "";
            client_contact.Text = "";
            client_cperson.Text = "";
            client_address.Text = "";
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

                client_name.Text = row.Cells[1].Value.ToString();
                client_contact.Text = row.Cells[2].Value.ToString();
                client_cperson.Text = row.Cells[3].Value.ToString();
                client_address.Text = row.Cells[4].Value.ToString();

            }
        }

        private void Client_Load(object sender, EventArgs e)
        {
            colorManagement();
        }

        private void colorManagement()
        {
            clientAdd_btn.BackColor = Form1.appC;
            clientUpdate_btn.BackColor = Form1.appC;
            clientRemove_btn.BackColor = Form1.appC;
            clientClear_btn.BackColor = Form1.appC;
            client_filterLocation_button.BackColor = Form1.appC;

            dataGridView1.DefaultCellStyle.BackColor = Form1.appC;

            clientAdd_btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            clientAdd_btn.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            clientUpdate_btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            clientUpdate_btn.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            clientRemove_btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            clientRemove_btn.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            clientClear_btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            clientClear_btn.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            client_filterLocation_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            client_filterLocation_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

        }

        private void clientRemove_btn_Click(object sender, EventArgs e)
        {
            if (client_name.Text == "" || client_contact.Text == "" || getID == 0)
            {
                MessageBox.Show("Empty Fields. Select a data in the table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearFields();
            }
            else
            {
                if (Form1.role == "Admin")
                {
                    clientRemove();
                }
                else
                {
                    AdminCredentials credentials = new AdminCredentials();
                    credentials.ShowDialog();

                    if (AdminCredentials.isTrue)
                    {
                        clientRemove();
                        AdminCredentials.isTrue = false;
                    }
                }

            }
        }

        public void clientRemove()
        {
            if (MessageBox.Show("Are you sure you want to Remove Client ID: " + getID + "?", "Information Message",
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

                        string removeData = "DELETE FROM client WHERE id = @id";

                        using (MySqlCommand deleteD = new MySqlCommand(removeData, connect))
                        {
                            deleteD.Parameters.AddWithValue("@id", getID);

                            deleteD.ExecuteNonQuery();


                        }

                        string addDesc = "Client Removed: " + client_name.Text.Trim();

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

                        MessageBox.Show("Client removed successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        clearFields();
                        displayClientData();


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

        private void clientClear_btn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void clientUpdate_btn_Click(object sender, EventArgs e)
        {
            if (client_name.Text == "" || client_contact.Text == "" || client_cperson.Text == "" || client_address.Text == "" || getID == 0)
            {
                MessageBox.Show("Empty Fields. Select a data in the table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearFields();
            }
            else
            {
                if (Form1.role == "Admin")
                {
                    clientUpdate();
                }
                else
                {
                    AdminCredentials credentials = new AdminCredentials();
                    credentials.ShowDialog();

                    if (AdminCredentials.isTrue)
                    {
                        clientUpdate();
                        AdminCredentials.isTrue = false;
                    }
                }

            }
        }

        public void clientUpdate()
        {
            if (MessageBox.Show("Are you sure you want to Update Client ID: " + getID + "?", "Information Message",
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

                        string checkSuppUp = "SELECT * FROM client WHERE client_name = @client AND contact_info = @contact" +
                            " AND contact_person = @contactp AND address = @address ";

                        using (MySqlCommand cmd = new MySqlCommand(checkSuppUp, connect))
                        {
                            cmd.Parameters.AddWithValue("@client", client_name.Text.Trim());
                            cmd.Parameters.AddWithValue("@contact", client_contact.Text.Trim());
                            cmd.Parameters.AddWithValue("@contactp", client_cperson.Text.Trim());
                            cmd.Parameters.AddWithValue("@address", client_address.Text.Trim());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show("You haven't change anything on the selected Client.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {

                                string updateData = "UPDATE client SET client_name = @client, contact_info = @contact" +
                                    ", contact_person = @contactp, address = @address WHERE id = @id";

                                using (MySqlCommand updateD = new MySqlCommand(updateData, connect))
                                {
                                    updateD.Parameters.AddWithValue("@id", getID);
                                    updateD.Parameters.AddWithValue("@client", client_name.Text.Trim());
                                    updateD.Parameters.AddWithValue("@contact", client_contact.Text.Trim());
                                    updateD.Parameters.AddWithValue("@contactp", client_cperson.Text.Trim());
                                    updateD.Parameters.AddWithValue("@address", client_address.Text.Trim());

                                    updateD.ExecuteNonQuery();

                                }

                                string addDesc = "Client Updated: " + client_name.Text.Trim();

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

                                MessageBox.Show("Client Updated successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }




                        clearFields();
                        displayClientData();

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
            displayClientData();
            client_filterLocation.Text = "";
        }


        public static string clientfilter;
        private void client_filterLocation_button_Click(object sender, EventArgs e)
        {
            if (client_filterLocation.Text == "")
            {
                MessageBox.Show("Empty Fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                clientfilter = client_filterLocation.Text;

                ClientsData apData = new ClientsData();
                List<ClientsData> listData = apData.FilterClietntsData();

                dataGridView1.DataSource = listData;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);


            }
        }

        private void client_filterLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                client_filterLocation_button_Click(sender, e);
            }
        }
    }
}
