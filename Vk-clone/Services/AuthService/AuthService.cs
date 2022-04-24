using System;
using System.Threading.Tasks;
using Vk_clone.Models;
using Vk_clone.Services.AuthService.Dto;
using Vk_clone.Services.AuthService.Types;
using Vk_clone.Services.MailService;
using Vk_clone.Services.TokenService;
using Vk_clone.Services.UserService;
using Vk_clone.Services.UserService.Errors;
using Vk_clone.Types;

namespace Vk_clone.Services.AuthService
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
        
        public async Task<ResponseType<AuthResponseInfo>>SignUp(SignupDto signupDto)
        {
            try
            {
                var user = await _userService.CreateUser(signupDto);
                var (accessToken, refreshToken) = await _tokenService.CreateTokens(new TokenPayload(user.Id, user.Email));
                _mailService.SendSignUpMessage();
                
                return ResponseType<AuthResponseInfo>.Create(new AuthResponseInfo(accessToken, refreshToken));
            }
            catch (Exception exception)
            {
                string errorCode = null;
                switch (exception)
                {
                    case EmailAlreadyExistsError emailAlreadyExistsError:
                    {
                        errorCode = UserErrorCodes.EmailAlreadyExists.ToString();
                        break;
                    }
                }
                
                if (errorCode != null)
                {
                    return ResponseType<AuthResponseInfo>.Create(new [] {errorCode});
                }
                
                
                throw exception;
            }
        }

        public async Task<ResponseType<AuthResponseInfo>> SignIn(SigninDto signinDto)
        {
            try
            {
                var user = await _userService.ValidateUser(signinDto);
                var (accessToken, refreshToken) = await _tokenService.UpdateTokens(new TokenPayload(user.Id, user.Email));
                return ResponseType<AuthResponseInfo>.Create(new AuthResponseInfo(accessToken, refreshToken));
            }
            catch (Exception exception)
            {
                string errorCode = null;
                switch (exception)
                {
                    case UserWithEmailNotFoundError userWithEmailNotFoundError:
                    {
                        errorCode = UserErrorCodes.UserWithEmailNotFound.ToString();
                        break;
                    }
                    case IncorrectPasswordError incorrectPasswordError:
                    {
                        errorCode = UserErrorCodes.IncorrectPassword.ToString();
                        break;
                    }
                }

                if (errorCode != null)
                {
                    return ResponseType<AuthResponseInfo>.Create(new [] {errorCode});
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