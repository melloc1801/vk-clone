using System;
using System.Threading.Tasks;
using MySqlConnector;
using Vk_clone.Dtos;
using Vk_clone.Models;

namespace Vk_clone.Dal.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConnectionOptions _databaseConnectionOptions;

        public UserRepository(DatabaseConnectionOptions databaseConnectionOptions)
        {
            _databaseConnectionOptions = databaseConnectionOptions;
        }

        public async Task<UserModel> FindOneByEmail(string email)
        {
            using
                var conn = new MySqlConnection(_databaseConnectionOptions.ConnectionString);
            await conn.OpenAsync();
            
            var cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * FROM user WHERE email = @email";
            cmd.Parameters.AddWithValue("email", email);
            var rdr = await cmd.ExecuteReaderAsync();
            if (rdr.Read())
            {
                return new UserModel((uint)rdr["id"], (string)rdr["email"], (string)rdr["passwordHash"]);
            }
            return null;
        }

        public async Task<UserModel> CreateUser(SignupDto signupDto)
        {
            using
                var conn = new MySqlConnection(_databaseConnectionOptions.ConnectionString);
            await conn.OpenAsync();

            var cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO user (email, passwordHash) VALUES (@email, @passwordHash)";
            cmd.Parameters.AddWithValue("email", signupDto.Email);
            cmd.Parameters.AddWithValue("passwordHash", signupDto.Password);
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = @"SELECT * FROM user WHERE email = @email";
            var rdr = await cmd.ExecuteReaderAsync();
            rdr.Read();
            
            return new UserModel((uint)rdr["id"], (string)rdr["email"], (string)rdr["passwordHash"]);
        }

        public void GetUser()
        {
            throw new System.NotImplementedException();
        }
    }
}