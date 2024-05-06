using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NibsInventoryManagementSystem
{
    public partial class RecoveryPassword : UserControl
    {
        public RecoveryPassword()
        {
            InitializeComponent();
        }

        private void restore_passConfirm_Click(object sender, EventArgs e)
        {
            if(restore_password.Text == "")
            {
                MessageBox.Show("Empty fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (restore_password.Text =="restore2024")
                {
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect Password.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void restore_passClear_Click(object sender, EventArgs e)
        {
            restore_password.Text = "";
        }

        private void restore_password_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                restore_passConfirm_Click((object)sender, e);

            }
        }

        private void RecoveryPassword_Load(object sender, EventArgs e)
        {
            colorManagement();
        }

        public void colorManagement()
        {
            restore_passConfirm.BackColor = Form1.appC;
        }
    }
}
