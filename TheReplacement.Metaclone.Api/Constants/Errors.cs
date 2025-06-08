namespace TheReplacement.Metaclone.Api.Constants
{
    public static class Errors
    {
        public const string MediaDataNullError = "Media data cannot be null";
        public const string MissingTitleError = "Title must not be null or empty";
        public const string MissingTagsError = "Tags must not be null or empty";
        public const string NewMediaHasNoReviewError = "All new media require exactly one review";
        public const string ReviewNullError = "Review cannot be null";
        public const string ScoreOutofRangeError = "Scores must be bounded between 0 and 100";
        public const string MissingUserError = "User must not be null or empty";
        public static readonly string InvalidPlatformError = $"Platform must be one of {string.Join(", ", Enum.GetNames<Platform>())}";
    }
}
