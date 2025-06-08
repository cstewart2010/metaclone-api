namespace TheReplacement.Metaclone.Api.Models
{
    public class PatchMediaRequest
    {
        public required int Id { get; init; }
        public required Review Review { get; init; }
    }
}
