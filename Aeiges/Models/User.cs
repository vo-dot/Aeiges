using System;

namespace Aeiges.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } // User, Officer, Admin
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime CreatedAt { get; set; }

        public static User CurrentUser { get; set; }
    }
}
