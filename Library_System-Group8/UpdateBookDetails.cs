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

namespace Library_System_Group8
{
    public partial class UpdateBookDetails : Form
    {
        public UpdateBookDetails()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlDataAdapter adapt;   //Dont use in listbox
        SqlCommand command;
    
        public string connectionString = @"Data Source=LAPTOP-PIV2U9BO\SQLSERVER;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private void UpdateBookDetails_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);

            string select_query = "SELECT Book_Title from Books";
            conn = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter(select_query, conn);
            conn.Open();

            DataSet ds = new DataSet();
            da.Fill(ds, "Books");

            CBBookTitle.DisplayMember = "Book_Title";
            CBBookTitle.DataSource = ds.Tables["Books"]; 

            conn.Close();

            CBBookTitle.SelectedIndex = -1;
        }
        private void btnUpdateBook_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                conn.Open();

                string sqlUpdate = "UPDATE BOOKS SET PublicationYear = @year WHERE Book_Title = @title";
                command = new SqlCommand(sqlUpdate, conn);

                //Execute updating the ITEMS table
                command.Parameters.AddWithValue("@year", txtPublicationYear.Text);

                command.Parameters.AddWithValue("@title", CBBookTitle.Text);

                // Initialize the dataAdapter variable
                adapt = new SqlDataAdapter(command);
                adapt.UpdateCommand = command;
                adapt.UpdateCommand.ExecuteNonQuery();

                //Display confirmation message
                MessageBox.Show("Are you sure?", "Updating...", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                this.Close();

                conn.Close();
            }
        }
    }
}
