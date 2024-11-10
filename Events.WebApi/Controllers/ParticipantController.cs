using Events.Application.DTO.Participant;
using Events.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Events.WebApi.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantController : Controller
    {
        private readonly IParticipantService participantService;
        private readonly ITokenService tokenService;

        public ParticipantController(IParticipantService participantService, ITokenService tokenService)
        {
            this.participantService = participantService;
            this.tokenService = tokenService;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] ParticipantAuthDTO dto, CancellationToken cancellationToken)
        {
            var tokens = await participantService.Login(dto, cancellationToken);

            var accessCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = tokens.AccessExpires
            };

            Response.Cookies.Append("access-token", tokens.Access, accessCookieOptions);

            var refreshCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = tokens.RefreshExpires
            };

            Response.Cookies.Append("refresh-token", tokens.Refresh, refreshCookieOptions);

            return Ok();
        }

        [HttpPut("/refresh")]
        public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["refresh-token"];

            if (refreshToken is null)
            {
                throw new UnauthorizedAccessException("Refresh token doesn't exist");
            }

            var participant = await participantService.GetByRefreshTokenAsync(refreshToken, cancellationToken);

            if (participant is null)
            {
                Response.Cookies.Delete("access-token");
                Response.Cookies.Delete("refresh-token");

                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadToken(refreshToken) as JwtSecurityToken;

            if (jwtToken == null || jwtToken.ValidTo < DateTime.UtcNow)
            {
                Response.Cookies.Delete("access-token");
                Response.Cookies.Delete("refresh-token");

                await participantService.DeleteRefreshTokenAsync(participant, cancellationToken);

                throw new UnauthorizedAccessException("Refresh token has expired");
            }

            var accessExpires = DateTime.UtcNow.AddMinutes(15);

            var newAccessToken = tokenService.GenerateAccessToken(participant, accessExpires);
            var accessCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = accessExpires
            };

            Response.Cookies.Append("access-token", newAccessToken, accessCookieOptions);

            return Ok();
        }

        [Authorize("AdminOrUserPolicy")]
        [HttpDelete("/logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["refresh-token"];

            if (refreshToken is null)
            {
                throw new UnauthorizedAccessException("Logout has already completed");
            }

            var participant = await participantService.GetByRefreshTokenAsync(refreshToken, cancellationToken);

            if (participant is null)
            {
                Response.Cookies.Delete("access-token");
                Response.Cookies.Delete("refresh-token");

                return Ok();
            }

            await participantService.DeleteRefreshTokenAsync(participant, cancellationToken);

            Response.Cookies.Delete("access-token");
            Response.Cookies.Delete("refresh-token");

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await participantService.GetAllAsync(pageNumber, pageSize, cancellationToken));
        }

        [Authorize("AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await participantService.GetByIdAsync(id, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipant([FromBody] CreateParticipantDTO participant, CancellationToken cancellationToken)
        {
            await participantService.InsertAsync(participant, cancellationToken);

            return Ok();
        }

        [Authorize("UserPolicy")]
        [HttpPut]
        public async Task<IActionResult> UpdateParticipant([FromBody] UpdateParticipantDTO participant, CancellationToken cancellationToken)
        {
            await participantService.UpdateAsync(participant, cancellationToken);

            return Ok();
        }

        [Authorize("AdminOrUserPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id, CancellationToken cancellationToken)
        {
            await participantService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
