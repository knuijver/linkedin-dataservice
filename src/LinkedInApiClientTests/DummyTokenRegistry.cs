using System;
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

        public static DateTimeOffset TokenEntryTimestamp = new DateTimeOffset(2021, 01, 26, 0, 0, 0, TimeSpan.FromHours(0));
        public static TokenRegisterEntry TokenEntry = new TokenRegisterEntry
        {
            TokenId = null,
            AccessToken = "AQUFVymZgoizQFo51Mg-XRCfLKm_nqVOjhc3nrlaO73FEZ60ZECUw7aheQzraeUcNA1szOJec6lQWFWfmt-U-5Br55CGd1ZkmQDQvmlHxL0jiJmJLmz8xn-Do0jHuB7YTqj1zJPICqycSm835u0yIOuokwWECl9HDA2q6W4xbffz54-zIQ_JazAdyhT5Be4HsZQEaMOhB2Zmr11bJpqyOV1a6N64AZGnubQfWLwoTlhwvPJJMy604xWloteV9hWEXzVKDTBLVJfh4-q3h5KbvmfT6sBk1YYGVVaWNRjT8BXiW8QjeQRPNi-KR1Usr5JV2H1UeUG3QMfGLF6aqOAqkWKG3TTUAg",
            ExpiresIn = 5183999,
            RefreshToken = "AQXLYQ8lXhOInpLwcnnXGJbxxO1j6moIbKQFr1vbA6vA51NFgPBnQDmSmGA46QDUFBrPfR5En3Ypk6fSbWwS9D39eqqjPkZbZ8Z-cBTGWASD6gnla4Fp-i8mda6G3SDzSHvC13csCMx1pkfdicu1E3SABX98J7UNSTqGLpOOoL7DBkm9vSNtDEOOSmHrBG6NeptqFWGG4ozLA1bRwd2ZhlmjKf1krF51D1NuT8NeGchwNTit0MvWnLayJWk8_Zq8J8HJOrcDvfuAnca_yrtSqTaHia5aZyBvTDtBp5Xm9Kos5KK4TE_U6hSSsLhuTUUWzt88CFfpKiPAigeHZiZDGbIY5Ce1MA",
            RefreshTokenExpiresIn = 31535999,
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
