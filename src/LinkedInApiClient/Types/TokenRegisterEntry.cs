using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.Types
{
    public class TokenRegisterEntry
    {
        public string TokenId { get; set; }
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshTokenExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
