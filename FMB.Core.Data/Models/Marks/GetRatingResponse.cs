using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Core.Data.Models.Marks
{
    public class GetRatingResponse
    {
        public int Rating { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }

        public static GetRatingResponse MapFrom(Tuple<int, int> model)
        {
            return new GetRatingResponse { 
                Upvotes = model.Item1, 
                Downvotes = model.Item2, 
                Rating = model.Item1 - model.Item2 
            };
        }
    }
}
