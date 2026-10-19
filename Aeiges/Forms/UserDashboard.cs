using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Aeiges.Database;
using Aeiges.Models;

namespace Aeiges.Forms
{
    public partial class UserDashboard : Form
    {
        public UserDashboard()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            Theme.ApplyFormBackground(this);
            Theme.StyleHeaderPanel(pnlTop);
            Theme.StyleDataGrid(dgvComplaints);
            Theme.StyleSuccessButton(btnSubmitNew);
            Theme.StyleOutlineButton(btnRefresh);
            Theme.StyleOutlineButton(btnViewDetails);
            Theme.StyleDangerButton(btnLogout);
            if (btnProfile != null) { btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat; btnProfile.ForeColor = System.Drawing.Color.White; btnProfile.FlatAppearance.BorderColor = System.Drawing.Color.White; }

            lblWelcome.Text = "Welcome, " + User.CurrentUser.FullName + " (" + User.CurrentUser.Role + ")";
            LoadMyComplaints();
        }

        private void LoadMyComplaints()
        {
            try
            {
                string query = @"SELECT c.ComplaintID, c.Title, ct.TypeName AS CrimeType, c.Status, c.Priority, 
                                        c.CreatedAt, c.UpdatedAt, u.FullName AS OfficerName
                                 FROM Complaints c
                                 LEFT JOIN CrimeTypes ct ON c.CrimeTypeID = ct.CrimeTypeID
                                 LEFT JOIN Users u ON c.AssignedOfficerID = u.UserID
                                 WHERE c.UserID = @uid
                                 ORDER BY c.CreatedAt DESC";

                DataTable dt = DBConnection.ExecuteQuery(query, new MySqlParameter("@uid", User.CurrentUser.UserID));
                dgvComplaints.DataSource = dt;

                if (dgvComplaints.Columns.Count > 0)
                {
                    dgvComplaints.Columns["ComplaintID"].HeaderText = "ID";
                    dgvComplaints.Columns["Title"].HeaderText = "Title";
                    dgvComplaints.Columns["CrimeType"].HeaderText = "Crime Type";
                    dgvComplaints.Columns["Status"].HeaderText = "Status";
                    dgvComplaints.Columns["Priority"].HeaderText = "Priority";
                    dgvComplaints.Columns["CreatedAt"].HeaderText = "Submitted On";
                    dgvComplaints.Columns["UpdatedAt"].HeaderText = "Last Updated";
                    dgvComplaints.Columns["OfficerName"].HeaderText = "Assigned Officer";
                }

                lblCount.Text = "Total Complaints: " + dt.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading complaints: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubmitNew_Click(object sender, EventArgs e)
        {
            SubmitComplaintForm form = new SubmitComplaintForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadMyComplaints();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMyComplaints();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvComplaints.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a complaint first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = Convert.ToInt32(dgvComplaints.SelectedRows[0].Cells["ComplaintID"].Value);
            ViewComplaintForm view = new ViewComplaintForm(id, false); // false = user view (read-only status)
            view.ShowDialog();
            LoadMyComplaints();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            User.CurrentUser = null;
            this.Hide();
            LoginForm login = new LoginForm();
            login.FormClosed += (s, args) => this.Close();
            login.Show();
        }


        private void btnProfile_Click(object sender, EventArgs e)
        {
            ProfileForm pf = new ProfileForm();
            var result = pf.ShowDialog();
            if (result == DialogResult.OK)
            {
                lblWelcome.Text = "Welcome, " + User.CurrentUser.FullName + " (" + User.CurrentUser.Role + ")";
            }
            else if (result == DialogResult.Abort)
            {
                // Account deleted - logout
                User.CurrentUser = null;
                this.Hide();
                LoginForm login = new LoginForm();
                login.FormClosed += (s, args) => this.Close();
                login.Show();
            }
        }

        private void dgvComplaints_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                btnViewDetails_Click(sender, e);
        }
    }
}
