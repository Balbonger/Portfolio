using MySql.Data.MySqlClient;
using NibsInventoryManagementSystem.Properties;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NibsInventoryManagementSystem
{

    public partial class Form1 : Form
    {
        public static string username;

        public static string role;

        public static string gateway = "datasource = @@@@@@@@@@@@@@@@@@@@;port = @@@@;Initial Catalog = '@@@@@@@@@';username = @@@@@@@;password = @@@@@@@";

        public static string database = "use @@@@@@@@@@@@@";

        public static Color appC = (Color)Settings.Default["AppColor"];





        MySqlConnection connect = new MySqlConnection(gateway);

        public Form1()
        {
            InitializeComponent();

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void register_label_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();

            this.Hide();
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

        private void loginshowpass_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = loginshowpass_checkbox.Checked ? '\0' : '*';

        }

        private void login_button_Click(object sender, EventArgs e)
        {
            if (checkConnection())
            {
                try
                {
                    connect.Open();
                    IDGenerator();

                    MySqlCommand direct = new MySqlCommand(database, connect);
                    direct.ExecuteNonQuery();



                    string selectData = "SELECT COUNT(*) FROM users WHERE username = @usern AND password = @pass AND status = @status;";

                    using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@usern", login_username.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", login_password.Text.Trim());
                        cmd.Parameters.AddWithValue("@status", "Active");



                        int rowCount = (int)(long)(cmd.ExecuteScalar());



                        if (rowCount > 0)
                        {
                            string selectRole = "SELECT role FROM users WHERE username = @usern AND password = @pass";

                            using (MySqlCommand getRole = new MySqlCommand(selectRole, connect))
                            {
                                getRole.Parameters.AddWithValue("@usern", login_username.Text.Trim());
                                getRole.Parameters.AddWithValue("@pass", login_password.Text.Trim());

                                string userRole = getRole.ExecuteScalar() as string;

                                role = userRole;

                                string selectUsername = "SELECT username FROM users WHERE username = @usern AND password = @pass";
                                using (MySqlCommand getUname = new MySqlCommand(selectUsername, connect))
                                {
                                    getUname.Parameters.AddWithValue("@usern", login_username.Text.Trim());
                                    getUname.Parameters.AddWithValue("@pass", login_password.Text.Trim());

                                    username = getUname.ExecuteScalar() as string;



                                    string uu = Form1.username.Substring(0, 1).ToUpper() + Form1.username.Substring(1);

                                    string addDesc = "User Logged In: " + uu;

                                    string insertProduct = "INSERT INTO changelog (log_id, prod_name, destination, person_incharge, log_date, log_user) " +
                                        "VALUES (@tID, @prodName, @dest, @person, @date, @user)";

                                    using (MySqlCommand cmdd = new MySqlCommand(insertProduct, connect))
                                    {
                                        cmdd.Parameters.AddWithValue("@tID", idGen);
                                        cmdd.Parameters.AddWithValue("@prodName", addDesc.ToString());
                                        cmdd.Parameters.AddWithValue("@dest", "");
                                        cmdd.Parameters.AddWithValue("@person", "");

                                        DateTime today = DateTime.UtcNow.AddHours(8);
                                        cmdd.Parameters.AddWithValue("@date", today);
                                        cmdd.Parameters.AddWithValue("@user", uu);

                                        cmdd.ExecuteNonQuery();

                                    }

                                }



                                MessageBox.Show("Login Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (userRole == "Admin")
                                {
                                    MainForm mainform = new MainForm();
                                    mainform.Show();

                                    this.Hide();
                                }
                                else if (userRole == "User")
                                {
                                    UserForm userForm = new UserForm();
                                    userForm.Show();

                                    this.Hide();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect username/password or there's no admin approval.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            login_username.Text = "";
                            login_password.Text = "";
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
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            BackupRestore restore = new BackupRestore();
            restore.Show();

        }

        private void login_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login_button_Click(sender, e);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = appC;
            login_button.BackColor = appC;
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

        private void change_color_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();


            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = true;
            MyDialog.CustomColors = new int[] { 557149, 5599561 };


            // Sets the initial color select to the current text color.
            MyDialog.Color = (Color)Settings.Default["AppColor"];

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                Settings.Default["AppColor"] = MyDialog.Color;
                Settings.Default.Save();

                Application.Restart();
            }

        }
    }
}
