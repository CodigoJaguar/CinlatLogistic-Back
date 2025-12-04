using CINLAT.WebApiTest.Application.Core;
using CINLAT.WebApiTest.Application.DTOs;
using MediatR;

namespace CINLAT.WebApiTest.Application.InfrastructureCQRS.Country.Queries
{
    public class CountryPopulationGetQuery
    {
        public record CountryPopulationGetQueryRequest : IRequest<Result<List<CountryPopulationResponse>>> { };
    }
}
