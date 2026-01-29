using FinanceAPI.Domain.Entities;

namespace FinanceAPI.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}
