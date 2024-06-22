using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Utilites;

namespace DataAccessLayer.DAO
{
    public class AccountDAO(ApplicationDbContext DbContext)
    {
        public async Task<Account> GetAccountById(int? Id)
        {
            var account = await DbContext.Account.Include(x => x.Profile).Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == Id);
            if (account != null)
            {
                account.Profile.Image = ImgUtil.Decompress(account.Profile.Image);
            }
            return account;
        }
        public async Task UpdateProfile(Profile pro, IFormFile img, Account account)
        {
            account.Profile.Name = pro.Name;
            account.Profile.Image = ImgUtil.Compress(img) ?? account.Profile.Image;
            account.Profile.Phone = pro.Phone;
            account.Profile.Address = pro.Address;
            DbContext.Update(account.Profile);
            await DbContext.SaveChangesAsync();
        }
    }
}
