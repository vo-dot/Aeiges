using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Aeiges.Database;
using Aeiges.Models;

namespace Aeiges.Forms
{
    public partial class ProfileForm : Form
    {
        private string selectedPicturePath = null;

        public ProfileForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = "My Profile - Aeiges";
            LoadProfile();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            Theme.ApplyFormBackground(this);
            Theme.StylePrimaryButton(btnSave);
            Theme.StyleOutlineButton(btnChangePicture);
            Theme.StyleOutlineButton(btnChangePassword);
            Theme.StyleDangerButton(btnDeleteAccount);
            Theme.StyleOutlineButton(btnCancel);
            Theme.StyleTextBox(txtFullName);
            Theme.StyleTextBox(txtEmail);
            Theme.StyleTextBox(txtPhone);
            Theme.StyleTextBox(txtNewPassword);
            Theme.StyleTextBox(txtConfirmPassword);
            lblTitle.ForeColor = Theme.Primary;
            lblTitle.Font = Theme.HeaderFont;
        }

        private void LoadProfile()
        {
            if (User.CurrentUser == null) { this.Close(); return; }

            txtFullName.Text = User.CurrentUser.FullName;
            txtEmail.Text = User.CurrentUser.Email;
            txtPhone.Text = User.CurrentUser.Phone ?? "";
            lblUsername.Text = "Username: " + User.CurrentUser.Username;
            lblRole.Text = "Role: " + User.CurrentUser.Role;

            LoadProfilePicture();
        }

        private void LoadProfilePicture()
        {
            try
            {
                if (!string.IsNullOrEmpty(User.CurrentUser.ProfilePicture))
                {
                    string path = Path.Combine(Application.StartupPath, "ProfilePictures", User.CurrentUser.ProfilePicture);
                    if (File.Exists(path))
                    {
                        using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                        {
                            picProfile.Image = Image.FromStream(fs);
                        }
                        return;
                    }
                }
            }
            catch { }
            // Default placeholder
            picProfile.Image = null;
            picProfile.BackColor = Theme.PrimaryLight;
        }

        private void btnChangePicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Profile Picture";
                ofd.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedPicturePath = ofd.FileName;
                    try
                    {
                        using (var fs = new FileStream(selectedPicturePath, FileMode.Open, FileAccess.Read))
                        {
                            picProfile.Image = Image.FromStream(fs);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Could not load image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Full name and email are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string pictureFile = User.CurrentUser.ProfilePicture;

                if (!string.IsNullOrEmpty(selectedPicturePath) && File.Exists(selectedPicturePath))
                {
                    string folder = Path.Combine(Application.StartupPath, "ProfilePictures");
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    pictureFile = Guid.NewGuid().ToString() + Path.GetExtension(selectedPicturePath);
                    File.Copy(selectedPicturePath, Path.Combine(folder, pictureFile), true);
                }

                DBConnection.ExecuteNonQuery(
                    @"UPDATE Users SET FullName = @name, Email = @email, Phone = @phone, ProfilePicture = @pic 
                      WHERE UserID = @id",
                    new MySqlParameter("@name", fullName),
                    new MySqlParameter("@email", email),
                    new MySqlParameter("@phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone),
                    new MySqlParameter("@pic", pictureFile ?? (object)DBNull.Value),
                    new MySqlParameter("@id", User.CurrentUser.UserID));

                User.CurrentUser.FullName = fullName;
                User.CurrentUser.Email = email;
                User.CurrentUser.Phone = phone;
                User.CurrentUser.ProfilePicture = pictureFile;

                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string newPass = txtNewPassword.Text;
            string confirm = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(newPass) || newPass.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newPass != confirm)
            {
                MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string hash = DBConnection.HashPassword(newPass);
                DBConnection.ExecuteNonQuery(
                    "UPDATE Users SET PasswordHash = @h WHERE UserID = @id",
                    new MySqlParameter("@h", hash),
                    new MySqlParameter("@id", User.CurrentUser.UserID));

                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to DELETE your account?\n\nThis will permanently remove your account and cannot be undone.\nYour submitted complaints will remain as anonymous records.",
                "Confirm Account Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            var confirm = MessageBox.Show(
                "Final confirmation: Delete account \"" + User.CurrentUser.Username + "\"?",
                "Final Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop);

            if (confirm != DialogResult.Yes) return;

            try
            {
                // Soft-delete: mark inactive, or hard delete
                // Option: set IsActive = 0 and clear personal data, keep complaints as anonymous
                DBConnection.ExecuteNonQuery(
                    @"UPDATE Complaints SET UserID = NULL, IsAnonymous = 1 WHERE UserID = @id;
                      DELETE FROM Users WHERE UserID = @id",
                    new MySqlParameter("@id", User.CurrentUser.UserID));

                MessageBox.Show("Your account has been deleted.", "Account Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                User.CurrentUser = null;
                this.DialogResult = DialogResult.Abort; // signal parent to logout
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picProfile_Click(object sender, EventArgs e)
        {

        }
    }
}
