using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Vk_clone.Dal.UserRepository;
using Vk_clone.Dtos;
using Vk_clone.Models;

namespace Vk_clone.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<UserModel> CreateUser(SignupDto signupDto)
        {
            var candidate = await _userRepository.FindOneByEmail(signupDto.Email);

            if (candidate != null)
            {
                var exception = new Exception("Email already exists");
                exception.Data["ErrorCode"] = ErrorCodes.EmailAlreadyExists;
                throw exception;
            }
            
            string hashedPassword = hashPassword(signupDto.Password);
            
            var user = new SignupDto(signupDto.Email, hashedPassword);
            return await _userRepository.CreateUser(user);
        }

        public async Task<UserModel> ValidateUser(SigninDto signinDto)
        {
            var candidate = await _userRepository.FindOneByEmail(signinDto.Email);
            if (candidate == null)
            {
                var exception = new Exception("Email already exists");
                exception.Data["ErrorCode"] = ErrorCodes.EmailAlreadyExists;
                throw exception;
            }

            var hashedPassword = hashPassword(signinDto.Password);
            if (hashedPassword != candidate.Password)
            {
                var exception = new Exception("Email already exists");
                exception.Data["ErrorCode"] = ErrorCodes.IncorrectPasswordException;
                throw exception;
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