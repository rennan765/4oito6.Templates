using _4oito6.Domain.Application.Core.Contracts.Arguments;
using _4oito6.Template.Api.Controllers.Base;
using _4oito6.Template.Domain.Application.Contracts.Interfaces;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Request;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace _4oito6.Template.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
            : base(new IDisposable[] { userAppService })
        {
            _userAppService = userAppService ?? throw new ArgumentNullException(nameof(userAppService));
        }

        /// <summary>
        /// Cria um novo usuário. Caso seja informado um telefone, deve ser um telefone válido. Caso seja informado um endereço, deve ser um endereço válido. O CPF e e-mail devem ser informações válidas. Caso o e-mail informado já esteja cadastrado, não será possível prosseguir com o cadastro.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseMessage<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage<UserResponse>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<UserResponse>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRequest request)
        {
            var response = await _userAppService.CreateUserAsync(request).ConfigureAwait(false);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Atualiza um usuário existente, informando o Id no request. O Id informado deve referir-se a um usuário cadastrado. As regras de validação dos dados são as mesmas utilizadas para criar um novo usuário.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseMessage<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage<UserResponse>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<LoginResponse>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ResponseMessage<UserResponse>), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ResponseMessage<UserResponse>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseMessage<UserResponse>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserRequest request)
        {
            var response = await _userAppService.UpdateUserAsync(request).ConfigureAwait(false);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Efetua o login do usuário de acordo com o seu e-mail
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseMessage<LoginResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage<LoginResponse>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<LoginResponse>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseMessage<LoginResponse>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [AllowAnonymous]
        [Route("[controller]/login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var result = await _userAppService.LoginAsync(request).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Efetua o login de acordo com o refresh token (obtido do header X-refreshToken).
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseMessage<LoginResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage<LoginResponse>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [AllowAnonymous]
        [Route("[controller]/refresh")]
        public async Task<IActionResult> RefreshLoginAsync()
        {
            Request.Headers.TryGetValue("X-refreshToken", out var refreshToken);

            var result = await _userAppService.RefreshLoginAsync(refreshToken).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }
    }
}