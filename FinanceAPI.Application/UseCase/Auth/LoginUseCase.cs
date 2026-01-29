using FinanceAPI.Application.DTOs;
using FinanceAPI.Application.Interfaces;

namespace FinanceAPI.Application.UseCases.Auth;

public class LoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwt;

    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwt)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwt = jwt;
    }

    public async Task<LoginResponseDto> ExecuteAsync(LoginRequestDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user is null)
            throw new Exception("Invalid credentials");

        var ok = _passwordHasher.Verify(dto.Password, user.PasswordHash);
        if (!ok)
            throw new Exception("Invalid credentials");

        var token = _jwt.Generate(user);

        return new LoginResponseDto { Token = token };
    }
}
