namespace bjaas.ApiService.Business.Users.Models
{
    public class UserRefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
