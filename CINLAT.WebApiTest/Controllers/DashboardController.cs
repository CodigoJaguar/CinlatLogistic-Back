using CINLAT.WebApiTest.Application.Core;
using CINLAT.WebApiTest.Application.DTOs;
using CINLAT.WebApiTest.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static CINLAT.WebApiTest.Application.InfrastructureCQRS.Country.Queries.CountryAreasGetQuery;
using static CINLAT.WebApiTest.Application.InfrastructureCQRS.Country.Queries.CountryPopulationGetQuery;

namespace CINLAT.WebApiTest.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly ISender _sender;

        public DashboardController(ISender sender)
        {
            _sender = sender;
        }

        
        [HttpGet]
        [Route("area")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<List<CountryAreasResponse>>>> GetCountryAreas(
            CancellationToken cancellationToken
        )
        {
            var query = new CountryAreasGetQueryRequest { };
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();
        }

        
        [HttpGet]
        [Route("population")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<List<CountryPopulationResponse>>>> GetCountryPopulation(
            CancellationToken cancellationToken
        )
        {
            var query = new CountryPopulationGetQueryRequest { };
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();
        }

    }
}
