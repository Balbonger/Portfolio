using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace NibsInventoryManagementSystem
{
    public partial class AdminCredentials : Form
    {
        MySqlConnection connect = new MySqlConnection (Form1.gateway);


        public AdminCredentials()
        {
            InitializeComponent();
        }



        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void admin_clear_Click(object sender, EventArgs e)
        {
            admin_password.Text = "";
        }

        public static bool isTrue;

        private void admin_proceed_Click(object sender, EventArgs e)
        {
            try
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();

                string passCheck = "SELECT COUNT(*) FROM users WHERE role = @role AND password = @pass AND status = @status";

                using (MySqlCommand inquire = new MySqlCommand(passCheck, connect))
                {
                    inquire.Parameters.AddWithValue("@role", "Admin");
                    inquire.Parameters.AddWithValue("@pass", admin_password.Text.Trim());
                    inquire.Parameters.AddWithValue("@status", "Active");

                    int rowCount = (int)(long)(inquire.ExecuteScalar());

                    if (rowCount > 0)
                    {
                        isTrue = true;

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Data removal failed. Incorrect Credentials ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }



        }

        private void AdminCredentials_Load(object sender, EventArgs e)
        {
            colorManagement();
        }

        public void colorManagement()
        {
            panel1.BackColor = Form1.appC;
            admin_proceed.BackColor = Form1.appC;

            admin_proceed.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            admin_proceed.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            admin_clear.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            admin_clear.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);
        }

        private void admin_password_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                admin_proceed_Click(sender, e);
            }
        }
    }
}
