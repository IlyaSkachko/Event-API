using System.IdentityModel.Tokens.Jwt;

namespace Events.Application.Interfaces.UseCase.Token
{
    public interface ITokenInvalidUseCase
    {
        bool Execute(JwtSecurityToken jwtToken);
    }
}
