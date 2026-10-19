using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Aeiges.Database;

namespace Aeiges.Forms
{
    public partial class AddOfficerForm : Form
    {
        public AddOfficerForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;
            string email = txtEmail.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString() ?? "Officer";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Please fill all required fields (*).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                object exists = DBConnection.ExecuteScalar(
                    "SELECT COUNT(*) FROM Users WHERE Username = @u OR Email = @e",
                    new MySqlParameter("@u", username),
                    new MySqlParameter("@e", email));

                if (Convert.ToInt32(exists) > 0)
                {
                    MessageBox.Show("Username or Email already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string hash = DBConnection.HashPassword(password);
                string query = @"INSERT INTO Users (Username, PasswordHash, Email, FullName, Role, Phone) 
                                 VALUES (@username, @hash, @email, @fullname, @role, @phone)";

                DBConnection.ExecuteNonQuery(query,
                    new MySqlParameter("@username", username),
                    new MySqlParameter("@hash", hash),
                    new MySqlParameter("@email", email),
                    new MySqlParameter("@fullname", fullName),
                    new MySqlParameter("@role", role),
                    new MySqlParameter("@phone", phone));

                MessageBox.Show(role + " account created successfully!\n\nUsername: " + username, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
