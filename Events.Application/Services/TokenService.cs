using Events.Application.Services.Interfaces;
using Events.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Events.Application.Services
{
    public class TokenService : ITokenService
    {
        public string Generate(Participant participant)
        {
            Claim[] claims = [new("participantId", participant.Id.ToString()), new("participantName", participant.Name + " " + participant.Surname)];

            var jwt = new JwtSecurityToken(
                   issuer: "EventServer",
                   audience: "EventClient",
                   notBefore: DateTime.UtcNow,
                   expires: DateTime.UtcNow.AddMinutes(15),
                   claims: claims,
                   signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thHxx1uPmtZYc7LYY1fIbx4t2SPTNf7AeONVQJPNQb0B")),
                                                                                       SecurityAlgorithms.HmacSha256)
            );


            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}
