using Api.Interfaces;
using System.Security.Claims;

namespace Api.Services
{
    public class HttpContextService : IHttpContextService
    {
        private IHttpContextAccessor _httpContext;

        public HttpContextService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public int? GetUserId => GetUser is null ? null : (int)int.Parse(GetUser.FindFirstValue(ClaimTypes.NameIdentifier));

        public ClaimsPrincipal? GetUser => _httpContext?.HttpContext?.User;
    }
}
