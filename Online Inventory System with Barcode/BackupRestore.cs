using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NibsInventoryManagementSystem
{
    public partial class BackupRestore : Form
    {
        public BackupRestore()
        {
            InitializeComponent();
            restoreData_location.ReadOnly = true;
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

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit the Restore tab? ", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void restoreData_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog searchBak = new OpenFileDialog();

            searchBak.InitialDirectory = @"C:\NibsInventoryBackup\";
            searchBak.Title = "Browse database recovery data";

            searchBak.CheckFileExists = true;
            searchBak.CheckPathExists = true;

            searchBak.DefaultExt = "SQL";
            searchBak.Filter = "Text files (*.sql)|*.sql";
            searchBak.FilterIndex = 2;
            searchBak.RestoreDirectory = true;

            searchBak.ReadOnlyChecked = true;
            searchBak.ShowReadOnly = true;

            if (searchBak.ShowDialog() == DialogResult.OK)
            {
                restoreData_location.Text = searchBak.FileName;
            }
        }

        private void restoreData_location_TextChanged(object sender, EventArgs e)
        {

        }

        public void restoreData()
        {

            try
            {
                if (restoreData_location.Text == "")
                {
                    MessageBox.Show("You didn't select any recovery file. ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    using (MySqlConnection connection = new MySqlConnection(Form1.gateway))
                    {
                        string rpath = restoreData_location.Text.ToString();
                        using (MySqlCommand db = new MySqlCommand(Form1.database, connection))
                        {
                            using (MySqlCommand cmd = new MySqlCommand())
                            {
                                using (MySqlBackup mb = new MySqlBackup(cmd))
                                {
                                    cmd.Connection = connection;
                                    connection.Open();
                                    mb.ImportFromFile(rpath);
                                    connection.Close();
                                }
                            }
                        }

                    }
                   

                    if (MessageBox.Show("DATABASE RECOVERED Successfully! Restart the application.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information)== DialogResult.OK)
                    {
                        this.Hide();
                        Application.Restart();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void restoreData_restore_Click(object sender, EventArgs e)
        {
            
            restoreData();
        }
        
  
        
  

        private void recoveryPassword1_Load(object sender, EventArgs e)
        {
            
        }

        private void restoreData_cancel_Click(object sender, EventArgs e)
        {
            restoreData_location.Text = "";
        }


        private void restoreData_manualBackup_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to create a manual backup file of the current database?", "Question Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ManageDatabase md = new ManageDatabase();
                md.createBackup();
            }
            else
            {

            }
        }

        private void BackupRestore_Load(object sender, EventArgs e)
        {
            colorManagement();
        }

        public void colorManagement()
        {
            panel1.BackColor = Form1.appC;
            restoreData_manualBackup.BackColor = Form1.appC;
            restoreData_restore.BackColor = Form1.appC;
        }

    }
}
