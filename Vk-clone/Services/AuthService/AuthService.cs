using System;
using System.Threading.Tasks;
using Vk_clone.Dal.TokenRepository;
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
        private readonly ITokenRepository _tokenRepository;
        
        public AuthService(
            ITokenService tokenService,
            IUserService userService,
            IMailService mailService,
            ITokenRepository tokenRepository
        )
        {
            _tokenService = tokenService;
            _userService = userService;
            _mailService = mailService;
            _tokenRepository = tokenRepository;
        }
        
        public async Task<SignUpResponse> SignUp(CreateUserDto createUserDto)
        {
            var user = await _userService.CreateUser(createUserDto);
            var (accessToken, refreshToken) = _tokenService.CreateToken(new TokenPayload(user.Id, user.Email));
            _mailService.SendSignUpMessage();
            
            return new SignUpResponse(user.Email, accessToken, refreshToken);
        }
        
        public void SignIn()
        {
            throw new System.NotImplementedException();
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