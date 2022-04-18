using System.Threading.Tasks;
using MySqlConnector;

namespace Vk_clone.Dal.TokenRepository
{
    public class TokenRepository: ITokenRepository
    {
        private readonly DatabaseConnectionOptions _databaseConnectionOptions;

        public TokenRepository(DatabaseConnectionOptions databaseConnectionOptions)
        {
            _databaseConnectionOptions = databaseConnectionOptions;
        }

        public async Task<string> CreateRefreshToken(string token, uint userId)
        {
            using 
                var conn = new MySqlConnection(_databaseConnectionOptions.ConnectionString);
            await conn.OpenAsync();
            
            var cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO token (user_id, token) VALUES (@user_id, @token)";
            cmd.Parameters.AddWithValue("user_id", userId);
            cmd.Parameters.AddWithValue("token", token);
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"SELECT token FROM token WHERE user_id = @user_id";
            var rdr = await cmd.ExecuteReaderAsync();
            rdr.Read();

            return rdr["token"].ToString();
        }

        public async Task<string> UpdateRefreshToken(uint id, string token)
        {
            using
                var conn = new MySqlConnection(_databaseConnectionOptions.ConnectionString);
            await conn.OpenAsync();

            var cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"UPDATE token SET token = @token WHERE user_id = @user_id";
            cmd.Parameters.AddWithValue("user_id", id);
            cmd.Parameters.AddWithValue("token", token);
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = @"SELECT token FROM token WHERE user_id = @user_id";
            var rdr = await cmd.ExecuteReaderAsync();
            rdr.Read();
            
            return rdr["token"].ToString();
        }

        public async Task<uint> DeleteRefreshToken(uint id)
        {
            using
                var conn = new MySqlConnection(_databaseConnectionOptions.ConnectionString);
            await conn.OpenAsync();

            var cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"DELETE FROM token WHERE user_id = @user_id";
            cmd.Parameters.AddWithValue("user_id", id);
            cmd.ExecuteNonQuery();

            return id;
        }
    }
}