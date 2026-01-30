using FinanceAPI.Application.DTOs;
using FinanceAPI.Application.UseCases.Transactions;
using FinanceAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.Controllers;

[ApiController]
[Route("api/transactions")]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly CreateTransactionUseCase _create;
    private readonly GetAccountTransactionsUseCase _getByAccount;

    public TransactionsController(
        CreateTransactionUseCase create,
        GetAccountTransactionsUseCase getByAccount)
    {
        _create = create;
        _getByAccount = getByAccount;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionDto dto)
    {
        var userId = User.GetUserId();
        await _create.ExecuteAsync(userId, dto);
        return Created("", null);
    }

    [HttpGet("account/{accountId:guid}")]
    public async Task<IActionResult> GetByAccount(Guid accountId)
    {
        var userId = User.GetUserId();
        var result = await _getByAccount.ExecuteAsync(userId, accountId);
        return Ok(result);
    }
}
