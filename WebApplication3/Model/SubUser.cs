using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Model
{
   

        public class SubUser
        {
            public SubUser(User user,string subUserName)
            {

                this.subUserName = subUserName;
                this.User = user;
            }

        public SubUser() { }

       
            [Key]
            public int subuserId { get; set; }

           
            
            public string subUserName { get; set; }


        public User User { get; set; }

            public IList<Spending> Spendings { get; } = new List<Spending>();
    }




}

