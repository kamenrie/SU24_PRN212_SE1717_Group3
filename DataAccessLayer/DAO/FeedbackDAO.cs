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
	public class FeedbackDAO(ApplicationDbContext DbContext)
	{
		public async Task<List<GeneralFeedback>> GetAllFeedback()
		{
			var ListFeedback = await DbContext.GeneralFeedback
												.Include(x => x.Account)
												.ThenInclude(x => x.Profile)
												.ToListAsync();
			ListFeedback.ForEach(Feedback => { Feedback.Account.Profile.Image = ImgUtil.Decompress(Feedback.Account.Profile.Image); });
			return ListFeedback;
		}

		public async Task<List<GeneralFeedback>> GetAllFeedbackByAccount(Account account)
		{
			var ListFeedback = await DbContext.GeneralFeedback
												.Include(x => x.Account)
												.ThenInclude(x => x.Profile)
												.Where(x => x.Account == account)
												.ToListAsync();
			ListFeedback.ForEach(Feedback => { Feedback.Account.Profile.Image = ImgUtil.Decompress(Feedback.Account.Profile.Image); });
			return ListFeedback;
		}

		public async Task AddFeedback(Account account, string comment)
		{
			var Feedback = new GeneralFeedback
			{
				FeedbackText = comment,
				FeedbackDate = DateTime.Now,
				Account = account,
				Ignored = false
			};
			await DbContext.GeneralFeedback.AddAsync(Feedback);
			await DbContext.SaveChangesAsync();
		}
	}
}
