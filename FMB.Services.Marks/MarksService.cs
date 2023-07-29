using FMB.Services.Marks.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Services.Marks
{
    public class MarksService : IMarksService
    {
        private readonly MarksContext _context;
        public MarksService(MarksContext context)
        {
            _context = context;
        }

        public async Task RatePostAsync(long postId, MarkEnum newMark, long userId)
        {
            var mark = _context.PostMarks.FirstOrDefault(p => p.UserId == userId && p.PostId == postId);
            if (mark == null)
            {
                mark = new PostMark { PostId = postId, UserId = userId, Mark = newMark };
                _context.PostMarks.Add(mark);
            }
            else if (mark.Mark == newMark)
            {
                _context.Remove(mark);
            }
            else
            {
                mark.Mark = newMark;
            }

            await _context.SaveChangesAsync();
        }
    }
}
