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
    public partial class DeleteBook : Form
    {
        public DeleteBook()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        SqlCommand command;
        SqlDataAdapter dataAdapter;

        public string connectionString = @"Data Source=LAPTOP-PIV2U9BO\SQLSERVER;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private void DeleteBook_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);

            string select_query = "SELECT Book_Title from BOOKS";
            conn = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter(select_query, conn);
            conn.Open();

            DataSet ds = new DataSet();
            da.Fill(ds, "BOOKS");

            CBBookTitle.DisplayMember = "Book_Title";
            CBBookTitle.DataSource = ds.Tables["BOOKS"];

            conn.Close();

            CBBookTitle.SelectedIndex = -1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Check the user's response
                if (result == DialogResult.Yes)
                {
                    conn.Open();

                    string sqlDelete = $"DELETE FROM BOOKS WHERE Book_Title = '{CBBookTitle.Text}'";
                    command = new SqlCommand(sqlDelete, conn);

                    //Execute deleting data from the BOOKS table
                    dataAdapter = new SqlDataAdapter();
                    dataAdapter.DeleteCommand = command;
                    dataAdapter.DeleteCommand.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("Are you sure?", "Deleting...", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    this.Close();
                }
            }

            catch (Exception error)
            {
                //Display error message
                MessageBox.Show(error.Message, "Ocurring error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
