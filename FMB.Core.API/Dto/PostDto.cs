namespace FMB.Core.API.Dto
{
    public class PostDto
    {
        public long PostId { get; set; }
        public string Author { get; set; }
        public long AuthorId { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public List<CommentDto> Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
