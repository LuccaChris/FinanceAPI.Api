using FinanceAPI.Application.DTOs;
using FinanceAPI.Application.UseCases.Accounts;
using FinanceAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.Controllers;

[ApiController]
[Route("api/accounts")]
[Authorize]
public class AccountsController : ControllerBase
{
    private readonly CreateAccountUseCase _createAccount;
    private readonly GetMyAccountsUseCase _getMyAccounts;

    public AccountsController(CreateAccountUseCase createAccount, GetMyAccountsUseCase getMyAccounts)
    {
        _createAccount = createAccount;
        _getMyAccounts = getMyAccounts;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAccountDto dto)
    {
        var userId = User.GetUserId();
        await _createAccount.ExecuteAsync(userId, dto);
        return Created("", null);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMine()
    {
        var userId = User.GetUserId();
        var result = await _getMyAccounts.ExecuteAsync(userId);
        return Ok(result);
    }
}
