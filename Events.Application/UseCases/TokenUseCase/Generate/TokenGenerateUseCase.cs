using Events.Application.DTO.Participant;
using Events.Application.UseCases.TokenUseCase.Generate.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Events.Application.UseCases.TokenUseCase.Generate
{
    public class TokenGenerateUseCase : ITokenGenerateUseCase
    {
        public string Execute(ParticipantDTO participant, DateTime expiresTime)
        {
            Claim[] claims =
            [
                new("participantEmail", participant.Email),
                new("participantName", participant.Name + " " + participant.Surname),
                new("Role", participant.Role.ToString())
            ];

            var jwt = new JwtSecurityToken(
                   issuer: "EventServer",
                   audience: "EventClient",
                   notBefore: DateTime.UtcNow,
                   expires: expiresTime,
                   claims: claims,
                   signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thHxx1uPmtZYc7LYY1fIbx4t2SPTNf7AeONVQJPNQb0B")),
                                                                                       SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}
