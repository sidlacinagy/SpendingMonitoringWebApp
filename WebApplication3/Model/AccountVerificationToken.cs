using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{


    public class AccountVerificationToken
    {
        public AccountVerificationToken(User user, string token, DateTime expirationDate)
        {

            this.user = user;
            this.token = token;
            this.expirationDate = expirationDate;
        }

        public AccountVerificationToken(){}


        public User user{ get; set; }

        [Key]
        public string token { get; set; }
        public DateTime expirationDate { get; set; }
    }




}