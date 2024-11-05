using bjaas.ApiService.Business.Users.Models;

namespace bjaas.ApiService.Business.Users.Services.Contracts
{
    public interface IUsersService
    {
        User? CreateUser(string username, string password);
        User? GetUser(string username, string password);
        User? GetUserByRefreshToken(string refreshToken);
        void CreateUserRefreshToken(Guid id, string? refreshToken);
        void DeleteUserRefreshToken(string refreshToken);
    }
}
