using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Model
{
    public class Spending
    {
        public Spending(SubUser subUser, string product, string productCategory, int price, DateTime date)
        {

            this.subUser = subUser;
            this.product = product;     
            this.productCategory = productCategory; 
            this.price = price; 
            this.date = date;
            Guid uuid = Guid.NewGuid();
            string uuidAsString = uuid.ToString();
            this.Id = uuidAsString;
        }

        public Spending() { }

        [Key]
        public String Id { get; set; }


        public SubUser subUser { get; set; }

        public string product  { get; set; }
        public string productCategory { get; set; }
        public int price { get; set; }

        public DateTime date { get; set; }


    }

}
