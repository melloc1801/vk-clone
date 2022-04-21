using System;
using System.Threading.Tasks;
using Vk_clone.Dal.TokenRepository;
using Vk_clone.Dal.UserRepository;
using Vk_clone.Dtos;
using Vk_clone.Models;
using Vk_clone.Services.MailService;

namespace Vk_clone.Services
{
    public class AuthService: IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        private readonly IUserRepository _userRepository;
        
        public AuthService(
            ITokenService tokenService,
            IUserService userService,
            IMailService mailService,
            IUserRepository userRepository
        )
        {
            _tokenService = tokenService;
            _userService = userService;
            _mailService = mailService;
            _userRepository = userRepository;
        }
        
        public async Task<SignupResponse> SignUp(SignupDto signupDto)
        {
            var user = await _userService.CreateUser(signupDto);
            var (accessToken, refreshToken) = await _tokenService.CreateTokens(new TokenPayload(user.Id, user.Email));
            _mailService.SendSignUpMessage();
            
            return new SignupResponse(user.Email, accessToken, refreshToken);
        }

        public async Task<SigninResponse> SignIn(SigninDto signinDto)
        {
            var user = await _userService.ValidateUser(signinDto);
            var (accessToken, refreshToken) = await _tokenService.UpdateTokens(new TokenPayload(user.Id, user.Email));
            return new SigninResponse(user.Email, accessToken, refreshToken);
        }

        public void ConfirmSignUp()
        {
            throw new System.NotImplementedException();
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