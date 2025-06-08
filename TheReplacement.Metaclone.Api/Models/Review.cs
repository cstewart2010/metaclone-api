namespace TheReplacement.Metaclone.Api.Models
{
    public class Review
    {
        public required int Score { get; init; }
        public string? Description { get; init; }
        public required string User {  get; init; }
    }
}
