using FinanceAPI.Application.DTOs;
using FinanceAPI.Application.Interfaces;
using FinanceAPI.Domain.Entities;

namespace FinanceAPI.Application.UseCases.Users;

public class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task ExecuteAsync(CreateUserDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser is not null)
            throw new Exception("E-mail já em uso");

        var passwordHash = _passwordHasher.Hash(dto.Password);

        var user = new User(dto.Username, dto.Email, passwordHash);

        await _userRepository.AddAsync(user);
    }
}
