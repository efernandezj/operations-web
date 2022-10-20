using DataLayer.DTOs;
using DataLayer.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DataLayer.Services
{
    public interface ISecurityService
    {
        public Task<User> GetUser(string username);
        public PasswordHash Hash(string text, string _salt);
    }


    public class SecurityService : ISecurityService
    {
        private readonly ApplicationDbContext context;

        public SecurityService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUser(string username)
        {
            return await context.Users.Include(e => e.Employee).FirstOrDefaultAsync(x => x.Username == username);

        }

        public PasswordHash Hash (string text, string _salt)
        {
            byte[] salt = Encoding.UTF8.GetBytes(_salt);
            var keyDerivation = KeyDerivation.Pbkdf2(password: text, salt, KeyDerivationPrf.HMACSHA1, iterationCount: 10000, numBytesRequested: 32);

            return new PasswordHash() { Hash = Convert.ToBase64String(keyDerivation), Salt = salt };
        }
    }
}
