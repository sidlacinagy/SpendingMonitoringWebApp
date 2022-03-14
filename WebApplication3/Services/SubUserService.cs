using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Model;


namespace WebApplication3.Services
{
    public class SubUserService
    {
        private readonly SpendingAppDbContext _dbContext;

        public SubUserService(SpendingAppDbContext context)
        {

            _dbContext = context;

        }

        public void CreateSubUserForEmail(String email, String subuser)
        {
            User? user=_dbContext.Find<User>(email);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            SubUser subUser = new SubUser(user,subuser);
            System.Console.WriteLine(subUser.subUserId);
            _dbContext.Add<SubUser>(subUser);
            _dbContext.SaveChanges();
        }

        public void DeleteSubUserByID(String subUserID)
        {
            SubUser? subuser= _dbContext.Find<SubUser>(subUserID);
            if (subuser == null)
            {
                throw new Exception("Subuser does not exist");
            }
            _dbContext.Remove<SubUser>(subuser);
            _dbContext.SaveChanges();
        }

        public void RenameSubUserByID(String subUserID,String name)
        {
            SubUser? subuser = _dbContext.Find<SubUser>(subUserID);
            if (subuser == null)
            {
                throw new Exception("Subuser does not exist");
            }
            subuser.subUserName = name;
            _dbContext.SaveChanges();
        }

        public String[] GetSubUsersIdByID(String email)
        {
            var Subusers = from b in _dbContext.SubUser
                        where b.User.email.Equals(email)
                        select b.subUserId;
            return Subusers.ToArray();


        }

        public SubUser[] GetSubUsersByID(String email)
        {
            var Subusers = from b in _dbContext.SubUser
                           where b.User.email.Equals(email)
                           select b;
            return Subusers.ToArray();


        }

        public Boolean IsSubUserBelongToEmail(int subUserId,string email)
        {
            SubUser? subuser = _dbContext.SubUser
                       .Where(b => b.subUserId.Equals(subUserId))
                       .Include(e => e.User)
                       .FirstOrDefault();
            if(subuser == null)
            {
                throw new Exception("No such subuser");
            }
            return subuser.User.email.Equals(email);
        }

        public Spending[] GetAllSpendingBySubUser(String subUserId)
        {
            Spending[] spendings = _dbContext.Spending
                        .Where(b => b.subUser.subUserId.Equals(subUserId))
                        .ToArray();
            return spendings;
        }



    }
}
