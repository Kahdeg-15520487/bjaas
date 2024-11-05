using bjaas.ApiService.Business.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace bjaas.ApiService.Data
{
    public class BlackjackDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> RefreshTokens { get; set; }
    }
}
