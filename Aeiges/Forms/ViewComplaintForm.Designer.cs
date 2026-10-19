namespace Aeiges.Forms
{
    partial class ViewComplaintForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblCaseId = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblCrimeType = new System.Windows.Forms.Label();
            this.lblReporter = new System.Windows.Forms.Label();
            this.lblOfficer = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblIncident = new System.Windows.Forms.Label();
            this.lblCreated = new System.Windows.Forms.Label();
            this.lblUpdated = new System.Windows.Forms.Label();
            this.lblDescLabel = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblEvidenceLabel = new System.Windows.Forms.Label();
            this.lblEvidence = new System.Windows.Forms.Label();
            this.btnOpenEvidence = new System.Windows.Forms.Button();
            this.lblHistory = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCaseId
            // 
            this.lblCaseId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaseId.Location = new System.Drawing.Point(20, 15);
            this.lblCaseId.Name = "lblCaseId";
            this.lblCaseId.Size = new System.Drawing.Size(200, 22);
            this.lblCaseId.TabIndex = 0;
            this.lblCaseId.Text = "Case ID: #";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 42);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(560, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(20, 75);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(180, 20);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status:";
            // 
            // lblPriority
            // 
            this.lblPriority.Location = new System.Drawing.Point(210, 75);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(150, 20);
            this.lblPriority.TabIndex = 3;
            this.lblPriority.Text = "Priority:";
            // 
            // lblCrimeType
            // 
            this.lblCrimeType.Location = new System.Drawing.Point(370, 75);
            this.lblCrimeType.Name = "lblCrimeType";
            this.lblCrimeType.Size = new System.Drawing.Size(210, 20);
            this.lblCrimeType.TabIndex = 4;
            this.lblCrimeType.Text = "Crime Type:";
            // 
            // lblReporter
            // 
            this.lblReporter.Location = new System.Drawing.Point(20, 100);
            this.lblReporter.Name = "lblReporter";
            this.lblReporter.Size = new System.Drawing.Size(280, 20);
            this.lblReporter.TabIndex = 5;
            this.lblReporter.Text = "Reported By:";
            // 
            // lblOfficer
            // 
            this.lblOfficer.Location = new System.Drawing.Point(310, 100);
            this.lblOfficer.Name = "lblOfficer";
            this.lblOfficer.Size = new System.Drawing.Size(270, 20);
            this.lblOfficer.TabIndex = 6;
            this.lblOfficer.Text = "Assigned Officer:";
            // 
            // lblLocation
            // 
            this.lblLocation.Location = new System.Drawing.Point(20, 125);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(280, 20);
            this.lblLocation.TabIndex = 7;
            this.lblLocation.Text = "Location:";
            // 
            // lblIncident
            // 
            this.lblIncident.Location = new System.Drawing.Point(310, 125);
            this.lblIncident.Name = "lblIncident";
            this.lblIncident.Size = new System.Drawing.Size(270, 20);
            this.lblIncident.TabIndex = 8;
            this.lblIncident.Text = "Incident Date:";
            // 
            // lblCreated
            // 
            this.lblCreated.Location = new System.Drawing.Point(20, 150);
            this.lblCreated.Name = "lblCreated";
            this.lblCreated.Size = new System.Drawing.Size(280, 20);
            this.lblCreated.TabIndex = 9;
            this.lblCreated.Text = "Submitted:";
            // 
            // lblUpdated
            // 
            this.lblUpdated.Location = new System.Drawing.Point(310, 150);
            this.lblUpdated.Name = "lblUpdated";
            this.lblUpdated.Size = new System.Drawing.Size(270, 20);
            this.lblUpdated.TabIndex = 10;
            this.lblUpdated.Text = "Last Updated:";
            // 
            // lblDescLabel
            // 
            this.lblDescLabel.AutoSize = true;
            this.lblDescLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescLabel.Location = new System.Drawing.Point(20, 180);
            this.lblDescLabel.Name = "lblDescLabel";
            this.lblDescLabel.Size = new System.Drawing.Size(74, 15);
            this.lblDescLabel.TabIndex = 11;
            this.lblDescLabel.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(20, 200);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(560, 80);
            this.txtDescription.TabIndex = 12;
            // 
            // lblEvidenceLabel
            // 
            this.lblEvidenceLabel.AutoSize = true;
            this.lblEvidenceLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEvidenceLabel.Location = new System.Drawing.Point(20, 290);
            this.lblEvidenceLabel.Name = "lblEvidenceLabel";
            this.lblEvidenceLabel.Size = new System.Drawing.Size(58, 15);
            this.lblEvidenceLabel.TabIndex = 13;
            this.lblEvidenceLabel.Text = "Evidence";
            // 
            // lblEvidence
            // 
            this.lblEvidence.Location = new System.Drawing.Point(20, 310);
            this.lblEvidence.Name = "lblEvidence";
            this.lblEvidence.Size = new System.Drawing.Size(420, 20);
            this.lblEvidence.TabIndex = 14;
            this.lblEvidence.Text = "No evidence";
            // 
            // btnOpenEvidence
            // 
            this.btnOpenEvidence.Location = new System.Drawing.Point(450, 305);
            this.btnOpenEvidence.Name = "btnOpenEvidence";
            this.btnOpenEvidence.Size = new System.Drawing.Size(130, 28);
            this.btnOpenEvidence.TabIndex = 15;
            this.btnOpenEvidence.Text = "Open Evidence File";
            this.btnOpenEvidence.UseVisualStyleBackColor = true;
            this.btnOpenEvidence.Click += new System.EventHandler(this.btnOpenEvidence_Click);
            // 
            // lblHistory
            // 
            this.lblHistory.AutoSize = true;
            this.lblHistory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHistory.Location = new System.Drawing.Point(20, 345);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(99, 15);
            this.lblHistory.TabIndex = 16;
            this.lblHistory.Text = "Status History";
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Location = new System.Drawing.Point(20, 365);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.Size = new System.Drawing.Size(560, 120);
            this.dgvHistory.TabIndex = 17;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(250, 500);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 32);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ViewComplaintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 545);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.lblHistory);
            this.Controls.Add(this.btnOpenEvidence);
            this.Controls.Add(this.lblEvidence);
            this.Controls.Add(this.lblEvidenceLabel);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescLabel);
            this.Controls.Add(this.lblUpdated);
            this.Controls.Add(this.lblCreated);
            this.Controls.Add(this.lblIncident);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblOfficer);
            this.Controls.Add(this.lblReporter);
            this.Controls.Add(this.lblCrimeType);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblCaseId);
            this.Name = "ViewComplaintForm";
            this.Text = "View Complaint Details - Aeiges";
            this.Load += new System.EventHandler(this.ViewComplaintForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblCaseId;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.Label lblCrimeType;
        private System.Windows.Forms.Label lblReporter;
        private System.Windows.Forms.Label lblOfficer;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblIncident;
        private System.Windows.Forms.Label lblCreated;
        private System.Windows.Forms.Label lblUpdated;
        private System.Windows.Forms.Label lblDescLabel;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblEvidenceLabel;
        private System.Windows.Forms.Label lblEvidence;
        private System.Windows.Forms.Button btnOpenEvidence;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Button btnClose;
    }
}
