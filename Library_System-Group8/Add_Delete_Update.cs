using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Library_System_Group8
{
    public partial class Add_Delete_Update : Form
    {
        public String Fullname { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        public Add_Delete_Update()
        {
            InitializeComponent();
        }

        private void Add_Delete_Update_Load(object sender, EventArgs e)
        {
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                Fullname = txtUsername.Text;
                Email = txtEmail.Text;
                Password = txtPassword.Text;
                this.Close();
            }
        }
    }
}
