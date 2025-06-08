using Microsoft.AspNetCore.Mvc;
using TheReplacement.Metaclone.Api.Constants;
using TheReplacement.Metaclone.Api.Models;
using TheReplacement.Metaclone.Api.Services;

namespace TheReplacement.Metaclone.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/media")]
    public class MediaController : ControllerBase
    {
        private readonly ILogger<MediaController> _logger;
        private readonly IRepository _repositiory;

        public MediaController(ILogger<MediaController> logger, IRepository repository)
        {
            _logger = logger;
            _repositiory = repository;
        }

        [HttpGet(Name = "GetMediaById")]
        public async Task<GetMediaResponse> GetMedia([FromQuery]int id)
        {
            return await _repositiory.GetMedia(id);
        }

        [HttpGet("{page}", Name = "GetMediaByPlatform")]
        public async Task<GetPageResponse> GetMediaByPlatform([FromQuery]string title, [FromQuery]Platform[] platforms, int page)
        {
            return await _repositiory.GetMediaByMetadata(title, platforms, page);
        }

        [HttpPost(Name = "AddMedia")]
        public async Task<PostMediaResponse> AddMedia([FromBody]PostMediaRequest request)
        {
            return await _repositiory.AddMedia(request);
        }

        [HttpPatch(Name = "AddReview")]
        public async Task<PatchMediaResponse> AddReview(PatchMediaRequest request)
        {
            return await _repositiory.AddReview(request);
        }
    }
}
