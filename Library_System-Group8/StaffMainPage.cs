using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_System_Group8
{
    public partial class StaffMainPage : Form
    {
        public StaffMainPage()
        {
            InitializeComponent();
        }

        private void vIewBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewBooks popupform = new ViewBooks();
            popupform.MdiParent = this;
            popupform.Show();
        }

        private void addBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBook popupform = new AddBook();
            popupform.MdiParent = this;
            popupform.Show();
        }

        private void deleteBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteBook popupform = new DeleteBook();
            popupform.MdiParent = this;
            popupform.Show();
        }

        private void updateBookDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateBookDetails popupform = new UpdateBookDetails();
            popupform.MdiParent = this;
            popupform.Show();
        }

        private void logOutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports popupform = new Reports();
            popupform.MdiParent = this;
            popupform.Show();
        }

        private void staffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageStaff popupform = new ManageStaff();
            popupform.MdiParent = this;
            popupform.Show();
        }

        private void membersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageMembers popupform = new ManageMembers();
            popupform.MdiParent = this;
            popupform.Show();
        }

        private void manageBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
