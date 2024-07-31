using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Library_System_Group8
{
    public partial class MemberSignUp : Form
    {
        public MemberSignUp()
        {
            InitializeComponent();
        }

        SqlConnection conn;        
        SqlCommand command;    
        SqlDataAdapter dataAdapter;
        
        public string connectionString = @"Data Source=LAPTOP-PIV2U9BO\SQLSERVER;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private bool IsValidEmail(string email)
        {
            // Use a regular expression to validate the email format
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {   
            this.Close();      
        }

        private void MemberSignIn_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);
        }

        private void btnEnterDetails_Click(object sender, EventArgs e)
        {
            try
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
                    string insertSql = $"INSERT INTO MEMBERS(fullname,email,password) VALUES ('{txtUsername.Text}','{txtEmail.Text}','{txtPassword.Text}')";
                    command = new SqlCommand(insertSql, conn);
                    dataAdapter = new SqlDataAdapter();
                    dataAdapter.InsertCommand = command;
                    dataAdapter.InsertCommand.ExecuteNonQuery();

                    MessageBox.Show("Details have been Entered!");
                    this.Close();
                    conn.Close();
                }
            }
            catch
            {
                MessageBox.Show("Operation was cancelled");
            }
        }
    }
}
