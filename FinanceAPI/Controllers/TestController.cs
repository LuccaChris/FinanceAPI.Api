using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet("private")]
    [Authorize]
    public IActionResult Private()
        => Ok(new { message = "Você está autenticado ✅" });
}
