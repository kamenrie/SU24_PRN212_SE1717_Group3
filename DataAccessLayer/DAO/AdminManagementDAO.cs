using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilites;

namespace DataAccessLayer.DAO
{
    public class AdminManagementDAO(ApplicationDbContext DbContext)
    {
        public int GetCountAccount()
        {
            return DbContext.Account.Count();
        }

        public Account GetAccountBuyMost()
        {
            DateTime firstDay = new(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);
            var accountId = DbContext.Order
                .Include(x => x.Account)
                .Include(x => x.Status)
                .Where(x => x.OrderDate >= firstDay && x.OrderDate <= lastDay && x.Status.Id == 3)
                .GroupBy(x => new { x.Account.Id, x.Quantity })
                .OrderByDescending(x => x.Sum(x => x.Quantity))
                .Select(x => x.Key.Id)
                .FirstOrDefault();
            var accountBuyMost = DbContext.Account.Include(x => x.Profile).FirstOrDefault(x => x.Id == accountId);
            if (accountBuyMost != null) {
                accountBuyMost.Profile.Image = ImgUtil.Decompress(accountBuyMost.Profile.Image);
            }
            return accountBuyMost;
        }

        public List<GeneralFeedback> GetAllFeedback()
        {
            var feedback = DbContext.GeneralFeedback
                .Include(x => x.Account)
                .ThenInclude(x => x.Profile)
                .Where(x => x.ResponseText == null && x.Ignored == false)
                .ToList();
            feedback.ForEach(x => x.Account.Profile.Image = ImgUtil.Decompress(x.Account.Profile.Image));
            return feedback;
        }

        public void SendFeedbackResponse(GeneralFeedback feedback, string response)
        {
            feedback = DbContext.GeneralFeedback.FirstOrDefault(x => x.Id == feedback.Id);
            feedback.ResponseText = response;
            feedback.ResponseDate = DateTime.Now;
            DbContext.GeneralFeedback.Update(feedback);
            DbContext.SaveChanges();
        }

        public void IgnoreFeedback(int Id)
        {
			var feedback = DbContext.GeneralFeedback.FirstOrDefault(x => x.Id == Id);
            feedback.Ignored = true;
			DbContext.GeneralFeedback.Update(feedback);
            DbContext.SaveChanges();
		}

	}
}
