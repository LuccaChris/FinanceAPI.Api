using System.Security.Claims;

namespace FinanceAPI.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var sub = user.FindFirstValue(ClaimTypes.NameIdentifier)
                  ?? user.FindFirstValue("sub");

        if (string.IsNullOrWhiteSpace(sub))
            throw new Exception("ID do usuário não encontrado no token.");

        return Guid.Parse(sub);
    }
}
