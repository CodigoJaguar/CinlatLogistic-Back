using CINLAT.WebApiTest.Application.Core;
using CINLAT.WebApiTest.Application.DTOs;
using CINLAT.WebApiTest.Models.Business;
using CINLAT.WebApiTest.Persistence;
using MediatR;
using static CINLAT.WebApiTest.Application.InfrastructureCQRS.Country.Queries.CountryPopulationGetQuery;

namespace CINLAT.WebApiTest.Application.InfrastructureCQRS.Country.Handlers
{
    public class CountryPopulationGetQueryHandler
    : IRequestHandler<CountryPopulationGetQueryRequest, Result<List<CountryPopulationResponse>>>
    {
        private readonly MainContext _context;
        //private readonly IMapper _mapper;

        public CountryPopulationGetQueryHandler(MainContext context)
        {
            _context = context;
            //_mapper = mapper;
        }
        public async Task<Result<List<CountryPopulationResponse>>> Handle(CountryPopulationGetQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Pais> queryable = _context.Paises!;
            List<CountryPopulationResponse> listaTotal = queryable.Where(x => x.population > 140000000).Select(x => new CountryPopulationResponse { commonName = x.commonName, population = x.population }).ToList();

            return Result<List<CountryPopulationResponse>>.Success(listaTotal);
        }
    }
}
