using CINLAT.WebApiTest.Application.Core;
using CINLAT.WebApiTest.Application.DTOs;
using MediatR;

namespace CINLAT.WebApiTest.Application.InfrastructureCQRS.Country.Queries
{
    public class CountryAreasGetQuery
    {
        public record CountryAreasGetQueryRequest : IRequest<Result<List<CountryAreasResponse>>>{ };
    }
}
