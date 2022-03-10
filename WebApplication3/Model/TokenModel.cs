using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{
    public class TokenModel 
    {

        public TokenModel()
        {

        }

        public TokenModel(string JWTToken)
        {
            this.RefreshToken = Guid.NewGuid().ToString(); 
            this.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);   
            this.JWTToken = JWTToken;
            this.IsUsed = false;
        }

        
        public string JWTToken { get; set; }

        [Key]
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

        public bool IsUsed { get; set; }
    }
}
