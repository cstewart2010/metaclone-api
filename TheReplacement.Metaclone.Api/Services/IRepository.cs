using TheReplacement.Metaclone.Api.Constants;
using TheReplacement.Metaclone.Api.Models;

namespace TheReplacement.Metaclone.Api.Services
{
    public interface IRepository
    {
        Task<GetMediaResponse> GetMedia(int id);
        Task<GetPageResponse> GetMediaByMetadata(string title, Platform[] platforms, int page);
        Task<GetPageResponse> GetMediaByTag(Platform[] platforms, string tag, int page);
        Task<PostMediaResponse> AddMedia(PostMediaRequest request);
        Task<PatchMediaResponse> AddReview(PatchMediaRequest request);
    }
}
