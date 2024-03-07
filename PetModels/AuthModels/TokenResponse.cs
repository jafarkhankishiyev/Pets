using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AuthModels
{
    public class TokenResponse
    {
        public Guid Token { get; }
        public DateTime ValidUntil { get; }
        public TokenResponse(Guid token, DateTime validUntil)
        {
            Token = token;
            ValidUntil = validUntil;
        }
    }
}
