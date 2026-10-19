using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Aeiges.Database;
using Aeiges.Models;

namespace Aeiges.Forms
{
    public partial class ViewComplaintForm : Form
    {
        private int complaintId;
        private bool isAdminView;

        public ViewComplaintForm(int id, bool adminView)
        {
            InitializeComponent();
            complaintId = id;
            isAdminView = adminView;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void ViewComplaintForm_Load(object sender, EventArgs e)
        {
            LoadDetails();
            LoadHistory();
        }

        private void LoadDetails()
        {
            try
            {
                string query = @"SELECT c.*, ct.TypeName, 
                                        CASE WHEN c.IsAnonymous = 1 OR c.UserID IS NULL THEN 'Anonymous' ELSE u.FullName END AS Reporter,
                                        CASE WHEN c.IsAnonymous = 1 OR c.UserID IS NULL THEN '' ELSE u.Email END AS ReporterEmail,
                                        CASE WHEN c.IsAnonymous = 1 OR c.UserID IS NULL THEN '' ELSE u.Phone END AS ReporterPhone,
                                        o.FullName AS OfficerName
                                 FROM Complaints c
                                 LEFT JOIN CrimeTypes ct ON c.CrimeTypeID = ct.CrimeTypeID
                                 LEFT JOIN Users u ON c.UserID = u.UserID
                                 LEFT JOIN Users o ON c.AssignedOfficerID = o.UserID
                                 WHERE c.ComplaintID = @id";

                DataTable dt = DBConnection.ExecuteQuery(query, new MySqlParameter("@id", complaintId));
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Complaint not found.");
                    this.Close();
                    return;
                }

                DataRow r = dt.Rows[0];
                lblCaseId.Text = "Case ID: #" + r["ComplaintID"];
                lblTitle.Text = r["Title"].ToString();
                lblStatus.Text = "Status: " + r["Status"];
                lblPriority.Text = "Priority: " + r["Priority"];
                lblCrimeType.Text = "Crime Type: " + (r["TypeName"]?.ToString() ?? "N/A");
                lblReporter.Text = "Reported By: " + r["Reporter"];
                lblOfficer.Text = "Assigned Officer: " + (r["OfficerName"]?.ToString() ?? "Not Assigned");
                lblLocation.Text = "Location: " + (r["Location"]?.ToString() ?? "N/A");
                lblIncident.Text = "Incident Date: " + (r["IncidentDate"] == DBNull.Value ? "N/A" : Convert.ToDateTime(r["IncidentDate"]).ToString("dd MMM yyyy"));
                lblCreated.Text = "Submitted: " + Convert.ToDateTime(r["CreatedAt"]).ToString("dd MMM yyyy HH:mm");
                lblUpdated.Text = "Last Updated: " + Convert.ToDateTime(r["UpdatedAt"]).ToString("dd MMM yyyy HH:mm");
                txtDescription.Text = r["Description"].ToString();

                string evid = r["EvidencePath"]?.ToString();
                if (!string.IsNullOrEmpty(evid))
                {
                    lblEvidence.Text = evid;
                    btnOpenEvidence.Enabled = true;
                }
                else
                {
                    lblEvidence.Text = "No evidence attached";
                    btnOpenEvidence.Enabled = false;
                }

                // Color status
                string st = r["Status"].ToString();
                if (st == "Resolved" || st == "Closed")
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                else if (st == "Rejected")
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                else
                    lblStatus.ForeColor = System.Drawing.Color.DarkOrange;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadHistory()
        {
            try
            {
                string query = @"SELECT cu.CreatedAt, cu.OldStatus, cu.NewStatus, cu.Remarks, u.FullName AS UpdatedBy
                                 FROM CaseUpdates cu
                                 JOIN Users u ON cu.UpdatedBy = u.UserID
                                 WHERE cu.ComplaintID = @id
                                 ORDER BY cu.CreatedAt DESC";

                DataTable dt = DBConnection.ExecuteQuery(query, new MySqlParameter("@id", complaintId));
                dgvHistory.DataSource = dt;

                if (dgvHistory.Columns.Count > 0)
                {
                    dgvHistory.Columns["CreatedAt"].HeaderText = "Date/Time";
                    dgvHistory.Columns["OldStatus"].HeaderText = "From";
                    dgvHistory.Columns["NewStatus"].HeaderText = "To";
                    dgvHistory.Columns["Remarks"].HeaderText = "Remarks";
                    dgvHistory.Columns["UpdatedBy"].HeaderText = "By";
                }
            }
            catch { }
        }

        private void btnOpenEvidence_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "EvidenceFiles", lblEvidence.Text);
                if (File.Exists(path))
                {
                    Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                }
                else
                {
                    MessageBox.Show("Evidence file not found on this machine.", "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open file: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
