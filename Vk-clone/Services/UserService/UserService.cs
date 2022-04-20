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
        
        public async Task<UserModel> CreateUser(CreateUserDto createUserDto)
        {
            var candidate = await _userRepository.FindOneByEmail(createUserDto.Email);

            if (candidate)
            {
                var exception = new Exception("Email already exists");
                exception.Data["ErrorCode"] = ErrorCodes.EmailAlreadyExists;
                throw exception;
            }

            byte[] salt = new byte[128 / 8];
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: createUserDto.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 3,
                numBytesRequested: 256 / 8
            ));
            
            var newUserDto = new CreateUserDto(createUserDto.Email, hashedPassword);
            
            return await _userRepository.CreateUser(newUserDto);
        }
    }
}