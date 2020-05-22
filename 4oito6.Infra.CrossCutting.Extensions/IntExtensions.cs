using System.Net;

namespace _4oito6.Infra.CrossCutting.Extensions
{
    public static class IntExtensions
    {
        public static HttpStatusCode ToHttpStatusCode(this int i) => (HttpStatusCode)i;
    }
}