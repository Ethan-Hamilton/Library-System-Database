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
    public partial class AddBook : Form
    {
        public AddBook()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlCommand command;

        public string connectionString = @"Data Source=LAPTOP-PIV2U9BO\SQLSERVER;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private void AddBook_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Check the user's response
                if (result == DialogResult.Yes)
                {
                    conn.Open();

                    // Use parameterized query to prevent SQL injection
                    string insertSql = "INSERT INTO BOOKS (ISBN_Number, Book_Title, Book_Author, PublicationYear) " +
                                       "VALUES (@ISBN, @Title, @Author, @PublicationYear)";

                    command = new SqlCommand(insertSql, conn);

                    // Replace these placeholders with the actual parameter names
                    command.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                    command.Parameters.AddWithValue("@Title", txtTitle.Text);
                    command.Parameters.AddWithValue("@Author", txtAuthor.Text);
                    command.Parameters.AddWithValue("@PublicationYear", txtPublicationYear.Text);

                    command.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("Are you sure?", "Adding Book to Catalogue...", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }   
    }
}
