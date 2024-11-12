using Events.Application.DTO.Participant;
using Events.Application.UseCases.ParticipantUseCase.Delete.Interfaces;
using Events.Application.UseCases.ParticipantUseCase.Get.Interfaces;
using Events.Application.UseCases.ParticipantUseCase.Insert.Interfaces;
using Events.Application.UseCases.ParticipantUseCase.Login.Interfaces;
using Events.Application.UseCases.ParticipantUseCase.Update.Interfaces;
using Events.Application.UseCases.TokenUseCase.Generate.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Events.WebApi.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantController : Controller
    {
        private readonly IGetAllParticipantUseCase getAllParticipantUseCase;
        private readonly IGetByIdParticipantUseCase getByIdParticipantUseCase;
        private readonly IGetByRefreshTokenParticipantUseCase getByRefreshTokenParticipantUseCase;
        private readonly IInsertParticipantUseCase insertParticipantUseCase;
        private readonly IUpdateParticipantUseCase updateParticipantUseCase;
        private readonly IDeleteParticipantUseCase deleteParticipantUseCase;
        private readonly IDeleteRefreshTokenParticipantUseCase deleteRefreshTokenParticipantUseCase;
        private readonly ILoginParticipantUseCase loginParticipantUseCase;
        private readonly ITokenGenerateUseCase tokenGenerateUseCase;

        public ParticipantController(IGetAllParticipantUseCase getAllParticipantUseCase, IGetByIdParticipantUseCase getByIdParticipantUseCase, 
            IGetByRefreshTokenParticipantUseCase getByRefreshTokenParticipantUseCase, 
            IInsertParticipantUseCase insertParticipantUseCase, IUpdateParticipantUseCase updateParticipantUseCase,
            IDeleteParticipantUseCase deleteParticipantUseCase, IDeleteRefreshTokenParticipantUseCase deleteRefreshTokenParticipantUseCase,
            ILoginParticipantUseCase loginParticipantUseCase, ITokenGenerateUseCase tokenGenerateUseCase)
        {
            this.getAllParticipantUseCase = getAllParticipantUseCase;
            this.getByIdParticipantUseCase = getByIdParticipantUseCase;
            this.getByRefreshTokenParticipantUseCase = getByRefreshTokenParticipantUseCase;
            this.insertParticipantUseCase = insertParticipantUseCase;
            this.updateParticipantUseCase = updateParticipantUseCase;
            this.deleteParticipantUseCase = deleteParticipantUseCase;
            this.deleteRefreshTokenParticipantUseCase = deleteRefreshTokenParticipantUseCase;
            this.loginParticipantUseCase = loginParticipantUseCase;
            this.tokenGenerateUseCase = tokenGenerateUseCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ParticipantAuthDTO dto, CancellationToken cancellationToken)
        {
            var tokens = await loginParticipantUseCase.ExecuteAsync(dto, cancellationToken);

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

        [HttpPut("refresh")]
        public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["refresh-token"];

            if (refreshToken is null)
            {
                throw new UnauthorizedAccessException("Refresh token doesn't exist");
            }

            var participant = await getByRefreshTokenParticipantUseCase.ExecuteAsync(refreshToken, cancellationToken);

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

                await deleteRefreshTokenParticipantUseCase.ExecuteAsync(participant, cancellationToken);

                throw new UnauthorizedAccessException("Refresh token has expired");
            }

            var accessExpires = DateTime.UtcNow.AddMinutes(15);

            var newAccessToken = tokenGenerateUseCase.Execute(participant, accessExpires);
            var accessCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = accessExpires
            };

            Response.Cookies.Append("access-token", newAccessToken, accessCookieOptions);

            return Ok();
        }

        [Authorize("AdminOrUserPolicy")]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["refresh-token"];

            if (refreshToken is null)
            {
                throw new UnauthorizedAccessException("Logout has already completed");
            }

            var participant = await getByRefreshTokenParticipantUseCase.ExecuteAsync(refreshToken, cancellationToken);

            if (participant is null)
            {
                Response.Cookies.Delete("access-token");
                Response.Cookies.Delete("refresh-token");

                return Ok();
            }

            await deleteRefreshTokenParticipantUseCase.ExecuteAsync(participant, cancellationToken);

            Response.Cookies.Delete("access-token");
            Response.Cookies.Delete("refresh-token");

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await getAllParticipantUseCase.ExecuteAsync(pageNumber, pageSize, cancellationToken));
        }

        [Authorize("AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await getByIdParticipantUseCase.ExecuteAsync(id, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipant([FromBody] CreateParticipantDTO participant, CancellationToken cancellationToken)
        {
            await insertParticipantUseCase.ExecuteAsync(participant, cancellationToken);

            return Ok();
        }

        [Authorize("UserPolicy")]
        [HttpPut]
        public async Task<IActionResult> UpdateParticipant([FromBody] UpdateParticipantDTO participant, CancellationToken cancellationToken)
        {
            await updateParticipantUseCase.ExecuteAsync(participant, cancellationToken);

            return Ok();
        }

        [Authorize("AdminOrUserPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id, CancellationToken cancellationToken)
        {
            await deleteParticipantUseCase.ExecuteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
