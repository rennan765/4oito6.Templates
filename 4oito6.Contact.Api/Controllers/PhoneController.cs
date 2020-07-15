using _4oito6.Contact.Api.Controllers.Base;
using _4oito6.Contact.Domain.Application.Contracts.Interfaces;
using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Domain.Application.Core.Contracts.Arguments;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace _4oito6.Contact.Api.Controllers
{
    public class PhoneController : ODataBaseController
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
        [ProducesResponseType(typeof(IQueryable<ViewPhone>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [EnableQuery]
        [ODataRoute("phone")]
        public async Task<IActionResult> GetFromUser()
        {
            return Ok(await _contactAppService.GetUserPhonesAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Obtém todos os telefones com o ddd informado
        /// </summary>
        /// <param name="localCode"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IQueryable<ViewPhone>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [EnableQuery]
        [ODataRoute("phone/{localCode}")]
        public async Task<IActionResult> GetFromLocalCode([FromODataUri] string localCode)
        {
            return Ok(await _contactAppService.GetPhonesByLocalCodeAsync(localCode).ConfigureAwait(false));
        }
    }
}