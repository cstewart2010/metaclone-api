using TheReplacement.Metaclone.Api.Constants;

namespace TheReplacement.Metaclone.Api.Models
{
    public class MediaData
    {
        public required string Title { get; init; }
        public required ICollection<Review> Reviews { get; init; }
        public Platform Platform { get; init; }
        public required ICollection<string> Tags { get; init; }
        public double? Average => Reviews?.Average(x => x.Score);
    }
}
