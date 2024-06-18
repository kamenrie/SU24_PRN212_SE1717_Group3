using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SU24_PRN212_SE1717_Group3.DataAccess;
using SU24_PRN212_SE1717_Group3.Models;

namespace SU24_PRN212_SE1717_Group3.Dao
{
    public class AuthDao(ApplicationDbContext dbcontext)
    {
        public async Task<Account> Login(string email,string pass)
        {
            return await dbcontext.Account.FirstOrDefaultAsync(x => x.Email == email && x.Password == pass);
        }

        public async Task createAccount( Account account)
        {
            var role = dbcontext.Role.FirstOrDefault(x => x.Name == "user");
            var profile = new Profile();
            dbcontext.Profile.Add(profile);
            account.Role = role;
            account.Profile = profile;
            dbcontext.Account.Add(account);
            await dbcontext.SaveChangesAsync(); 
        }
    }
}
