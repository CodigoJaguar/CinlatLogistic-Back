namespace CINLAT.WebApiTest.Application.Interfaces
{
    public interface ICountriesApi
    {
        Task<List<string>> GetCountriesAsync();
    }
}
