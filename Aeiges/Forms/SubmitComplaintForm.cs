using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Aeiges.Database;
using Aeiges.Models;

namespace Aeiges.Forms
{
    public partial class SubmitComplaintForm : Form
    {
        private string selectedEvidencePath = null;
        private bool isAnonymous = false;

        public SubmitComplaintForm(bool isAnonymous = false)
        {
            this.isAnonymous = isAnonymous;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            Theme.ApplyFormBackground(this);
            Theme.StyleSuccessButton(btnSubmit);
            Theme.StyleOutlineButton(btnCancel);
            Theme.StyleOutlineButton(btnBrowse);
            Theme.StyleTextBox(txtTitle);
            Theme.StyleTextBox(txtDescription);
            Theme.StyleTextBox(txtLocation);
            lblTitle.ForeColor = Theme.PrimaryDark;
            lblTitle.Font = Theme.HeaderFont;
        }

        private void SubmitComplaintForm_Load(object sender, EventArgs e)
        {
            LoadCrimeTypes();
            cmbPriority.SelectedIndex = 1; // Medium
            dtpIncident.Value = DateTime.Now;

            if (isAnonymous)
            {
                this.Text = "Anonymous Report - Aeiges";
                lblTitle.Text = "Submit Anonymous Cybercrime Report";
                lblTitle.ForeColor = Theme.Accent;
                // Show privacy notice
                lblPrivacy.Visible = true;
                lblPrivacy.Text = "Your identity will not be stored. This report is completely anonymous.";
            }
            else
            {
                lblPrivacy.Visible = false;
            }
        }

        private void LoadCrimeTypes()
        {
            try
            {
                DataTable dt = DBConnection.ExecuteQuery("SELECT CrimeTypeID, TypeName FROM CrimeTypes ORDER BY TypeName");
                cmbCrimeType.DataSource = dt;
                cmbCrimeType.DisplayMember = "TypeName";
                cmbCrimeType.ValueMember = "CrimeTypeID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading crime types: " + ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Evidence File";
                ofd.Filter = "All Files|*.*|Images|*.jpg;*.jpeg;*.png;*.gif|Documents|*.pdf;*.doc;*.docx|Videos|*.mp4;*.avi";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedEvidencePath = ofd.FileName;
                    lblEvidence.Text = Path.GetFileName(selectedEvidencePath);
                    lblEvidence.ForeColor = Theme.Success;
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            string location = txtLocation.Text.Trim();

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Title and Description are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCrimeType.SelectedValue == null)
            {
                MessageBox.Show("Please select a Crime Type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string evidenceFileName = null;
                if (!string.IsNullOrEmpty(selectedEvidencePath) && File.Exists(selectedEvidencePath))
                {
                    string evidenceFolder = Path.Combine(Application.StartupPath, "EvidenceFiles");
                    if (!Directory.Exists(evidenceFolder))
                        Directory.CreateDirectory(evidenceFolder);

                    evidenceFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(selectedEvidencePath);
                    string destPath = Path.Combine(evidenceFolder, evidenceFileName);
                    File.Copy(selectedEvidencePath, destPath, true);
                }

                object userIdParam = DBNull.Value;
                int isAnon = 1;

                if (!isAnonymous && User.CurrentUser != null)
                {
                    userIdParam = User.CurrentUser.UserID;
                    isAnon = 0;
                }

                string query = @"INSERT INTO Complaints 
                    (UserID, Title, Description, CrimeTypeID, Status, Priority, EvidencePath, Location, IncidentDate, IsAnonymous)
                    VALUES (@uid, @title, @desc, @ctid, 'Submitted', @prio, @evid, @loc, @incdate, @anon);
                    SELECT LAST_INSERT_ID();";

                object newId = DBConnection.ExecuteScalar(query,
                    new MySqlParameter("@uid", userIdParam),
                    new MySqlParameter("@title", title),
                    new MySqlParameter("@desc", description),
                    new MySqlParameter("@ctid", cmbCrimeType.SelectedValue),
                    new MySqlParameter("@prio", cmbPriority.SelectedItem.ToString()),
                    new MySqlParameter("@evid", evidenceFileName ?? (object)DBNull.Value),
                    new MySqlParameter("@loc", string.IsNullOrEmpty(location) ? (object)DBNull.Value : location),
                    new MySqlParameter("@incdate", dtpIncident.Value),
                    new MySqlParameter("@anon", isAnon));

                int complaintId = Convert.ToInt32(newId);

                // Log initial status (use system/anonymous marker)
                int updatedBy = (User.CurrentUser != null) ? User.CurrentUser.UserID : 1; // fallback to admin id if fully anonymous
                try
                {
                    DBConnection.ExecuteNonQuery(
                        "INSERT INTO CaseUpdates (ComplaintID, UpdatedBy, OldStatus, NewStatus, Remarks) VALUES (@cid, @uid, NULL, 'Submitted', @rem)",
                        new MySqlParameter("@cid", complaintId),
                        new MySqlParameter("@uid", updatedBy),
                        new MySqlParameter("@rem", isAnonymous ? "Anonymous complaint submitted" : "Complaint submitted by user"));
                }
                catch { /* ignore if no valid user for UpdatedBy */ }

                if (!string.IsNullOrEmpty(evidenceFileName))
                {
                    DBConnection.ExecuteNonQuery(
                        "INSERT INTO Evidence (ComplaintID, FileName, FilePath, FileType) VALUES (@cid, @fn, @fp, @ft)",
                        new MySqlParameter("@cid", complaintId),
                        new MySqlParameter("@fn", Path.GetFileName(selectedEvidencePath)),
                        new MySqlParameter("@fp", evidenceFileName),
                        new MySqlParameter("@ft", Path.GetExtension(selectedEvidencePath)));
                }

                string msg = isAnonymous
                    ? "Anonymous report submitted successfully!\n\nYour Case ID: " + complaintId + "\n\nPlease save this Case ID to track your report later."
                    : "Complaint submitted successfully!\nYour Case ID: " + complaintId;

                MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting complaint: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
