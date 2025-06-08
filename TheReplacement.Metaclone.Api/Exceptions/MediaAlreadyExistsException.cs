using System.Net;
using TheReplacement.Metaclone.Api.Constants;

namespace TheReplacement.Metaclone.Api.Exceptions
{
    public class MediaAlreadyExistsException : MetacloneBaseException
    {
        public MediaAlreadyExistsException(string title, Platform platform) : base($"'({platform}) {title}' already exists") { }
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}
