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
    public partial class MemberMainPage : Form
    {
        public MemberMainPage()
        {
            InitializeComponent();
        }

       

        private void changeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMemberDetails popupform = new ChangeMemberDetails();
            popupform.MdiParent = this;
            popupform.Show();
        }

        private void browseBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrowseBooks popupform = new BrowseBooks();
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
    }
}
