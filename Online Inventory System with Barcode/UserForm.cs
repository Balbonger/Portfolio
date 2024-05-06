using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NibsInventoryManagementSystem
{
    public partial class UserForm : Form
    {
        MySqlConnection connect = new MySqlConnection(Form1.gateway);

        Timer t = new Timer();
        Point currPos;
        Point oldPos;

        public UserForm()
        {
            InitializeComponent();
            displayUsername();

            t.Start();
        }

        public void displayUsername()
        {
            string username = Form1.username.Substring(0, 1).ToUpper() + Form1.username.Substring(1);

            usernamebut.Text = username;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the application? ", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ManageDatabase db = new ManageDatabase();
                db.createBackupAuto();

                Application.Exit();
            }
        }

        private void maximize_button_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;

            }
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        public Point mouseLocation;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);

                Location = mousePose;
            }
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Logout? ", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 loginform = new Form1();
                loginform.Show();

                ManageDatabase db = new ManageDatabase();
                db.createBackupAuto();
                recordLogout();

                this.Hide();
            }
        }

        private void dashboard_button_Click(object sender, EventArgs e)
        {

            adminDashboard1.Visible = true;

            adminAddCategories1.Visible = false;
            adminAddProducts1.Visible = false;
            manageInventory1.Visible = false;
            supplier1.Visible = false;
            client1.Visible = false;
            activityLog1.Visible = false;

            AdminDashboard adform = adminDashboard1 as AdminDashboard;
            if (adform != null)
            {
                adform.refreshData();
            }

            adform.displayTotalUsers();
            adform.displayTotalItems();
            adform.displayTotalUsers();
            adform.displayTotalClients();
            adform.displayTotalSupplier();
            adform.displayTotalUniqueProducts();
            adform.displayTotalProductCategory();
        }

        private void addcategory_button_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            adminAddCategories1.Visible = true;
            adminAddProducts1.Visible = false;
            manageInventory1.Visible = false;
            supplier1.Visible = false;
            client1.Visible = false;
            activityLog1.Visible = false;

            AdminAddCategories adform = adminAddCategories1 as AdminAddCategories;
            if (adform != null)
            {
                adform.refreshData();
            }

            adform.displayCategoriesData();

        }

        private void addproduct_button_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            adminAddCategories1.Visible = false;
            adminAddProducts1.Visible = true;
            manageInventory1.Visible = false;
            supplier1.Visible = false;
            client1.Visible = false;
            activityLog1.Visible = false;

            AdminAddProducts adform = adminAddProducts1 as AdminAddProducts;
            if (adform != null)
            {
                adform.refreshData();
            }

            adform.displayAllProducts();
            adform.displayCategories();

        }

        private void customer_button_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            adminAddCategories1.Visible = false;
            adminAddProducts1.Visible = false;
            manageInventory1.Visible = true;
            supplier1.Visible = false;
            client1.Visible = false;
            activityLog1.Visible = false;

            ManageInventory adform = manageInventory1 as ManageInventory;
            if (adform != null)
            {
                adform.refreshData();
            }

            adform.displayAllAvailableProducts();
            adform.displayTotalItems();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            colorManagement();

            checkActivity();
        }

        public void recordLogout()
        {
            try
            {
                IDGenerator();
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string uu = Form1.username.Substring(0, 1).ToUpper() + Form1.username.Substring(1);

                string addDesc = "User Logged Out: " + uu;

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
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                connect.Close();
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

        public void colorManagement()
        {
            panel2.BackColor = Form1.appC;
            dashboard_button.BackColor = Form1.appC;
            addcategory_button.BackColor = Form1.appC;
            addproduct_button.BackColor = Form1.appC;
            customer_button.BackColor = Form1.appC;
            logout_button.BackColor = Form1.appC;
            client_button.BackColor = Form1.appC;
            supplier_button.BackColor = Form1.appC;
            activity_button.BackColor = Form1.appC;

            dashboard_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            dashboard_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            addcategory_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            addcategory_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            addproduct_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            addproduct_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            customer_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            customer_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            logout_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            logout_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            client_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            client_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            supplier_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            supplier_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);

            activity_button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Form1.appC);
            activity_button.FlatAppearance.MouseDownBackColor = ControlPaint.Light(Form1.appC);
        }

        public void checkActivity()
        {
            currPos = Cursor.Position;
            t.Interval = (30 * 4000 * 60);
            t.Enabled = true;

            t.Tick += new EventHandler(timer1_Tick);

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                currPos = Cursor.Position;
                if (oldPos == currPos)
                {
                    t.Stop();
                    var res = MessageBox.Show("You've been logged out automatically due to inactivity. ", "Alert");
                    if (res == DialogResult.OK)
                    {
                        this.Hide();

                        ManageDatabase db = new ManageDatabase();
                        db.createBackupAuto();
                        recordLogout();

                        Application.Restart();
                    }
                }
                oldPos = currPos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void supplier_button_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            adminAddCategories1.Visible = false;
            adminAddProducts1.Visible = false;
            manageInventory1.Visible = false;
            supplier1.Visible = true;
            client1.Visible = false;
            activityLog1.Visible = false;

            AddSupplier adform = supplier1 as AddSupplier;
            if (adform != null)
            {
                adform.refreshData();
            }

            adform.displaySupplierData();
        }

        private void client_button_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            adminAddCategories1.Visible = false;
            adminAddProducts1.Visible = false;
            manageInventory1.Visible = false;
            supplier1.Visible = false;
            client1.Visible = true;
            activityLog1.Visible = false;

            AddClient adform = client1 as AddClient;
            if (adform != null)
            {
                adform.refreshData();
            }

            adform.displayClientData();
        }

        private void activity_button_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            adminAddCategories1.Visible = false;
            adminAddProducts1.Visible = false;
            manageInventory1.Visible = false;
            supplier1.Visible = false;
            client1.Visible = false;
            activityLog1.Visible = true;

            ActivityLog adform = activityLog1 as ActivityLog;
            if (adform != null)
            {
                adform.refreshData();
            }

            adform.displayActivityLog();
        }
    }
}
