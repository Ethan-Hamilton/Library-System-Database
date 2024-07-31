using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Library_System_Group8
{
    public partial class ChangeMemberDetails : Form
    {
        public ChangeMemberDetails()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        SqlCommand command;
        SqlDataAdapter adapt;

        public string connectionString = @"Data Source=LAPTOP-PIV2U9BO\SQLSERVER;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private bool IsValidEmail(string email)
        {
            // Use a regular expression to validate the email format
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }


        private void ChangeMemberDetails_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method if email is not valid
            }

            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                conn.Open();

                string sqlUpdate = "UPDATE MEMBERS SET Email = @email, Password = @password WHERE Fullname = @fullname";
                command = new SqlCommand(sqlUpdate, conn);

                //Execute updating the ITEMS table
                command.Parameters.AddWithValue("@email", txtEmail.Text);
                command.Parameters.AddWithValue("@password", txtPassword.Text);
                command.Parameters.AddWithValue("@fullname", txtUsername.Text);

                // Initialize the dataAdapter variable
                adapt = new SqlDataAdapter(command);
                adapt.UpdateCommand = command;
                adapt.UpdateCommand.ExecuteNonQuery();

                //Display confirmation message
                MessageBox.Show("Are you sure?", "Updating...", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                conn.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
