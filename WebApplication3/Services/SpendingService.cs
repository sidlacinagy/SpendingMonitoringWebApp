using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication3.Model;

namespace WebApplication3.Services
{
    public class SpendingService
    {
        private readonly SpendingAppDbContext _dbContext;

        public SpendingService(SpendingAppDbContext context)
        {

            _dbContext = context;

        }

        public void AddSpendingToSubUser(String subUserId,String product,String productCategory,int price,DateTime date)
        {
            SubUser? subUser = _dbContext.Find<SubUser>(subUserId);
            if (subUser == null)
            {
                throw new Exception("No subuser with given id");
            }
            Spending spending = new Spending(subUser,product,productCategory,price,date);
            _dbContext.Add<Spending>(spending);
            _dbContext.SaveChanges();
        }

        public void RemoveSpendingById(String email,String id)
        {
            Spending? spending = _dbContext.Find<Spending>(id);

            if (!IsSpendingBelongsToUser(email, id) || spending == null) 
            {
                throw new Exception("Invalid spending id");
            }
            
            _dbContext.Remove<Spending>(spending);
            _dbContext.SaveChanges();
        }

        public String[] GetCategories(String subuserid)
        {
           
            List<String> categories = _dbContext.Spending
                          .Where(e => e.subUser.subUserId==subuserid)
                         .GroupBy(e => e.productCategory)
                         .Select(grp => grp.First().productCategory)
                         .ToList();
            categories.Add("Food");
            categories.Add("Clothing");
            categories.Add("Fuel");

            return categories.Distinct().ToArray();
        }


        public void UpdateSpendingById(String email, String id, String product, String productCategory, int price, DateTime date)
        {

            Spending? spending = _dbContext.Find<Spending>(id);

            if (!IsSpendingBelongsToUser(email, id) || spending == null)
            {
                throw new Exception("Invalid spending id");
            }
    

            spending.product = product;
            spending.productCategory = productCategory;
            spending.price = price;
            spending.date = date;
            _dbContext.SaveChanges();

        }

        private bool IsSpendingBelongsToUser(String email, String id)
        {
            String? trueEmail = _dbContext.Spending
                        .Where(b => b.Id.Equals(id))
                        .Select(b => b.subUser.User.email)
                        .FirstOrDefault();

            return email.Equals(trueEmail);

        }

        public Spending[] GetAllSpendingBySubUser(String subUserId)
        {
            Spending[] spendings = _dbContext.Spending
                        .Where(b => b.subUser.subUserId.Equals(subUserId))
                        .ToArray();
            return spendings;
        }

        public Spending[] GetAllSpendingByUser(String email)
        {
            Spending[] spendings = _dbContext.Spending
                         .Where(b => b.subUser.User.email.Equals(email))
                         .ToArray();
            return spendings;

        }

        public Spending[] GetAllSpendingBySubUserAndPage(String subuserid, int page)
        {
            int numberOfSpendingsReturned = 50;
            int startIndex = (page - 1) * numberOfSpendingsReturned;
            Spending[] spendings = _dbContext.Spending
                         .OrderBy(b => b.date)
                         .Skip(startIndex)
                         .Take(numberOfSpendingsReturned)
                         .ToArray();
            return spendings;
        }

        public Spending[] GetAllSpendingByQuery(String subuserid, DateTime mindate, DateTime maxdate,
            int minprice, int maxprice, List<String> categories,int page, String orderby)
        {
            int numberOfSpendingsReturned = 30;
            int startIndex = (page - 1) * numberOfSpendingsReturned;
            Func<Spending, object> orderByFunction= b => b.date;
            switch (orderby.ToLower())
            {
                case "price":
                    orderByFunction = b => b.price;
                    break;
                case "date":
                    orderByFunction = b => b.date;
                    break;
                case "category":
                    orderByFunction = b => b.productCategory;
                    break;
                default:
                    orderByFunction = b => b.date;
                    break;
            }
            Spending[] spendings = new Spending[] {};
            if (categories.Count > 0) {
                          spendings = _dbContext.Spending
                         .Where(b =>(b.date>=mindate && b.date<=maxdate && 
                          categories.Contains(b.productCategory) && 
                          b.price>minprice && b.price<maxprice && b.subUser.subUserId==subuserid))
                         .OrderBy(orderByFunction)
                         .Skip(startIndex)
                         .Take(numberOfSpendingsReturned)
                         .ToArray(); 
            }
            else
            {
                          spendings = _dbContext.Spending
                         .Where(b => (b.date > mindate && b.date < maxdate &&
                          b.price > minprice && b.price < maxprice && b.subUser.subUserId == subuserid))
                         .OrderBy(orderByFunction)
                         .Skip(startIndex)
                         .Take(numberOfSpendingsReturned)
                         .ToArray();
            }
            
            return spendings;
        }

    }
}
