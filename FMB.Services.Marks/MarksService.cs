using FMB.Services.Marks.Models;
using FMB.Core.Data.Models.Marks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMB.Core.Data.Models.Enums;

namespace FMB.Services.Marks
{
    public class MarksService : IMarksService
    {
        private readonly MarksContext _context;
        public MarksService(MarksContext context)
        {
            _context = context;
        }

        public async Task RatePost(long postId, MarkEnum newMark, long userId)
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

        public async Task<RatingDto> GetPostRating(long postId)
        {
            var allVotes = await _context.PostMarks.Where(x => x.PostId == postId).ToListAsync();
            return new RatingDto()
            {
                EntityId = postId,
                Type = RateableEntityType.Post,
                UpVotes = allVotes.Count(x => x.Mark == MarkEnum.UpVote),
                DownVotes = allVotes.Count(x => x.Mark == MarkEnum.DownVote)
            };
        }

        public async Task<RatingDto> GetCommentRating(long commentId)
        {
            var allVotes = await _context.CommentMarks.Where(x => x.CommentId == commentId).ToListAsync();
            return new RatingDto()
            {
                EntityId = commentId,
                Type = RateableEntityType.Comment,
                UpVotes = allVotes.Count(x => x.Mark == MarkEnum.UpVote),
                DownVotes = allVotes.Count(x => x.Mark == MarkEnum.DownVote)
            };
        }

        public async Task RateComment(long commentId, MarkEnum newMark, long userId)
        {
            var mark = _context.CommentMarks.FirstOrDefault(p => p.UserId == userId && p.CommentId == commentId);
            if (mark == null)
            {
                mark = new CommentMark { CommentId = commentId, UserId = userId, Mark = newMark };
                _context.CommentMarks.Add(mark);
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

        public async Task<List<RatingDto>> GetCommentsRating(List<long> commentIds)
        {
            var allVotes = await _context.CommentMarks.Where(x => commentIds.Contains(x.CommentId)).ToListAsync();
            var result = new List<RatingDto>();
            foreach (var commentId in commentIds)
            {
                result.Add(new RatingDto
                {
                    EntityId = commentId,
                    Type = RateableEntityType.Comment,
                    UpVotes = allVotes.Where(x => x.CommentId == commentId).Count(x => x.Mark == MarkEnum.UpVote),
                    DownVotes = allVotes.Where(x => x.CommentId == commentId).Count(x => x.Mark == MarkEnum.DownVote)
                });
            }
            return result;
        }
    }
}
