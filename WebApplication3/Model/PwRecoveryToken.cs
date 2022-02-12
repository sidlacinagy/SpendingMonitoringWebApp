using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Model
{


    public class PwRecoveryToken
    {
        public PwRecoveryToken(User user, string token, DateTime expirationDate)
        {

            this.user = user;
            this.token = token;
            this.expirationDate = expirationDate;
        }

        public PwRecoveryToken() { }

 
        public User user { get; set; }

        [Key]
        public string token { get; set; }

      
        public DateTime expirationDate { get; set; }
    }





}