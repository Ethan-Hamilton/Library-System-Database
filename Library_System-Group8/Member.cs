using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Library_System_Group8
{
    public partial class Member : Form
    {
        public Member()
        {
            InitializeComponent();
        }
        SqlConnection conn;         
        SqlCommand command;        
        SqlDataReader dataReader;


        public string connectionString = @"Data Source=LAPTOP-PIV2U9BO\SQLSERVER;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            MemberSignUp popupform = new MemberSignUp();
            popupform.Show();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                //This demostrates how to utilize the errorProvider component
                //As parameters, we first bind it to a component, then set an error message
                errorProviderUsername.SetError(txtUsername, "Please Enter Correct Username");
              
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtUsername.Focus();
            }
            else if (txtPassword.Text == "")
            {
                //This demostrates how to utilize the errorProvider component
                //As parameters, we first bind it to a component, then set an error message
                errorProviderPassword.SetError(txtPassword, "Please Enter Correct Password");
                
                txtUsername.Focus();
            }
            else
            {
                try
                {
                    //This try-block shows how to iterate through a table and test records against a certain value
                    conn = new SqlConnection(connectionString);
                    conn.Open();                        //Opens the connection
                    string sql = "SELECT * FROM MEMBERS"; //Formulate the SQL statement
                    command = new SqlCommand(sql, conn);//Initiate the command object
                    dataReader = command.ExecuteReader();   //Use the dataReader to execute the command object

                    while (dataReader.Read())
                    {
                        if ((txtUsername.Text == dataReader.GetValue(1).ToString()) && (txtPassword.Text == dataReader.GetValue(3).ToString()))
                        {
                            MemberMainPage adminForm = new MemberMainPage();  //Initialise a new instance of the second Form (Form2)
                            this.Hide();                    //Hide our current form
                            adminForm.ShowDialog();         //Show Dialog since we want to pass values (connectionString) from this form to other forms. Note that the new form displayed is a stand-alone form (not contained within a MDI container)
                        }
                        if((txtUsername.Text != dataReader.GetValue(1).ToString()) && (txtPassword.Text != dataReader.GetValue(3).ToString()))
                        {
                            MessageBox.Show("Incorrect Username or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPassword.Clear(); // Clear the password field
                            txtUsername.Clear();
                            txtUsername.Focus();
                            return;
                        }
                    }
                    conn.Close();                           //Close the connection

                    this.Close();
                }
                catch (Exception er)
                {
                    MessageBox.Show("The following error occured:\t" + er.ToString());  //Catch the exception should one occurr
                }
            }
            
        }

        private void Member_Load(object sender, EventArgs e)
        {
            
        }
    }
}
