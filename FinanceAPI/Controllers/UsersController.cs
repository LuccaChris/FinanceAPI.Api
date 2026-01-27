using FinanceAPI.Application.DTOs;
using FinanceAPI.Application.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly CreateUserUseCase _createUserUseCase;

    public UsersController(CreateUserUseCase createUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        await _createUserUseCase.ExecuteAsync(dto);
        return Created("", null);
    }
}
