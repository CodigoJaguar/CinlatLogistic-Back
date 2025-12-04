using System.Net;
using CINLAT.WebApiTest.Application.Core;
using CINLAT.WebApiTest.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CINLAT.WebApiTest.Application.InfrastructureCQRS.Country.Queries.CountryAreasGetQuery;

namespace CINLAT.WebApiTest.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly ISender _sender;

        public DashboardController(ISender sender)
        {
            _sender = sender;
        }


        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<List<CountryAreasResponse>>>> GetCountryAreas(
            CancellationToken cancellationToken
        )
        {
            var query = new CountryAreasGetQueryRequest { };
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();
        }
    }
}
