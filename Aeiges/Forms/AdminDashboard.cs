using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Aeiges.Database;
using Aeiges.Models;

namespace Aeiges.Forms
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            Theme.ApplyFormBackground(this);
            Theme.StyleHeaderPanel(pnlTop);
            Theme.StyleDataGrid(dgvComplaints);
            Theme.StylePrimaryButton(btnUpdateStatus);
            Theme.StyleSuccessButton(btnAssign);
            Theme.StyleOutlineButton(btnRefresh);
            Theme.StyleOutlineButton(btnViewDetails);
            if (btnAddOfficer != null) Theme.StyleOutlineButton(btnAddOfficer);
            Theme.StyleDangerButton(btnLogout);
            if (btnProfile != null) { btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat; btnProfile.ForeColor = System.Drawing.Color.White; btnProfile.FlatAppearance.BorderColor = System.Drawing.Color.White; }

            lblWelcome.Text = "Welcome, " + User.CurrentUser.FullName + " (" + User.CurrentUser.Role + ")";
            LoadAllComplaints();
            LoadOfficers();
            LoadStats();

            // Only Admin can create new officers/admins
            if (User.CurrentUser.Role == "Admin")
            {
                btnAddOfficer.Visible = true;
            }
            else
            {
                btnAddOfficer.Visible = false;
            }
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
                User.CurrentUser = null;
                this.Hide();
                LoginForm login = new LoginForm();
                login.FormClosed += (s, args) => this.Close();
                login.Show();
            }
        }

        private void btnAddOfficer_Click(object sender, EventArgs e)
        {
            AddOfficerForm form = new AddOfficerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadOfficers(); // refresh officer list in assign dropdown
            }
        }

        private void LoadStats()
        {
            try
            {
                object total = DBConnection.ExecuteScalar("SELECT COUNT(*) FROM Complaints");
                object pending = DBConnection.ExecuteScalar("SELECT COUNT(*) FROM Complaints WHERE Status IN ('Submitted','Under Review')");
                object investigating = DBConnection.ExecuteScalar("SELECT COUNT(*) FROM Complaints WHERE Status IN ('Assigned','Investigating')");
                object resolved = DBConnection.ExecuteScalar("SELECT COUNT(*) FROM Complaints WHERE Status IN ('Resolved','Closed')");

                lblTotal.Text = "Total: " + total;
                lblPending.Text = "Pending: " + pending;
                lblInvestigating.Text = "Investigating: " + investigating;
                lblResolved.Text = "Resolved: " + resolved;
            }
            catch { }
        }

        private void LoadAllComplaints()
        {
            try
            {
                string filter = cmbStatusFilter.SelectedItem?.ToString() ?? "All";
                string query = @"SELECT c.ComplaintID, c.Title, ct.TypeName AS CrimeType, c.Status, c.Priority, 
                                        CASE WHEN c.IsAnonymous = 1 OR c.UserID IS NULL THEN 'Anonymous' ELSE u.FullName END AS Reporter,
                                        o.FullName AS Officer, c.CreatedAt, c.UpdatedAt
                                 FROM Complaints c
                                 LEFT JOIN CrimeTypes ct ON c.CrimeTypeID = ct.CrimeTypeID
                                 LEFT JOIN Users u ON c.UserID = u.UserID
                                 LEFT JOIN Users o ON c.AssignedOfficerID = o.UserID";

                if (filter != "All" && !string.IsNullOrEmpty(filter))
                {
                    query += " WHERE c.Status = @status";
                    DataTable dt = DBConnection.ExecuteQuery(query + " ORDER BY c.CreatedAt DESC",
                        new MySqlParameter("@status", filter));
                    dgvComplaints.DataSource = dt;
                }
                else
                {
                    DataTable dt = DBConnection.ExecuteQuery(query + " ORDER BY c.CreatedAt DESC");
                    dgvComplaints.DataSource = dt;
                }

                if (dgvComplaints.Columns.Count > 0)
                {
                    dgvComplaints.Columns["ComplaintID"].HeaderText = "ID";
                    dgvComplaints.Columns["Title"].HeaderText = "Title";
                    dgvComplaints.Columns["CrimeType"].HeaderText = "Crime Type";
                    dgvComplaints.Columns["Status"].HeaderText = "Status";
                    dgvComplaints.Columns["Priority"].HeaderText = "Priority";
                    dgvComplaints.Columns["Reporter"].HeaderText = "Reported By";
                    dgvComplaints.Columns["Officer"].HeaderText = "Assigned To";
                    dgvComplaints.Columns["CreatedAt"].HeaderText = "Submitted";
                    dgvComplaints.Columns["UpdatedAt"].HeaderText = "Updated";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOfficers()
        {
            try
            {
                DataTable dt = DBConnection.ExecuteQuery(
                    "SELECT UserID, FullName FROM Users WHERE Role IN ('Officer','Admin') AND IsActive = 1 ORDER BY FullName");
                cmbOfficer.DataSource = dt;
                cmbOfficer.DisplayMember = "FullName";
                cmbOfficer.ValueMember = "UserID";
                cmbOfficer.SelectedIndex = -1;
            }
            catch { }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllComplaints();
            LoadStats();
        }

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllComplaints();
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (dgvComplaints.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a complaint first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cmbOfficer.SelectedValue == null)
            {
                MessageBox.Show("Select an officer to assign.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int complaintId = Convert.ToInt32(dgvComplaints.SelectedRows[0].Cells["ComplaintID"].Value);
            int officerId = Convert.ToInt32(cmbOfficer.SelectedValue);

            try
            {
                string oldStatus = dgvComplaints.SelectedRows[0].Cells["Status"].Value?.ToString() ?? "";
                DBConnection.ExecuteNonQuery(
                    "UPDATE Complaints SET AssignedOfficerID = @oid, Status = 'Assigned', UpdatedAt = NOW() WHERE ComplaintID = @cid",
                    new MySqlParameter("@oid", officerId),
                    new MySqlParameter("@cid", complaintId));

                // Log update
                DBConnection.ExecuteNonQuery(
                    "INSERT INTO CaseUpdates (ComplaintID, UpdatedBy, OldStatus, NewStatus, Remarks) VALUES (@cid, @uid, @old, 'Assigned', @rem)",
                    new MySqlParameter("@cid", complaintId),
                    new MySqlParameter("@uid", User.CurrentUser.UserID),
                    new MySqlParameter("@old", oldStatus),
                    new MySqlParameter("@rem", "Assigned to officer by " + User.CurrentUser.FullName));

                MessageBox.Show("Case assigned successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllComplaints();
                LoadStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvComplaints.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a complaint first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cmbNewStatus.SelectedItem == null)
            {
                MessageBox.Show("Select a new status.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int complaintId = Convert.ToInt32(dgvComplaints.SelectedRows[0].Cells["ComplaintID"].Value);
            string newStatus = cmbNewStatus.SelectedItem.ToString();
            string remarks = txtRemarks.Text.Trim();

            try
            {
                string oldStatus = dgvComplaints.SelectedRows[0].Cells["Status"].Value?.ToString() ?? "";
                DBConnection.ExecuteNonQuery(
                    "UPDATE Complaints SET Status = @st, UpdatedAt = NOW() WHERE ComplaintID = @cid",
                    new MySqlParameter("@st", newStatus),
                    new MySqlParameter("@cid", complaintId));

                DBConnection.ExecuteNonQuery(
                    "INSERT INTO CaseUpdates (ComplaintID, UpdatedBy, OldStatus, NewStatus, Remarks) VALUES (@cid, @uid, @old, @new, @rem)",
                    new MySqlParameter("@cid", complaintId),
                    new MySqlParameter("@uid", User.CurrentUser.UserID),
                    new MySqlParameter("@old", oldStatus),
                    new MySqlParameter("@new", newStatus),
                    new MySqlParameter("@rem", remarks));

                MessageBox.Show("Status updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRemarks.Clear();
                LoadAllComplaints();
                LoadStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvComplaints.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a complaint first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int id = Convert.ToInt32(dgvComplaints.SelectedRows[0].Cells["ComplaintID"].Value);
            ViewComplaintForm view = new ViewComplaintForm(id, true); // true = admin/officer can see more
            view.ShowDialog();
            LoadAllComplaints();
            LoadStats();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            User.CurrentUser = null;
            this.Hide();
            LoginForm login = new LoginForm();
            login.FormClosed += (s, args) => this.Close();
            login.Show();
        }

        private void dgvComplaints_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                btnViewDetails_Click(sender, e);
        }
    }
}
