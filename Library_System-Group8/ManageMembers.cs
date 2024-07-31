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
    public partial class ManageMembers : Form
    {
        public ManageMembers()
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
            string sql = "Select * FROM MEMBERS";
            command = new SqlCommand(sql, conn);

            dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;

            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "MEMBERS");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "MEMBERS";

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
            dataAdapter.Fill(ds, "MEMBERS");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "MEMBERS";

            conn.Close();

        }
        private void ManageMembers_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);

            displayData();

            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 150;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            displayData($"SELECT * FROM MEMBERS WHERE Fullname LIKE '%" + txtSearch.Text + "%' OR Email LIKE '%" + txtSearch.Text + "%' OR Password LIKE '%" + txtSearch.Text + "%'");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add_Delete_Update popupForm = new Add_Delete_Update();
            popupForm.ShowDialog();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = $"INSERT INTO MEMBERS(Fullname,Email,Password) VALUES('{popupForm.Fullname}','{popupForm.Email}','{popupForm.Password}')"; //Have to include dollar sign for format
            command = new SqlCommand(sql, conn);

            dataAdapter = new SqlDataAdapter();
            dataAdapter.InsertCommand = command;
            dataAdapter.InsertCommand.ExecuteNonQuery();

            displayData();
            conn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Add_Delete_Update popupForm = new Add_Delete_Update();
            popupForm.ShowDialog();

            if (!(conn.State == ConnectionState.Open))//Another way of doing ConnectionState
            {
                conn.Open();

            }
            string sql = "DELETE FROM MEMBERS WHERE Fullname = @user_name";
            command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@user_name", popupForm.Fullname);
            command.ExecuteNonQuery();

            conn.Close();
            displayData(); //Call method to refresh data
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Add_Delete_Update popupForm = new Add_Delete_Update();
            popupForm.ShowDialog();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();

            }
            string sql = "UPDATE MEMBERS SET Email = @email, Password = @password WHERE Fullname = @user_name";
            command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@email", popupForm.Email);
            command.Parameters.AddWithValue("@password", popupForm.Password);
            command.Parameters.AddWithValue("@user_name", popupForm.Fullname);
            command.ExecuteNonQuery();

            conn.Close();
            displayData(); //Call method to refresh data

        }
    }
}
