using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model

{
    public class User
    {
        public User(string email, string salt, string password, bool verified)
        {

            this.email = email;
            this.passwordHash = password;
            this.verified = verified;
            this.salt = salt;
        }

        public User() { }

        [Key]
        public string email { get; set; }

        public string salt { get; set; }
        public string passwordHash { get; set; }
        public bool verified { get; set; }

        public IList<SubUser> SubUsers { get; } = new List<SubUser>();
    }

   
}
