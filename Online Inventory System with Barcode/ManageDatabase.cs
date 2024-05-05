using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows.Forms;

namespace NibsInventoryManagementSystem
{
    internal class ManageDatabase
    {

        public void createBackup()
        {
            string path = @"C:\NibsInventoryBackup\ManualBackup";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Directory created successfully.");
            }


            DateTime d = DateTime.Now;
            string dd = d.Day + "-" + d.Month + "-" + d.Year;

            string backup = "C:\\NibsInventoryBackup\\ManualBackup\\Nibs_IMS_Manual_Backup_" + dd + ".sql";


            try
            {
                using (MySqlConnection connection = new MySqlConnection(Form1.gateway))
                {
                    using (MySqlCommand equip = new MySqlCommand(Form1.database, connection))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = connection;
                                connection.Open();
                                mb.ExportToFile(backup);
                                connection.Close();
                            }
                        }
                    }
                }



                MessageBox.Show("Backup created successfully! You can locate the file in Drive C, NibsInventoryBackup folder, and Manual Backup folder", 
                    "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }


        }

        public void createBackupAuto()
        {
            string path = @"C:\NibsInventoryBackup";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Directory created successfully.");
            }


            DateTime d = DateTime.Now;
            string dd = d.Day + "-" + d.Month + "-" + d.Year;

            string backup = "C:\\NibsInventoryBackup\\Nibs_IMS_Backup_" + dd + ".sql";


            try
            {
                using (MySqlConnection connection = new MySqlConnection(Form1.gateway))
                {
                    using (MySqlCommand equip = new MySqlCommand(Form1.database, connection))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = connection;
                                connection.Open();
                                mb.ExportToFile(backup);

                                connection.Close();
                            }
                        }
                    }
                }

               // Console.WriteLine("Database Backed up successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }

        }


        /* Local DB SQL only!!
        public void createDatabase()
        {
            MySqlConnection connection = new MySqlConnection ();

            using (connection)
            {
                try
                {

                    connection.Open();

                    string check = @"Select * FROM sys.databases where name = 'C:\USERS\JAMES\DOCUMENTS\INVENTORY.MDF'";

                    MySqlCommand sCheck = new MySqlCommand(check, connection);
                    object r = sCheck.ExecuteScalar();


                        if (r ==null)
                        {
                            
                            string sql = string.Format(@"
                            CREATE DATABASE
                            [C:\USERS\JAMES\DOCUMENTS\INVENTORY.MDF]
                            ");

                            MySqlCommand command = new MySqlCommand(sql, connection);
                            command.ExecuteNonQuery();

                            string use = "USE " +
                                @"[C:\USERS\JAMES\DOCUMENTS\INVENTORY.MDF]";

                            string createTables = "CREATE TABLE users" +
                                                        "(" +
                                                            "id INT PRIMARY KEY IDENTITY(1,1), " +
                                                            "username VARCHAR(MAX) NULL, " +
                                                            "password VARCHAR(MAX) NULL, " +
                                                            "role VARCHAR(MAX) NULL, " +
                                                            "status VARCHAR(MAX) NULL, " +
                                                            "date DATE NULL " +
                                                        ")" +

                                                        "CREATE TABLE categories " +
                                                        "(" +
                                                            "id INT PRIMARY KEY IDENTITY(1,1), " +
                                                            "category VARCHAR(MAX) NULL, " +
                                                            "date DATE NULL " +
                                                        ")" +

                                                        "CREATE TABLE products " +
                                                        "(" +
                                                            "id INT PRIMARY KEY IDENTITY(1,1), " +
                                                            "prod_id VARCHAR(MAX) NULL, " +
                                                            "prod_name VARCHAR(MAX) NULL, " +
                                                            "category VARCHAR(MAX) NULL, " +
                                                            "price FLOAT NULL, " +
                                                            "stock FLOAT NULL, " +
                                                            "unit VARCHAR(30) NULL, " +
                                                            "image_path VARCHAR(MAX) NULL, " +
                                                            "status VARCHAR(MAX) NULL, " +
                                                            "date_insert DATE NULL, " +
                                                            "prod_location VARCHAR(MAX) NULL, " +
                                                            "prod_personnel VARCHAR(MAX) NULL " +
                                                        ")" +

                                                        "CREATE TABLE orders " +
                                                        "(" +
                                                            "id INT PRIMARY KEY IDENTITY(1,1), " +
                                                            "prod_id VARCHAR(MAX) NULL, " +
                                                            "prod_name VARCHAR(MAX) NULL, " +
                                                            "category VARCHAR(MAX) NULL, " +
                                                            "qty INT NULL, " +
                                                            "order_date DATE NULL, " +
                                                            "transac_id INT NULL, " +
                                                            "unit VARCHAR(30) NULL " +
                                                        ")" +

                                                        "CREATE TABLE changelog " +
                                                        "(" +
                                                            "id INT PRIMARY KEY IDENTITY(1,1), " +
                                                            "log_id INT NULL, " +
                                                            "prod_name VARCHAR(MAX) NULL, " +
                                                            "destination VARCHAR(MAX) NULL, " +
                                                            "person_incharge VARCHAR(MAX) NULL, " +
                                                            "log_date DATE NULL, " +
                                                            "log_user VARCHAR(MAX) NULL, " +
                                                        ")";

                            MySqlCommand command1 = new MySqlCommand(use, connection);
                            MySqlCommand command2 = new MySqlCommand(createTables, connection);


                            command1.ExecuteNonQuery();
                            command2.ExecuteNonQuery();

                            string adminCheck = "SELECT COUNT (*) FROM users WHERE username = 'admin'";
                            using (MySqlCommand command3 = new MySqlCommand(adminCheck, connection))
                            {
                                int rowCount = (int)command3.ExecuteScalar();

                                if (rowCount == 0)
                                {
                                    string createAdmin = "INSERT INTO users(username, password, role, status, date)" +
                                                         "VALUES ('admin', 'admin1234', 'Admin', 'Active', @date)";

                                    using (MySqlCommand command4 = new MySqlCommand(createAdmin, connection))
                                    {
                                        DateTime today = DateTime.Today;
                                        command4.Parameters.AddWithValue("@date", today);

                                        command4.ExecuteNonQuery();
                                    }
                            
                                }
                            
                                else
                                {
                                    Console.WriteLine("admin already exists.");
                                }
                            
                            }
                            
                            if (MessageBox.Show("Database created successfully. Application restart needed ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                Application.Restart();
                            }

                        

                        }
                        else
                        {
                            MessageBox.Show("Database already exists.", "Infomation Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    connection.Close();
                }

            }
        }
        */

    }
}
