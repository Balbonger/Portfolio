using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics.Eventing.Reader;

namespace NibsInventoryManagementSystem
{
    public partial class RegisterForm : Form
    {
        MySqlConnection connect = new MySqlConnection(Form1.gateway);
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void login_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void signin_label_Click(object sender, EventArgs e)
        {
            Form1 loginform = new Form1();
            loginform.Show();

            this.Hide();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signup_button_Click(object sender, EventArgs e)
        {
            if(register_username.Text == "" || register_password.Text == "" || reregister_password.Text == "")
            {
                MessageBox.Show("Please fill all empty fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        string checkUsername = "SELECT * FROM users WHERE username = @usern";

                        using (MySqlCommand cmd = new MySqlCommand(checkUsername, connect))
                        {
                            cmd.Parameters.AddWithValue("@usern", register_username.Text.Trim());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if(table.Rows.Count > 0)
                            {
                                MessageBox.Show(register_username.Text.Trim() + " is already taken.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (register_password.Text.Length < 9)
                            {
                                MessageBox.Show("Invalid Password, Should be more than 9 characters.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (register_password.Text.Trim() != reregister_password.Text.Trim())
                            {
                                MessageBox.Show("Password does not match.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string inserData = "INSERT INTO users (username, password, role, status, date) " +
                                "VALUES (@usern, @pass, @role, @status, @date)";

                                using (MySqlCommand insertD = new MySqlCommand(inserData, connect))
                                {
                                    insertD.Parameters.AddWithValue("@usern", register_username.Text.Trim());
                                    insertD.Parameters.AddWithValue("@pass", register_password.Text.Trim());
                                    insertD.Parameters.AddWithValue("@role", "User");
                                    insertD.Parameters.AddWithValue("@status", "Approval");

                                    DateTime today = DateTime.UtcNow.AddHours(8);
                                    insertD.Parameters.AddWithValue("@date", today);

                                    insertD.ExecuteNonQuery();

                                    MessageBox.Show("Registered Succesfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Form1 loginform = new Form1();
                                    loginform.Show();

                                    this.Hide();
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
            if(connect.State == ConnectionState.Closed)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        private void registershowpass_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            register_password.PasswordChar = registershowpass_checkbox.Checked ? '\0' : '*';
            reregister_password.PasswordChar = registershowpass_checkbox.Checked ? '\0' : '*';
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            colorManagement();
        }

        public void colorManagement()
        {
            this.BackColor = Form1.appC;
            signup_button.BackColor = Form1.appC;
        }
    }
}
