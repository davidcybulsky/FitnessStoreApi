using System.Security.Claims;

namespace Api.Interfaces
{
    public interface IHttpContextService
    {
        public int? GetUserId { get; }

        public ClaimsPrincipal? GetUser { get; }
    }
}
