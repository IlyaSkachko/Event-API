using Events.Application.DTO.Participant;
using Events.Application.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Events.Application.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateAccessToken(ParticipantDTO participant, DateTime expiresTime)
        {
            return Generate(participant, expiresTime);
        }

        public string GenerateRefreshToken(ParticipantDTO participant, DateTime expiresTime)
        {
            return Generate(participant, expiresTime);
        }

        private string Generate(ParticipantDTO participant, DateTime expiresTime)
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
