using System;
using System.Threading.Tasks;
using Vk_clone.Errors.Request.Services.MailService;
using Vk_clone.Errors.Request.Services.TokenService;
using Vk_clone.Errors.Request.Services.UserService;

namespace Vk_clone.Errors.Request.Services.AuthService
{
    public class AuthService: IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        
        public AuthService(
            ITokenService tokenService,
            IUserService userService,
            IMailService mailService
        )
        {
            _tokenService = tokenService;
            _userService = userService;
            _mailService = mailService;
        }
        
        public async Task<ResponseType<AuthResponseInfo>>SignUp(SignupRequest signupRequest)
        {
            try
            {
                var user = await _userService.CreateUser(signupRequest);
                var (accessToken, refreshToken) = await _tokenService.CreateTokens(new TokenPayload(user.Id, user.Email));
                _mailService.SendSignUpMessage();
                
                return ResponseType<AuthResponseInfo>.Create(new AuthResponseInfo(accessToken, refreshToken));
            }
            catch (Exception exception)
            {
                string errorField = null;
                switch (exception)
                {
                    case EmailAlreadyExistsError:
                    {
                        errorField = nameof(signupRequest.Email).ToLower();
                        break;
                    }
                }
                
                if ( errorField != null)
                {
                    var error = new ErrorFieldResponse(exception.Message, errorField);
                    return ResponseType<AuthResponseInfo>.Create("Invalid data error", new [] {error});
                }
                
                
                throw exception;
            }
        }

        public async Task<ResponseType<AuthResponseInfo>> SignIn(SigninRequest signinRequest)
        {
            try
            {
                var user = await _userService.ValidateUser(signinRequest);
                var (accessToken, refreshToken) = await _tokenService.UpdateTokens(new TokenPayload(user.Id, user.Email));
                return ResponseType<AuthResponseInfo>.Create(new AuthResponseInfo(accessToken, refreshToken));
            }
            catch (Exception exception)
            {
                string errorField = null;
                switch (exception)
                {
                    case UserWithEmailNotFoundError:
                    {
                        errorField = nameof(signinRequest.Email).ToLower();
                        break;
                    }
                    case IncorrectPasswordError:
                    {
                        errorField = nameof(signinRequest.Password).ToLower();
                        break;
                    }
                }

                if (errorField != null)
                {
                    var error = new ErrorFieldResponse(exception.Message, errorField);
                    return ResponseType<AuthResponseInfo>.Create("Invalid data error", new [] {error});
                }
                throw exception;
            }
        }

        public void ConfirmSignUp()
        {
            throw new NotImplementedException();
        }
        
        public void ResetPassword()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}