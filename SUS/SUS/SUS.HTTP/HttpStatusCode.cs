using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public enum HttpStatusCode
    {
        Ok = 200,
        MovedPermanently = 301,
        Found = 302,
        TemporaryRedirect = 307,
        NotFound = 404,
        ServerError = 500,

    }
}
