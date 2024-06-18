using SU24_PRN212_SE1717_Group3.DataAccess;
using Microsoft.EntityFrameworkCore;
using SU24_PRN212_SE1717_Group3.Models;
using Microsoft.AspNetCore.Http;
using Utilites;

namespace SU24_PRN212_SE1717_Group3.Dao
{
    public class AccountDAO(ApplicationDbContext dbcontext)
    {
        public async Task<Account> getAccountById(int? Id)
        {
            var account = await dbcontext.Account.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == Id);
            if (account != null)
            {
                account.Profile.Image = ImgUtil.Decompress(account.Profile.Image);
            }
            return account;
        }
        public async Task UpdateProfile(Profile pro, IFormFile img, Account account)
        {
            account.Profile.Name = pro.Name;
            account.Profile.Image = ImgUtil.Compress(ImgUtil.ConvertIFormFileToByte(img)) ?? account.Profile.Image;
            account.Profile.Phone = pro.Phone;
            account.Profile.Address = pro.Address;
            dbcontext.Update(account.Profile);
            await dbcontext.SaveChangesAsync();
        }
    }
}
