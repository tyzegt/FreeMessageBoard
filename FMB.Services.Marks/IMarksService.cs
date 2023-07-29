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
        Task RatePostAsync(long postId, MarkEnum newMark, long userId);
    }
}
