namespace bjaas.ApiService.Business.Users.Services
{
    using bjaas.ApiService.Business.Users.Models;
    using bjaas.ApiService.Business.Users.Repository;
    using bjaas.ApiService.Business.Users.Services.Contracts;
    using bjaas.ApiService.Data;
    using System.Security.Cryptography;
    using System.Text;

    public class UsersService : IUsersService
    {
        private readonly IUserRepository userRepository;
        private readonly string salt;

        public UsersService(IUserRepository userRepository, IUserRefreshTokenRepository userRefreshTokenRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.salt = configuration["Salt"]!;
        }

        public User? GetUser(string username, string password)
        {
            string saltedPassword = password + salt;
            string hashedPassword = ComputeSha256Hash(saltedPassword);

            return userRepository.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);
        }

        public User? CreateUser(string username, string password)
        {
            User user = new User();
            user.Username = username;
            string saltedPassword = password + salt;
            string hashedPassword = ComputeSha256Hash(saltedPassword);
            user.PasswordHash = hashedPassword;

            userRepository.Users.Add(user);
            userRepository.SaveChanges();

            return user;
        }

        public User? GetUserByRefreshToken(string refreshToken)
        {
            var userRefreshToken = userRepository.RefreshTokens.FirstOrDefault(rt => rt.RefreshToken == refreshToken);

            if (userRefreshToken == null)
            {
                return null;
            }

            return userRepository.Users.FirstOrDefault(u => u.Id == userRefreshToken.UserId);
        }

        public void CreateUserRefreshToken(Guid id, string? refreshToken)
        {
            UserRefreshToken userRefreshToken = new UserRefreshToken();
            userRefreshToken.UserId = id;
            userRefreshToken.RefreshToken = refreshToken;

            userRepository.RefreshTokens.Add(userRefreshToken);
            userRepository.SaveChanges();
        }

        public void DeleteUserRefreshToken(string refreshToken)
        {
            var userRefreshToken = userRepository.RefreshTokens.FirstOrDefault(rt => rt.RefreshToken == refreshToken);

            if (userRefreshToken != null)
            {
                userRepository.RefreshTokens.Remove(userRefreshToken);
                userRepository.SaveChanges();
            }
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
