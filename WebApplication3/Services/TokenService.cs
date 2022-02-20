using Microsoft.EntityFrameworkCore;
using WebApplication3.Model;

namespace WebApplication3.Services
{
    public class TokenService
    {
        private readonly SpendingAppDbContext _dbContext;
   
        public TokenService(SpendingAppDbContext context)
        {

            _dbContext = context;

        }

        public String CreatePwRecoveryToken(string email)
        {
            Guid uuid = Guid.NewGuid();
            string uuidAsString = uuid.ToString();
            User? user=_dbContext.Find<User>(email);
            if (user == null)
            {
                throw new Exception("No user with such email");
            }
            DateTime expirationDate = DateTime.Now.AddMinutes(30);
            PwRecoveryToken pwRecoveryToken = new PwRecoveryToken(user, uuidAsString, expirationDate);
            _dbContext.Add<PwRecoveryToken>(pwRecoveryToken);
            _dbContext.SaveChanges();
            return uuidAsString;
        }

        public String CreateAccountVerificationToken(string email)
        {
            Guid uuid = Guid.NewGuid();
            string uuidAsString = uuid.ToString();
            User? user = _dbContext.Find<User>(email);
            if (user == null)
            {
                throw new Exception("No user with such email");
            }
            DateTime expirationDate = DateTime.Now.AddMinutes(30);
            AccountVerificationToken accountVerificationToken = new AccountVerificationToken(user, uuidAsString, expirationDate);
            _dbContext.Add<AccountVerificationToken>(accountVerificationToken);
            _dbContext.SaveChanges();
            return uuidAsString;
        }

        public String GetEmailByVerificationToken(string token)
        {
            AccountVerificationToken? verToken = _dbContext.AccountVerificationToken
                       .Where(b => b.token == token)
                       .Include(e => e.user)
                       .FirstOrDefault();
            if (verToken == null)
            {
                throw new Exception("No such token");
            }
            else
            {   
                if(verToken.expirationDate< DateTime.Now)
                {
                    throw new Exception("Token expired");
                }
                
                String email = verToken.user.email;
                return email;
                
              
  
            }

        }

        public String GetEmailByResetToken(string token)
        {
            PwRecoveryToken? resToken = _dbContext.PwRecoveryToken
                       .Where(b => b.token == token)
                       .Include(e => e.user)
                       .FirstOrDefault();
            if (resToken == null)
            {
                throw new Exception("No such token");
            }
            else
            {
                if (resToken.expirationDate < DateTime.Now)
                {
                    throw new Exception("Token expired");
                }
                String email = resToken.user.email;
                return email;
            }

        }



    }
}
