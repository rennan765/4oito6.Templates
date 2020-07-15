using _4oito6.Contact.Api.Controllers.Base;
using _4oito6.Contact.Domain.Application.Contracts.Interfaces;
using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Contracts.Arguments;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace _4oito6.Contact.Api.Controllers
{
    public class AddressController : ODataBaseController
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
        [ProducesResponseType(typeof(IQueryable<ViewAddress>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [EnableQuery]
        [HttpGet]
        [ODataRoute("address")]
        public async Task<IActionResult> GetFromUser()
        {
            return Ok(await _contactAppService.GetUserAddressAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Obtém todos os telefones com o bairro e cidade informados
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(IQueryable<ViewAddress>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [EnableQuery]
        [ODataRoute("address/{district}/{city}")]
        public async Task<IActionResult> GetByDistrictAndCity([FromODataUri] string district, [FromODataUri] string city)
        {
            return Ok(await _contactAppService.GetAddressByDistrictAndCityAsync(district, city).ConfigureAwait(false));
        }

        /// <summary>
        /// Obtém o endereço (rua, bairro, cidade e estado) de acordo com o CEP através de um webservice.
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(AddressFromPostalCodeResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [EnableQuery]
        [Route("address/ws/{postalCode}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFromWebServiceByPostalCodeAsync([FromODataUri] string postalCode)
        {
            return Ok(await _contactAppService.GetFromWebServiceByPostalCodeAsync(postalCode).ConfigureAwait(false));
        }
    }
}