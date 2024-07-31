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
    public partial class ViewBooks : Form
    {
        public ViewBooks()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlDataAdapter dataAdapt;   //Dont use in listbox
        SqlCommand command;
        

        public string connectionString = @"Data Source=LAPTOP-PIV2U9BO\SQLSERVER;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public void displayData()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "Select * FROM BOOKS";
            command = new SqlCommand(sql, conn);

            dataAdapt = new SqlDataAdapter();
            dataAdapt.SelectCommand = command;

            DataSet ds = new DataSet();
            dataAdapt.Fill(ds, "BOOKS");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "BOOKS";

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

            dataAdapt = new SqlDataAdapter();
            dataAdapt.SelectCommand = command;

            DataSet ds = new DataSet();
            dataAdapt.Fill(ds, "BOOKS");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "BOOKS";

            conn.Close();
        }
        private void ViewBooks_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);
            displayData();

            hScrollBarYear.Minimum = 1950;
            hScrollBarYear.Maximum = 2023;
            hScrollBarYear.SmallChange = 10;
            hScrollBarYear.LargeChange = 10;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 180;
            dataGridView1.Columns[4].Width = 180;        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            displayData($"SELECT * FROM BOOKS WHERE ISBN_Number LIKE '%" + txtSearch.Text+ "%' OR Book_Title LIKE '%" + txtSearch.Text + "%' OR Book_Author LIKE '%" + txtSearch.Text + "%' OR PublicationYear LIKE '%" + txtSearch.Text + "%'");
        }

        private void txtSearchTitle_TextChanged(object sender, EventArgs e)
        {
            displayData("SELECT * FROM BOOKS WHERE Book_Title LIKE'%" + txtSearchTitle.Text + "%'");
        }

        private void txtAuthor_TextChanged(object sender, EventArgs e)
        {
            displayData("SELECT * FROM BOOKS WHERE Book_Author LIKE'%" + txtAuthor.Text + "%'");
        }

        private void txtISBN_TextChanged(object sender, EventArgs e)
        {
            displayData("SELECT * FROM BOOKS WHERE ISBN_Number LIKE'%" + txtISBN.Text + "%'");
        }

        private void hScrollBarYear_Scroll(object sender, ScrollEventArgs e)
        {
            lblYears.Text = hScrollBarYear.Value.ToString();
            displayData("SELECT * FROM BOOKS WHERE PublicationYear < " + hScrollBarYear.Value.ToString() + "");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
