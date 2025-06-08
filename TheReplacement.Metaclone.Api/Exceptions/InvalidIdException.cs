using System.Net;

namespace TheReplacement.Metaclone.Api.Exceptions
{
    public class InvalidIdException : MetacloneBaseException
    {
        public InvalidIdException(int id) : base($"No media found with id '{id}'") { }

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
