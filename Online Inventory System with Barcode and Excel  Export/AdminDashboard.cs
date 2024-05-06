using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NibsInventoryManagementSystem
{
    public partial class AdminDashboard : UserControl
    {
        public AdminDashboard()
        {
            InitializeComponent();
            displayTotalItems();
            displayTotalUsers();
            displayTotalClients();
            displayTotalSupplier();
            displayTotalUniqueProducts();
            displayTotalProductCategory();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            colorMangement();
            timer1.Start();
        }

        private void colorMangement()
        {
            panel2.BackColor = Form1.appC;
            panel4.BackColor = Form1.appC;
            panel5.BackColor = Form1.appC;
            panel6.BackColor = Form1.appC;
            panel7.BackColor = Form1.appC;
            panel8.BackColor = Form1.appC;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dashboard_time.Text = DateTime.UtcNow.AddHours(8).ToLongTimeString();
            dashboard_date.Text = DateTime.UtcNow.AddHours(8).ToLongDateString();
        }


        public void displayTotalItems()
        {
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();

                string selectData = $"SELECT SUM(stock) FROM products";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    object result = cmd.ExecuteScalar();


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (result != DBNull.Value)
                        {
                            float totalitems = Convert.ToSingle(result);
                            dashboard_totalItems.Text = totalitems.ToString("");
                        }
                    }
                }
            }

        }

        public void displayTotalUsers()
        {
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = $"SELECT COUNT(*) FROM users";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    object result = cmd.ExecuteScalar();

                    // int test = int.Parse(string.Format("{0}", result));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (result != DBNull.Value)
                        {
                            float allusers = Convert.ToSingle(result);
                            dashboard_allUser.Text = allusers.ToString("");
                        }
                    }
                }
            }

        }

        public void displayTotalClients()
        {
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = $"SELECT COUNT(*) FROM client";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    object result = cmd.ExecuteScalar();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (result != DBNull.Value)
                        {
                            float allusers = Convert.ToSingle(result);
                            dashboard_allClients.Text = allusers.ToString("");
                        }
                    }
                }
            }

        }

        public void displayTotalSupplier()
        {
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = $"SELECT COUNT(*) FROM supplier";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    object result = cmd.ExecuteScalar();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (result != DBNull.Value)
                        {
                            float allusers = Convert.ToSingle(result);
                            dashboard_allSupplier.Text = allusers.ToString("");
                        }
                    }
                }
            }

        }

        public void displayTotalUniqueProducts()
        {
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = $"SELECT COUNT(*) FROM products";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    object result = cmd.ExecuteScalar();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (result != DBNull.Value)
                        {
                            float allusers = Convert.ToSingle(result);
                            dashboard_totalUniqueItems.Text = allusers.ToString("");
                        }
                    }
                }
            }

        }

        public void displayTotalProductCategory()
        {
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = $"SELECT COUNT(*) FROM categories";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    object result = cmd.ExecuteScalar();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (result != DBNull.Value)
                        {
                            float allusers = Convert.ToSingle(result);
                            dashboard_totalCategory.Text = allusers.ToString("");
                        }
                    }
                }
            }

        }

    }
   
}
