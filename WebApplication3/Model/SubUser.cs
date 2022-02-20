using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Model
{


    public class SubUser
    {
        public SubUser(User user, string subUserName)
        {

            this.subUserName = subUserName;
            this.User = user;
            Guid uuid = Guid.NewGuid();
            string uuidAsString = uuid.ToString();
            this.subUserId = uuidAsString;
        }

        public SubUser() { }


        [Key]
        public String subUserId { get; set; }



        public string subUserName { get; set; }


        public User User { get; set; }

        public IList<Spending> Spendings { get; } = new List<Spending>();
    }




}

