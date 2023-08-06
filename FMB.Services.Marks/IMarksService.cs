using FMB.Services.Marks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Services.Marks
{
    public interface IMarksService
    {
        Task<Tuple<int, int>> GetCommentRating(long commentId);
        Task<Tuple<int, int>> GetPostRating(long postId);
        Task RateComment(long commentId, MarkEnum newMark, long userId);
        Task RatePost(long postId, MarkEnum newMark, long userId);
    }
}
