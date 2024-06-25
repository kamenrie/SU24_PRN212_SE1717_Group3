using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;

namespace DataAccessLayer.DAO
{
    public class AuthDao(ApplicationDbContext DbContext)
    {
        public async Task<Account> Login(string email,string pass)
        {
            return await DbContext.Account.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email && x.Password == pass);
        }

        public async Task CreateAccount(Account account)
        {
            var role = DbContext.Role.FirstOrDefault(x => x.Name == "User");
            var profile = new Profile();
            DbContext.Profile.Add(profile);
            account.Role = role;
            account.Profile = profile;
            DbContext.Account.Add(account);
            await DbContext.SaveChangesAsync(); 
        }

        public void ChangePassword(Account account, string newPassword)
        {
            account.Password = newPassword;
            DbContext.Account.Update(account);
            DbContext.SaveChanges();
        }
    }
}
