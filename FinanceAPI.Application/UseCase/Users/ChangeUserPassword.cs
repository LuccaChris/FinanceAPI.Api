using FinanceAPI.Application.Security;
using FinanceAPI.Domain.Repositories;

namespace FinanceAPI.Application.UseCase.Users;

public class ChangeUserPassword
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public ChangeUserPassword(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task ExecuteAsync(Guid userId, string newPassword, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct);
        if (user is null)
            throw new Exception("Usuário não encontrado.");

        var hash = _passwordHasher.Hash(newPassword);

        user.ChangePassword(hash);

        await _userRepository.UpdateAsync(user, ct);
    }
}
