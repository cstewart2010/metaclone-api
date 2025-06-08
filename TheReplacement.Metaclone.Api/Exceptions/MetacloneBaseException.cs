using System.Net;

namespace TheReplacement.Metaclone.Api.Exceptions
{
    public abstract class MetacloneBaseException : Exception
    {
        public abstract HttpStatusCode StatusCode { get; }

        public MetacloneBaseException(string message) : base(message) { }
    }
}
