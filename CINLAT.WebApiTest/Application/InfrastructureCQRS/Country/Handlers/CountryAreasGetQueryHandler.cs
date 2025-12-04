using System.Linq.Expressions;
using CINLAT.WebApiTest.Application.Core;
using CINLAT.WebApiTest.Application.DTOs;
using CINLAT.WebApiTest.Models.Business;
using CINLAT.WebApiTest.Persistence;
using MediatR;
using static CINLAT.WebApiTest.Application.InfrastructureCQRS.Country.Queries.CountryAreasGetQuery;

namespace CINLAT.WebApiTest.Application.InfrastructureCQRS.Country.Handlers
{
    public class CountryAreasGetQueryHandler
    : IRequestHandler<CountryAreasGetQueryRequest, Result<List<CountryAreasResponse>>>
    {
        private readonly MainContext _context;
        //private readonly IMapper _mapper;

        public CountryAreasGetQueryHandler(MainContext context)
        {
            _context = context;
            //_mapper = mapper;
        }


        /// <summary>
        /// Should return a list of pairs country:area of Pais table.
        /// </summary>
        /// <returns> Result<List<CountryAreasResponse>> </returns>
        public async Task<Result<List<CountryAreasResponse>>> Handle(CountryAreasGetQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Pais> queryable = _context.Paises!;
            List<CountryAreasResponse> listaTotal = queryable.Select(x => new CountryAreasResponse { commonName = x.commonName, area = x.area }).ToList();

            return Result<List<CountryAreasResponse>>.Success(listaTotal);

        }

 
    }
}
