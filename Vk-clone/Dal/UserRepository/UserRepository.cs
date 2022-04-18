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

        public async Task<UserModel> CreateUser(CreateUserDto createUserDto)
        {
            using
                var conn = new MySqlConnection(_databaseConnectionOptions.ConnectionString);
            await conn.OpenAsync();

            var cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO user (email, passwordHash) VALUES (@email, @passwordHash)";
            cmd.Parameters.AddWithValue("email", createUserDto.Email);
            cmd.Parameters.AddWithValue("passwordHash", createUserDto.Password);
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = @"SELECT * FROM user WHERE email = @email";
            var rdr = await cmd.ExecuteReaderAsync();
            rdr.Read();
            
            return new UserModel((uint)rdr["id"], rdr["email"].ToString(), rdr["passwordHash"].ToString());
        }

        public void GetUser()
        {
            throw new System.NotImplementedException();
        }
    }
}