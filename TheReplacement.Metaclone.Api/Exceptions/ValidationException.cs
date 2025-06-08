using System.Net;

namespace TheReplacement.Metaclone.Api.Exceptions
{
    public class ValidationException : MetacloneBaseException
    {
        public ValidationException(ICollection<string> errors) : base("Request failed validation")
        {
            Errors = errors;
        }
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public ICollection<string> Errors { get; }
    }
}
