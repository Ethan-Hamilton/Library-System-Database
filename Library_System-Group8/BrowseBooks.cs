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
    public partial class BrowseBooks : Form
    {
        
        public BrowseBooks()
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
            string sql = "Select * FROM Books";
            command = new SqlCommand(sql, conn);

            dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;

            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Books");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Books";

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
            dataAdapter.Fill(ds, "Books");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Books";

            conn.Close();
        }
        private void BrowseBooks_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "Select * FROM Books";
            command = new SqlCommand(sql, conn);

            dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;

            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Books");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Books";

            conn.Close();

            hScrollBarYear.Minimum = 1950;
            hScrollBarYear.Maximum = 2023;
            hScrollBarYear.SmallChange = 10;
            hScrollBarYear.LargeChange = 10;

            string select_query = "SELECT Book_Title from Books";
            conn = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter(select_query, conn);
            conn.Open();

            DataSet dss = new DataSet();
            da.Fill(dss, "Books");

            CBCheckOut.DisplayMember = "Book_Title";
            CBCheckOut.DataSource = dss.Tables["Books"];

            conn.Close();

            CBCheckOut.SelectedIndex = -1;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 200;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Check the user's response
                if (result == DialogResult.Yes)
                {
                    conn.Open();

                    // Retrieve the corresponding Book_ID from the BOOKS table
                    int selectedBookID = 0;
                    string sqlSelect = $"SELECT Book_ID FROM BOOKS WHERE Book_Title = @BookTitle";

                    using (SqlCommand cmd = new SqlCommand(sqlSelect, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookTitle", CBCheckOut.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedBookID = Convert.ToInt32(reader.GetValue(0));
                            }
                        }
                    }

                    Member popupform = new Member();

                    int selectedMemberID = 0;
                    string sqlSelectMember = $"SELECT Member_ID FROM MEMBERS WHERE Fullname = @MemberLogin";

                    using (SqlCommand cmdMember = new SqlCommand(sqlSelectMember, conn))
                    {
                        cmdMember.Parameters.AddWithValue("@MemberLogin", txtUsername.Text);

                        using (SqlDataReader reader = cmdMember.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedMemberID = Convert.ToInt32(reader.GetValue(0));
                            }
                        }
                    }

                    // Insert values into the CHECKOUT_CHECKIN table
                    string sqlInsert = $"INSERT INTO CHECKOUT_CHECKIN (Member_ID, Book_ID, CheckOut_Timestamp, CheckIn_Timestamp) VALUES (@MemberID, @BookID, @CheckOut, @CheckIn)";

                    DateTime selectedDate = dateTimePicker1.Value.Date; // Extract only the date part
                    DateTime date20DaysAhead = selectedDate.AddDays(20);

                    using (SqlCommand command = new SqlCommand(sqlInsert, conn))
                    {
                        command.Parameters.AddWithValue("@MemberID", selectedMemberID);
                        command.Parameters.AddWithValue("@BookID", selectedBookID);
                        command.Parameters.AddWithValue("@CheckOut", selectedDate);
                        command.Parameters.AddWithValue("@CheckIn", date20DaysAhead);
                        command.ExecuteNonQuery();
                    }

                    conn.Close();

                    lblCheckout.Text = "Check-Out Successful!\n\nYou took out: " + CBCheckOut.Text + ". \nDate Checked Out: " + selectedDate.ToString("yyyy-MM-dd") + "\nBook/s to be returned: " + date20DaysAhead.ToString("yyyy-MM-dd");
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions here
                MessageBox.Show("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions here
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close(); // Make sure to close the connection in case of exceptions
            }
        }

            private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            displayData($"SELECT * FROM Books WHERE ISBN_Number LIKE '%" + txtSearch.Text + "%' OR Book_Title LIKE '%" + txtSearch.Text + "%' OR Book_Author LIKE '%" + txtSearch.Text + "%' OR PublicationYear LIKE '%" + txtSearch.Text + "%'");

        }

        private void hScrollBarYear_Scroll(object sender, ScrollEventArgs e)
        {
            lblYears.Text = hScrollBarYear.Value.ToString();
            displayData("SELECT * FROM Books WHERE PublicationYear < " + hScrollBarYear.Value.ToString() + "");
        }
    }
}

