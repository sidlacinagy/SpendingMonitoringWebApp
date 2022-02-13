using System.Configuration;
using System.Security.Cryptography;

namespace WebApplication3.Services
{
    public class AuthHelper
    {
        public static string GenerateSalt(int size)
        {
            var saltBytes = new byte[size];

            Random provider = new Random();
            provider.NextBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt, int nIterations, int nHash)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
            }
        }

        
    }
}
