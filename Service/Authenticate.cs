using IdentityModel.Client;


namespace Service
{
    public class Authenticate
    {
        public async Task<TokenResponse> GetAccessTokenAsync(string username, string password)
        {
            var client = new HttpClient();
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "https://your-identity-server.com/connect/token",
                ClientId = "client_id",
                ClientSecret = "client_secret",
                UserName = username,
                Password = password,
                Scope = "api1"
            });

            return tokenResponse;
        }
    }
}
