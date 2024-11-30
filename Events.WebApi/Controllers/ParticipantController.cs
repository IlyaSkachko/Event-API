using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Interfaces.UseCase.Hash;
using Events.Application.Interfaces.UseCase.Participant;
using Events.Application.Interfaces.UseCase.Token;
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
        private readonly IGetByEmailParticipantUseCase getByEmailParticipantUseCase;
        private readonly IInsertParticipantUseCase insertParticipantUseCase;
        private readonly IUpdateParticipantUseCase updateParticipantUseCase;
        private readonly IDeleteParticipantUseCase deleteParticipantUseCase;
        private readonly IDeleteRefreshTokenParticipantUseCase deleteRefreshTokenParticipantUseCase;
        private readonly ILoginParticipantUseCase loginParticipantUseCase;
        private readonly ITokenGenerateUseCase tokenGenerateUseCase;
        private readonly ITokenInvalidUseCase tokenInvalidUseCase;
        private readonly IHashPasswordUseCase hashPasswordUseCase;
        private readonly IVerifyPasswordUseCase verifyPasswordUseCase;
        private readonly IRefreshTokenParticipantUseCase refreshTokenParticipantUseCase;
        private readonly IMapper mapper;

        public ParticipantController(IGetAllParticipantUseCase getAllParticipantUseCase, IGetByIdParticipantUseCase getByIdParticipantUseCase, 
            IGetByRefreshTokenParticipantUseCase getByRefreshTokenParticipantUseCase,  IGetByEmailParticipantUseCase getByEmailParticipantUseCase,
            IInsertParticipantUseCase insertParticipantUseCase, IUpdateParticipantUseCase updateParticipantUseCase,
            IDeleteParticipantUseCase deleteParticipantUseCase, IDeleteRefreshTokenParticipantUseCase deleteRefreshTokenParticipantUseCase,
            ILoginParticipantUseCase loginParticipantUseCase, ITokenGenerateUseCase tokenGenerateUseCase, ITokenInvalidUseCase tokenInvalidUseCase,
            IHashPasswordUseCase hashPasswordUseCase, IVerifyPasswordUseCase verifyPasswordUseCase, IRefreshTokenParticipantUseCase refreshTokenParticipantUseCase,
            IMapper mapper)
        {
            this.getAllParticipantUseCase = getAllParticipantUseCase;
            this.getByIdParticipantUseCase = getByIdParticipantUseCase;
            this.getByRefreshTokenParticipantUseCase = getByRefreshTokenParticipantUseCase;
            this.getByEmailParticipantUseCase = getByEmailParticipantUseCase;
            this.insertParticipantUseCase = insertParticipantUseCase;
            this.updateParticipantUseCase = updateParticipantUseCase;
            this.deleteParticipantUseCase = deleteParticipantUseCase;
            this.deleteRefreshTokenParticipantUseCase = deleteRefreshTokenParticipantUseCase;
            this.loginParticipantUseCase = loginParticipantUseCase;
            this.tokenGenerateUseCase = tokenGenerateUseCase;
            this.tokenInvalidUseCase = tokenInvalidUseCase;
            this.hashPasswordUseCase = hashPasswordUseCase;
            this.verifyPasswordUseCase = verifyPasswordUseCase;
            this.refreshTokenParticipantUseCase = refreshTokenParticipantUseCase;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ParticipantAuthDTO participantDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            await loginParticipantUseCase.ExecuteAsync(HttpContext, participantDTO, cancellationToken);

            return Ok();
        }

        [HttpPut("refresh")]
        public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
        {
            await refreshTokenParticipantUseCase.ExecuteAsync(HttpContext, cancellationToken);

            return Ok();
        }

        [Authorize("AdminOrUserPolicy")]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["refresh-token"];

            var participant = await getByRefreshTokenParticipantUseCase.ExecuteAsync(refreshToken, cancellationToken);

            Response.Cookies.Delete("access-token");

            Response.Cookies.Delete("refresh-token");

            await deleteRefreshTokenParticipantUseCase.ExecuteAsync(participant, cancellationToken);

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            participant.Password = hashPasswordUseCase.Execute(participant.Password, out byte[] salt);

            await insertParticipantUseCase.ExecuteAsync(participant, salt, cancellationToken);

            return Ok();
        }

        [Authorize("UserPolicy")]
        [HttpPut]
        public async Task<IActionResult> UpdateParticipant([FromBody] UpdateParticipantDTO participant, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
