using FinanceAPI.Application.DTOs;
using FinanceAPI.Application.Interfaces;
using FinanceAPI.Domain.Entities;
using FinanceAPI.Domain.Enums;

namespace FinanceAPI.Application.UseCases.Users;

public class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserUseCase(
        IUserRepository userRepository,
        IAccountRepository accountRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task ExecuteAsync(CreateUserDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser is not null)
            throw new Exception("Email already in use");

        var passwordHash = _passwordHasher.Hash(dto.Password);

        // 1) cria usuário
        var user = new User(dto.Username, dto.Email, passwordHash);
        await _userRepository.AddAsync(user);

        // 2) cria conta automaticamente (padrão)
        var account = new Account(user.Id, "Conta Principal", AccountType.Checking);
        await _accountRepository.AddAsync(account);
    }
}