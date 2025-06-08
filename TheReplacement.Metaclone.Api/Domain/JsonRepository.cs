using System;
using System.Text.Json;
using TheReplacement.Metaclone.Api.Constants;
using TheReplacement.Metaclone.Api.Exceptions;
using TheReplacement.Metaclone.Api.Models;
using TheReplacement.Metaclone.Api.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TheReplacement.Metaclone.Api.Domain
{
    internal class JsonRepository : IRepository
    {
        private static readonly string FilePath = Path.Combine(".", "metaclone.json");
        private readonly ICollection<Media> _repository;

        public JsonRepository()
        {
            if (File.Exists(FilePath))
            {
                var content = File.ReadAllText(FilePath);
                _repository = JsonSerializer.Deserialize<ICollection<Media>>(content)!;
            }
            else
            {
                _repository = new List<Media>();
                SaveFile();
            }
        }

        public async Task<PostMediaResponse> AddMedia(PostMediaRequest request)
        {
            return await Task.Run(() =>
            {
                ValidatePostMediaRequest(request);
                if (GetMediaFromRepository(request.Data.Title, request.Data.Platform) != null)
                {
                    throw new MediaAlreadyExistsException(request.Data.Title, request.Data.Platform);
                }
                var media = new Media
                {
                    Id = _repository.Count,
                    Data = request.Data,
                };
                _repository.Add(media);
                SaveFile();
                return new PostMediaResponse
                {
                    Media = media,
                };
            });
        }

        public async Task<PatchMediaResponse> AddReview(PatchMediaRequest request)
        {
            return await Task.Run(() =>
            {
                var media = GetMediaFromRepository(request.Id) ?? throw new InvalidIdException(request.Id);
                ValidatePatchMediaRequest(request);
                media.Data.Reviews.Add(request.Review);
                SaveFile();
                return new PatchMediaResponse
                {
                    Media = media
                };
            });
        }

        public async Task<GetMediaResponse> GetMedia(int id)
        {
            return await Task.Run(() =>
            {
                var media = GetMediaFromRepository(id) ?? throw new InvalidIdException(id);
                return new GetMediaResponse
                {
                    Media = media
                };
            });
        }

        public async Task<GetPageResponse> GetMediaByMetadata(string title, Platform[] platforms, int page)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(title))
                {
                    throw new ValidationException([Errors.MissingTitleError]);
                }
                if (platforms.Any(platform => !Enum.IsDefined(platform)))
                {
                    throw new ValidationException([Errors.InvalidPlatformError]);
                }
                var collection = _repository.Where(x => x.Data.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase));
                if (platforms.Any())
                {
                    collection = collection.Where(x => platforms.Contains(x.Data.Platform));
                }
                var media = collection.Skip((page - 1) * 10).Take(10);
                return new GetPageResponse
                {
                    Media = media
                };
            });
        }

        public async Task<GetPageResponse> GetMediaByTag(Platform[] platforms, string tag, int page)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(tag))
                {
                    throw new ValidationException([Errors.MissingTagsError]);
                }
                if (platforms.Any(platform => !Enum.IsDefined(platform)))
                {
                    throw new ValidationException([Errors.InvalidPlatformError]);
                }
                var collection = _repository.Where(x => x.Data.Tags.Contains(tag));
                if (platforms.Any())
                {
                    collection = collection.Where(x => platforms.Contains(x.Data.Platform));
                }
                var media = collection.Skip((page - 1) * 10).Take(10);
                return new GetPageResponse
                {
                    Media = media
                };
            });
        }

        #region Helper methods
        private void SaveFile()
        {
            var content = JsonSerializer.Serialize(_repository);
            File.WriteAllText(FilePath, content);
        }

        private Media? GetMediaFromRepository(int id)
        {
            return _repository.FirstOrDefault(x => x.Id == id);
        }
        private Media? GetMediaFromRepository(string title, Platform platform)
        {
            return _repository.FirstOrDefault(x => x.Data.Title == title && x.Data.Platform == platform);
        }

        private static void ValidatePostMediaRequest(PostMediaRequest request)
        {
            var data = request.Data ?? throw new ValidationException([Errors.MediaDataNullError]);
            var errors = new List<string>();
            if (!Enum.IsDefined(data.Platform))
            {
                errors.Add(Errors.InvalidPlatformError);
            }
            if (string.IsNullOrEmpty(data.Title))
            {
                errors.Add(Errors.MissingTitleError);
            }
            if (data.Reviews == null || data.Reviews.Count != 1)
            {
                errors.Add(Errors.NewMediaHasNoReviewError);
                throw new ValidationException(errors);
            }
            var firstReview = data.Reviews.First();
            if (string.IsNullOrEmpty(firstReview.User))
            {
                errors.Add(Errors.MissingUserError);
            }
            if (firstReview.Score < 0 || firstReview.Score > 100)
            {
                errors.Add(Errors.ScoreOutofRangeError);
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }

        private static void ValidatePatchMediaRequest(PatchMediaRequest request)
        {
            var review = request.Review ?? throw new ValidationException([Errors.ReviewNullError]);
            var errors = new List<string>();
            if (string.IsNullOrEmpty(review.User))
            {
                errors.Add(Errors.MissingUserError);
            }
            if (review.Score < 0 || review.Score > 100)
            {
                errors.Add(Errors.ScoreOutofRangeError);
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }
        #endregion
    }
}
