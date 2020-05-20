using _4oito6.Contact.Api.Controllers.Base;
using _4oito6.Contact.Domain.Application.Contracts.Interfaces;
using _4oito6.Contact.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Domain.Application.Core.Contracts.Arguments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace _4oito6.Contact.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : BaseController
    {
        private readonly IContactAppService _contactAppService;

        public AddressController(IContactAppService contactAppService)
            : base(new IDisposable[] { contactAppService })
        {
            _contactAppService = contactAppService ?? throw new ArgumentNullException(nameof(contactAppService));
        }

        /// <summary>
        /// Obtém o endereço do usuário logado
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseMessage<AddressResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetFromUser()
        {
            var result = await _contactAppService.GetUserAddressAsync().ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Obtém todos os telefones com o bairro e cidade informados
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseMessage<AddressResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("[controller]/{district}/{city}")]
        public async Task<IActionResult> GetByDistrictAndCity(string district, string city)
        {
            var result = await _contactAppService.GetAddressByDistrictAndCityAsync(district, city).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Obtém o endereço (rua, bairro, cidade e estado) de acordo com o CEP através de um webservice.
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseMessage<AddressResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("[controller]/ws/{postalCode}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFromWebServiceByPostalCodeAsync(string postalCode)
        {
            var result = await _contactAppService.GetFromWebServiceByPostalCodeAsync(postalCode).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }
    }
}