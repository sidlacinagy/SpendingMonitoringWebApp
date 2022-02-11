using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model

{
    public class User
    {
        public User(string email, string password, bool verified)
        {

            this.email = email;
            this.password = password;
            this.verified = verified;
        }

        public User() { }

        [Key]
        public string email { get; set; }

        public string password { get; set; }
        public bool verified { get; set; }

        public IList<SubUser> SubUsers { get; } = new List<SubUser>();
    }

   
}
