namespace CINLAT.WebApiTest.Application.Interfaces
{
    public interface IUserTabsPermissions
    {
        Task<List<string>> Permissions(string? username);

        Task<List<string>> GetSectionPermissions(string? username);
    }
}
