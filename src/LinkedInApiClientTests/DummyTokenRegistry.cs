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
            AccessToken = "AQVqLUHWoWdJd-aWrtU1g4I-97jrWJjvTj2206Eeo7nmWuwhM_HzD0TfCZI-xTewEdUiQZwLI11Tbg8_0J_vDoj_SZWN2EmvrGLB2bHQFVQrfSoMSq7IEyMRy4HAQtRXFTFw2guxT3ZR7tTbqcXxivq0P7L_wLCJOmtLCgTiShGhVyecAKFQ9zPj5uzV6ZEyRq5qyzf-djg8BsZi6_ahtGk8udqDnx9CJfYd1nS8U7rE0UN1-Vk-jYMzmrWP49Qwf-xbAIgljndpnZvbNx9DMzIjfr3T2ozvIpVHTJud-tX1khd1Paa9IOy2qHaOg40tH93ecLhJi51KEPc1trqM6E-v6641Ng",
            ExpiresIn = 5183999,
            RefreshToken = "AQUy42CZFN51U4HGFbtPPsKrpD-m8B1W6ptUWhgmPhRJAf5-8bIYdBqxXwX-9cOiNcg91mfecFIIS6jjfQmHTYFVtf725XO1K499FG0dKJdx225JOTOtS8tywYMsurVwB6Z0BNthVhsb7DbEFNLRT0mFK9e5GMQ3edxVMBDS16snVPGCmzW_UrbacKGitQhaLt8DPC-DBviCxFCkshPCnSu1y7XPDhNiWQn-6h01mIU9L7t9L8N34q1NJ4UAGXMtrZa8Q9qF6umDzPfSbJS4JnAclkGmXkgZsCYU2O5hggsu6SVjJ7VBVJkUkg8HsaBi1cTtJSQ-0tqiUiwqm517xmrSlvd-bQ",
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
