using System.Text.Json;
using System.Threading.Tasks;
using LinkedInApiClient;
using LinkedInApiClient.Authentication;
using LinkedInApiClient.Types;

namespace LinkedInApiClientTests
{
    internal class DummyAccessTokenRegistry : IAccessTokenRegistry
    {
        public static readonly RefreshAccessToken RefreshToken = new RefreshAccessToken
        {
            AccessToken = "BBBB2kXITHELmWblJigbHEuoFdfRhOwGA0QNnumBI8XOVSs0HtOHEU-wvaKrkMLfxxaB1O4poRg2svCWWgwhebQhqrETYlLikJJMgRAvH1ostjXd3DP3BtwzCGeTQ7K9vvAqfQK5iG_eyS-q-y8WNt2SnZKZumGaeUw_zKqtgCQavfEVCddKHcHLaLPGVUvjCH_KW0DJIdUMXd90kWqwuw3UKH27ki5raFDPuMyQXLYxkqq4mYU-IUuZRwq1pcrYp1Vv-ltbA_svUxGt_xeWeSxKkmgivY_DlT3jQylL44q36ybGBSbaFn-UU7zzio4EmOzdmm2tlGwG7dDeivdPDsGbj5ig",
            ExpiresIn = 86400,
            RefreshToken = "AQWAft_WjYZKwuWXLC5hQlghgTam-tuT8CvFej9-XxGyqeER_7jTr8HmjiGjqil13i7gMFjyDxh1g7C_G1gyTZmfcD0Bo2oEHofNAkr_76mSk84sppsGbygwW-5oLsb_OH_EXADPIFo0kppznrK55VMIBv_d7SINunt-7DtXCRAv0YnET5KroQOlmAhc1_HwW68EZniFw1YnB2dgDSxCkXnrfHYq7h63w0hjFXmgrdxeeAuOHBHnFFYHOWWjI8sLenPy_EBrgYIitXsAkLUGvZXlCjAWl-W459feNjHZ0SIsyTVwzAQtl5lmw1ht08z5Du-RiQahQE0sv89eimHVg9VSNOaTvw",
            RefreshTokenExpiresIn = 439200
        };

        public Task<Result<string, string>> AccessTokenAsync(string tokenId)
        {
            return Task.FromResult<Result<string, string>>(Result.Success(string.Empty));
        }

        public Task<Result<string, string>> UpdateAccessTokenAsync(string tokenId, string accessToken, string expiresIn, string refreshToken)
        {
            return Task.FromResult<Result<string, string>>(Result.Success(string.Empty));
        }

        public static IAccessTokenRegistry Create() => new DummyAccessTokenRegistry();
    }
}
