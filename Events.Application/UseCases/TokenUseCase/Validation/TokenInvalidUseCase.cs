using Events.Application.Interfaces.UseCase.Token;
using System.IdentityModel.Tokens.Jwt;

namespace Events.Application.UseCases.TokenUseCase.Validation
{
    public class TokenInvalidUseCase : ITokenInvalidUseCase
    {
        public bool Execute(JwtSecurityToken jwtToken)
        {
            return jwtToken == null || jwtToken.ValidTo < DateTime.UtcNow;
        }
    }
}
