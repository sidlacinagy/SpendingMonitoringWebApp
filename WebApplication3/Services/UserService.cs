using WebApplication3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Services
{
   
    public class UserService
    {
        private readonly SpendingAppDbContext _dbContext;
        private const int hashIterations = 17676;
        private const int saltLength = 50;
        private const int hashLength = 70;
        public UserService( SpendingAppDbContext context)
        {

            _dbContext = context;

        }
        public void RegisterUser(string email,string password)
        {
            User? user=_dbContext.Find<User>(email);
            if (user != null)
            {
                throw new Exception("User already exists");
            }

            else
            {
               String salt = AuthHelper.GenerateSalt(saltLength);
               String hashedPassword = AuthHelper.HashPassword(password, salt, hashIterations, hashLength);
               User userCreate = new User(email, salt, hashedPassword, false);
               _dbContext.Add<User>(userCreate);
               _dbContext.SaveChanges();
               
            }
            
        }

        public bool LogInUser(string email,string password)
        {
            if (!IsValidEmail(email))
            {
                throw new Exception("Invalid email");
            }

            User? user = _dbContext.Find<User>(email);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }

            else
            {
                if (!user.verified)
                {
                    throw new Exception("You need to first confirm your account");
                }
                String hashedPassword = AuthHelper.HashPassword(password, user.salt, hashIterations, hashLength);
                if (user.passwordHash.Equals(hashedPassword)){
                    return true;
                }
                else
                {
                    throw new Exception("Unsuccessful login");
                }

            }
        }

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; 
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public void VerifyUser(string email)
        {
            User? user = _dbContext.Find<User>(email);
            if (user == null)
            {
                throw new Exception("No such user");
            }
            else
            {
                user.verified = true;
                _dbContext.SaveChanges();
            }
        }

        public void ChangePassword(string email,string password)
        {
            User? user = _dbContext.Find<User>(email);
            if (user == null)
            {
                throw new Exception("No such user");
            }

            else
            {
                String salt = AuthHelper.GenerateSalt(saltLength);
                String hashedPassword = AuthHelper.HashPassword(password, salt, hashIterations, hashLength);
                user.salt = salt;
                user.passwordHash = hashedPassword;
                _dbContext.SaveChanges();
            }
        }
    }
}
