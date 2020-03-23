using System.Web;
using System.Web.Routing;

namespace ExchangeToAccounting
{
    public class MyRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new HttpHandler();
        }
    }
    public class HttpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string result = "<p>Ваш IP: " + context.Request.UserHostAddress + "</p>";
            result += "<p>UserAgent: " + context.Request.UserAgent + "</p>";
            context.Response.Write(result);
        }
        public bool IsReusable
        {
            get { return false; }
        }
    }
}