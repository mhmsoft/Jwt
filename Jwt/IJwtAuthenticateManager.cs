using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt
{
    public interface IJwtAuthenticateManager
    {
        string Authenticate(string userName, string password);
    }
}
