using _4oito6.Contact.Api.Controllers.Base;
using _4oito6.Contact.Domain.Application.Contracts.Interfaces;
using _4oito6.Contact.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Domain.Application.Core.Contracts.Arguments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace _4oito6.Contact.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneController : BaseController
    {
        private readonly IContactAppService _contactAppService;

        public PhoneController(IContactAppService contactAppService)
            : base(new IDisposable[] { contactAppService })
        {
            _contactAppService = contactAppService ?? throw new ArgumentNullException(nameof(contactAppService));
        }

        /// <summary>
        /// Obtém os telefones do usuário logado
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseMessage<IList<PhoneResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetFromUser()
        {
            var result = await _contactAppService.GetUserPhonesAsync().ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Obtém todos os telefones com o ddd informado
        /// </summary>
        /// <param name="localCode"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseMessage<IList<PhoneResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{localCode}")]
        public async Task<IActionResult> GetFromLocalCode(string localCode)
        {
            var result = await _contactAppService.GetPhonesByLocalCodeAsync(localCode).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }
    }
}