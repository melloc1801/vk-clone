using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Vk_clone.Errors.Request.Dal.UserRepository;

namespace Vk_clone.Errors.Request.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<UserModel> CreateUser(SignupRequest signupRequest)
        {
            var candidate = await _userRepository.FindOneByEmail(signupRequest.Email);
            if (candidate != null)
            {
                throw new EmailAlreadyExistsError(signupRequest.Email);
            }
            
            string hashedPassword = hashPassword(signupRequest.Password);
            
            var user = new SignupRequest(signupRequest.Email, hashedPassword);
            return await _userRepository.CreateUser(user);
        }
        public async Task<UserModel> ValidateUser(SigninRequest signinRequest)
        {
            var candidate = await _userRepository.FindOneByEmail(signinRequest.Email);
            if (candidate == null)
            {
                throw new UserWithEmailNotFoundError(signinRequest.Email);
            }

            var hashedPassword = hashPassword(signinRequest.Password);
            if (hashedPassword != candidate.Password)
            {
                throw new IncorrectPasswordError();
            }

            return candidate;
        }
        private string hashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 3,
                numBytesRequested: 256 / 8
            ));
        }
    }
}