using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SU24_PRN212_SE1717_Group3.DataAccess;
using SU24_PRN212_SE1717_Group3.Models;

namespace SU24_PRN212_SE1717_Group3.Dao
{
    public class AuthDao(ApplicationDbContext DbContext)
    {
        public async Task<Account> Login(string email,string pass)
        {
            return await DbContext.Account.FirstOrDefaultAsync(x => x.Email == email && x.Password == pass);
        }

        public async Task CreateAccount( Account account)
        {
            var role = DbContext.Role.FirstOrDefault(x => x.Name == "user");
            var profile = new Profile();
            DbContext.Profile.Add(profile);
            account.Role = role;
            account.Profile = profile;
            DbContext.Account.Add(account);
            await DbContext.SaveChangesAsync(); 
        }
    }
}
