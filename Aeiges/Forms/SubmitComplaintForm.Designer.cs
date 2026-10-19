namespace Aeiges.Forms
{
    partial class SubmitComplaintForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPrivacy = new System.Windows.Forms.Label();
            this.lblCaseTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblCrimeType = new System.Windows.Forms.Label();
            this.cmbCrimeType = new System.Windows.Forms.ComboBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblIncidentDate = new System.Windows.Forms.Label();
            this.dtpIncident = new System.Windows.Forms.DateTimePicker();
            this.lblEvidenceLabel = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblEvidence = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(480, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Submit New Cybercrime Complaint";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrivacy
            // 
            this.lblPrivacy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPrivacy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(121)))), ((int)(((byte)(107)))));
            this.lblPrivacy.Location = new System.Drawing.Point(20, 42);
            this.lblPrivacy.Name = "lblPrivacy";
            this.lblPrivacy.Size = new System.Drawing.Size(480, 22);
            this.lblPrivacy.TabIndex = 18;
            this.lblPrivacy.Text = "Your identity will not be stored. This report is completely anonymous.";
            this.lblPrivacy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPrivacy.Visible = false;
            // 
            // lblCaseTitle
            // 
            this.lblCaseTitle.AutoSize = true;
            this.lblCaseTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCaseTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblCaseTitle.Location = new System.Drawing.Point(30, 75);
            this.lblCaseTitle.Name = "lblCaseTitle";
            this.lblCaseTitle.Size = new System.Drawing.Size(32, 15);
            this.lblCaseTitle.TabIndex = 1;
            this.lblCaseTitle.Text = "Title*";
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTitle.Location = new System.Drawing.Point(30, 94);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(460, 25);
            this.txtTitle.TabIndex = 2;
            // 
            // lblCrimeType
            // 
            this.lblCrimeType.AutoSize = true;
            this.lblCrimeType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCrimeType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblCrimeType.Location = new System.Drawing.Point(30, 130);
            this.lblCrimeType.Name = "lblCrimeType";
            this.lblCrimeType.Size = new System.Drawing.Size(70, 15);
            this.lblCrimeType.TabIndex = 3;
            this.lblCrimeType.Text = "Crime Type*";
            // 
            // cmbCrimeType
            // 
            this.cmbCrimeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCrimeType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCrimeType.FormattingEnabled = true;
            this.cmbCrimeType.Location = new System.Drawing.Point(30, 149);
            this.cmbCrimeType.Name = "cmbCrimeType";
            this.cmbCrimeType.Size = new System.Drawing.Size(220, 25);
            this.cmbCrimeType.TabIndex = 4;
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPriority.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblPriority.Location = new System.Drawing.Point(270, 130);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(45, 15);
            this.lblPriority.TabIndex = 5;
            this.lblPriority.Text = "Priority";
            // 
            // cmbPriority
            // 
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High",
            "Critical"});
            this.cmbPriority.Location = new System.Drawing.Point(270, 149);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(220, 25);
            this.cmbPriority.TabIndex = 6;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblDescription.Location = new System.Drawing.Point(30, 185);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(70, 15);
            this.lblDescription.TabIndex = 7;
            this.lblDescription.Text = "Description*";
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(30, 204);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(460, 100);
            this.txtDescription.TabIndex = 8;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblLocation.Location = new System.Drawing.Point(30, 315);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(52, 15);
            this.lblLocation.TabIndex = 9;
            this.lblLocation.Text = "Location";
            // 
            // txtLocation
            // 
            this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLocation.Location = new System.Drawing.Point(30, 334);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(220, 25);
            this.txtLocation.TabIndex = 10;
            // 
            // lblIncidentDate
            // 
            this.lblIncidentDate.AutoSize = true;
            this.lblIncidentDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblIncidentDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblIncidentDate.Location = new System.Drawing.Point(270, 315);
            this.lblIncidentDate.Name = "lblIncidentDate";
            this.lblIncidentDate.Size = new System.Drawing.Size(76, 15);
            this.lblIncidentDate.TabIndex = 11;
            this.lblIncidentDate.Text = "Incident Date";
            // 
            // dtpIncident
            // 
            this.dtpIncident.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpIncident.Location = new System.Drawing.Point(270, 334);
            this.dtpIncident.Name = "dtpIncident";
            this.dtpIncident.Size = new System.Drawing.Size(220, 25);
            this.dtpIncident.TabIndex = 12;
            // 
            // lblEvidenceLabel
            // 
            this.lblEvidenceLabel.AutoSize = true;
            this.lblEvidenceLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEvidenceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblEvidenceLabel.Location = new System.Drawing.Point(30, 375);
            this.lblEvidenceLabel.Name = "lblEvidenceLabel";
            this.lblEvidenceLabel.Size = new System.Drawing.Size(55, 15);
            this.lblEvidenceLabel.TabIndex = 13;
            this.lblEvidenceLabel.Text = "Evidence";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(95)))));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnBrowse.Location = new System.Drawing.Point(30, 395);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(110, 30);
            this.btnBrowse.TabIndex = 14;
            this.btnBrowse.Text = "Browse File...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblEvidence
            // 
            this.lblEvidence.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEvidence.ForeColor = System.Drawing.Color.Gray;
            this.lblEvidence.Location = new System.Drawing.Point(150, 400);
            this.lblEvidence.Name = "lblEvidence";
            this.lblEvidence.Size = new System.Drawing.Size(340, 20);
            this.lblEvidence.TabIndex = 15;
            this.lblEvidence.Text = "No file selected";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(140, 450);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(140, 40);
            this.btnSubmit.TabIndex = 16;
            this.btnSubmit.Text = "Submit Report";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(95)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(300, 450);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SubmitComplaintForm
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(520, 515);
            this.Controls.Add(this.lblPrivacy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblEvidence);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblEvidenceLabel);
            this.Controls.Add(this.dtpIncident);
            this.Controls.Add(this.lblIncidentDate);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.cmbPriority);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.cmbCrimeType);
            this.Controls.Add(this.lblCrimeType);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblCaseTitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "SubmitComplaintForm";
            this.Text = "Submit Complaint - Aeiges";
            this.Load += new System.EventHandler(this.SubmitComplaintForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPrivacy;
        private System.Windows.Forms.Label lblCaseTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblCrimeType;
        private System.Windows.Forms.ComboBox cmbCrimeType;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblIncidentDate;
        private System.Windows.Forms.DateTimePicker dtpIncident;
        private System.Windows.Forms.Label lblEvidenceLabel;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblEvidence;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
    }
}
