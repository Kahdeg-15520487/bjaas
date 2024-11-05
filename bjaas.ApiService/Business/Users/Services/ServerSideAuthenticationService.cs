using BitzArt.Blazor.Auth;
using bjaas.ApiService.Business.Users.Services.Contracts;
using bjaas.Shared;
using System.Security.Claims;
using System.Text;

namespace bjaas.ApiService.Business.Users.Services
{
    public class LocalServerSideAuthenticationService
        : ServerSideAuthenticationService<SignInPayload, SignUpPayload>
    {
        private readonly JwtService jwtService;
        private readonly IUsersService userService;

        public LocalServerSideAuthenticationService(JwtService jwtService, IUsersService userService) : base()
        {
            this.jwtService = jwtService;
            this.userService = userService;
        }

        protected override Task<AuthenticationResult> GetSignInResultAsync(SignInPayload signInPayload)
        {
            var user = userService.GetUser(signInPayload.Username, signInPayload.Password);

            if (user == null)
            {
                return Task.FromResult(AuthenticationResult.Failure("Invalid username or password."));
            }

            var jwtPair = jwtService.BuildJwtPair(user.Username, user.Id.ToString());

            var authResult = AuthenticationResult.Success(jwtPair);

            return Task.FromResult(authResult);
        }

        protected override Task<AuthenticationResult> GetSignUpResultAsync(SignUpPayload signUpPayload)
        {
            var user = userService.CreateUser(signUpPayload.Username, signUpPayload.Password);

            if (user == null)
            {
                return Task.FromResult(AuthenticationResult.Failure("Invalid username or password."));
            }

            var jwtPair = jwtService.BuildJwtPair(user.Username, user.Id.ToString());

            var authResult = AuthenticationResult.Success(jwtPair);

            return Task.FromResult(authResult);
        }

        public override Task<AuthenticationResult> RefreshJwtPairAsync(string refreshToken)
        {
            var user = userService.GetUserByRefreshToken(refreshToken);

            if (user == null)
            {
                return Task.FromResult(AuthenticationResult.Failure("Invalid refresh token."));
            }

            userService.DeleteUserRefreshToken(refreshToken);

            // Generate a new JWT pair using the userId
            var jwtPair = jwtService.RefreshJwtPair(refreshToken);

            if (jwtPair == null)
            {
                return Task.FromResult(AuthenticationResult.Failure("Invalid refresh token."));
            }

            userService.CreateUserRefreshToken(user.Id, jwtPair.RefreshToken);

            var authResult = AuthenticationResult.Success(jwtPair);

            return Task.FromResult(authResult);
        }
    }
}
