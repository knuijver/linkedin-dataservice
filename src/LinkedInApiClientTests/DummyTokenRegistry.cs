using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient;
using LinkedInApiClient.Types;

namespace LinkedInApiClientTests
{
    internal class DummyTokenRegistry : IAccessTokenRegistry
    {
        public static readonly string ValidTokenId = "262cb823-7bad-4155-93c9-d04ed481f27a";

        public static readonly RefreshAccessToken RefreshToken = new RefreshAccessToken
        {
            AccessToken = "BBBB2kXITHELmWblJigbHEuoFdfRhOwGA0QNnumBI8XOVSs0HtOHEU-wvaKrkMLfxxaB1O4poRg2svCWWgwhebQhqrETYlLikJJMgRAvH1ostjXd3DP3BtwzCGeTQ7K9vvAqfQK5iG_eyS-q-y8WNt2SnZKZumGaeUw_zKqtgCQavfEVCddKHcHLaLPGVUvjCH_KW0DJIdUMXd90kWqwuw3UKH27ki5raFDPuMyQXLYxkqq4mYU-IUuZRwq1pcrYp1Vv-ltbA_svUxGt_xeWeSxKkmgivY_DlT3jQylL44q36ybGBSbaFn-UU7zzio4EmOzdmm2tlGwG7dDeivdPDsGbj5ig",
            ExpiresIn = 86400,
            RefreshToken = "AQWAft_WjYZKwuWXLC5hQlghgTam-tuT8CvFej9-XxGyqeER_7jTr8HmjiGjqil13i7gMFjyDxh1g7C_G1gyTZmfcD0Bo2oEHofNAkr_76mSk84sppsGbygwW-5oLsb_OH_EXADPIFo0kppznrK55VMIBv_d7SINunt-7DtXCRAv0YnET5KroQOlmAhc1_HwW68EZniFw1YnB2dgDSxCkXnrfHYq7h63w0hjFXmgrdxeeAuOHBHnFFYHOWWjI8sLenPy_EBrgYIitXsAkLUGvZXlCjAWl-W459feNjHZ0SIsyTVwzAQtl5lmw1ht08z5Du-RiQahQE0sv89eimHVg9VSNOaTvw",
            RefreshTokenExpiresIn = 439200
        };

        public static TokenRegisterEntry TokenEntry = new TokenRegisterEntry
        {
            TokenId = null,
            AccessToken = "AQUk749Pl1o6wK65Voe0hg-7fcWySvy3cfA32NGZs9nHJsHva0kv7UuWbId8bf3QBQQLa3rc2brTk8e5z6zjM1wZolS8seLv0onSou9DeyJ1nBeiHJwhsNN74J_Jl7ThJeOgeSXCs9k0mFIrocsyyshOwuhRo1UTXdAn-HJmwSv8n_cb3ceddH8K07V5ac8hnjeMB4UIhrjSBhFDw3bDGGJYDsmYrogaS9nqCFdXOhV1zy2gBHyJ26tZszrwzuoNKQiioFNkMRJAKfKV1tFHaeEUOCm5pGtA-n40Cfsy-RZxKj9neIQfhewF7qhqPMyYhU-zYcWNESxiCbr2t9LkQAE3oPDy9A",
            ExpiresIn = "5183999",
            RefreshToken = "AQV0WKdrmw_7iygKd8eySpmR9-O9288My5owsu1bwiPKBdVC32DO_NaKx41NO1bMHGEQiiQHmMVToz2jqYr4lx8BG_s_euIIlQi-1Bp2MhU3XZj8pohNwdcVduJUkNqc2kth3-ZepZ4V1cw063R7kC2nDq69-Dj6sCP8uA4qq-ynPlrR29aG7vFCkBTeOd9SfXgMbNIwPHLNZTzVDveVazGUUHRD6tBcjYKH2UiuP4gOfTpzOD3uTBDfGczPdcNxlkGA5tW2NHx8nC7B34lI2PAPfxDEVh0SR_2ZJj6SvIGbgG9VuiemuL5YuNwMnbLefdUyG6U_MJnO8htXownVV7idUvQ5SQ",
            TokenType = null
        };

        public Task<Result<string, string>> AccessTokenAsync(string tokenId, CancellationToken cancellationToken)
        {
            if (ValidTokenId == tokenId)
                return Task.FromResult<Result<string, string>>(Result.Success(TokenEntry.AccessToken));
            else
                return Task.FromResult<Result<string, string>>(Result.Fail($"Invalid token id [{tokenId}]"));
        }

        public Task<Result<string, string>> UpdateAccessTokenAsync(string tokenId, string accessToken, string expiresIn, string refreshToken, CancellationToken cancellationToken)
        {
            return Task.FromResult<Result<string, string>>(Result.Success(string.Empty));
        }

        public static IAccessTokenRegistry Create() => new DummyTokenRegistry();
    }
}
