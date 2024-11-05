using bjaas.ApiService.Business.Users.Models;
using bjaas.ApiService.Data;

namespace bjaas.ApiService.Business.Users.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(BlackjackDBContext context) : base(context)
        {
        }

        public User? GetUser(string username, string password)
        {
            return (base.Query(u => u.Username == username && u.PasswordHash == password)).Result.FirstOrDefault();
        }
    }
}
