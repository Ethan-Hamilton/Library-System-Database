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
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        SqlCommand command;
        SqlDataAdapter dataAdapter;

        public string connectionString = @"Data Source=LAPTOP-PIV2U9BO\SQLSERVER;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void displayData()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "Select * FROM CHECKOUT_CHECKIN";
            command = new SqlCommand(sql, conn);

            dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;

            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "CHECKOUT_CHECKIN");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "CHECKOUT_CHECKIN";

            conn.Close();
        }

        public void displayData(string sqlStatement)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = sqlStatement;
            command = new SqlCommand(sql, conn);

            dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;

            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "CHECKOUT_CHECKIN");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "CHECKOUT_CHECKIN";

            conn.Close();
        }
        private void Reports_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);

            displayData();

            string select_query = "SELECT Check_ID from CHECKOUT_CHECKIN";
            conn = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter(select_query, conn);
            conn.Open();

            DataSet ds = new DataSet();
            da.Fill(ds, "CHECKOUT_CHECKIN");

            CBDelete.DisplayMember = "Check_ID";
            CBDelete.DataSource = ds.Tables["CHECKOUT_CHECKIN"];

            conn.Close();

            CBDelete.SelectedIndex = -1;

            
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 120;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            displayData($"SELECT * FROM CHECKOUT_CHECKIN WHERE Check_ID LIKE '%" + txtSearch.Text + "%' OR Member_ID LIKE '%" + txtSearch.Text + "%' OR Book_ID LIKE '%" + txtSearch.Text + "%' OR CheckOut_Timestamp LIKE '%" + txtSearch.Text + "%' OR CheckIn_Timestamp LIKE '%" + txtSearch.Text + "%'");
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Check the user's response
                if (result == DialogResult.Yes)
                {
                    conn.Open();

                    string sqlDelete = $"DELETE FROM CHECKOUT_CHECKIN WHERE Check_ID = '{CBDelete.Text}'";
                    command = new SqlCommand(sqlDelete, conn);

                    //Execute deleting data from the BOOKS table
                    dataAdapter = new SqlDataAdapter();
                    dataAdapter.DeleteCommand = command;
                    dataAdapter.DeleteCommand.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("Are you sure?", "Deleting...", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    displayData();
                }
            }

            catch (Exception error)
            {
                //Display error message
                MessageBox.Show(error.Message, "Ocurring error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CBDelete_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
