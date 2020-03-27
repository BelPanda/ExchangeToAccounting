using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Routing;

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
        result += "<p>UserAgent: " + context.Request.UserAgent + "</p>"
                  + "<p>IISVersion: " + HttpRuntime.IISVersion + "</p>"
                  + "<p>TargetFramework: " + HttpRuntime.TargetFramework + "</p>";


        int loop1;
        NameValueCollection coll;

        //Load Form variables into NameValueCollection variable.
        coll = context.Request.Form;
        // Get names of all forms into a string array.
        String[] arr1 = coll.AllKeys;
        for (loop1 = 0; loop1 < arr1.Length; loop1++)
        {
            context.Response.Write("Form: " + arr1[loop1] + "<br>");
        }

        //context.Response.Write(result);
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Test"].ConnectionString);
        connection.Open();
        using (SqlCommand command = new SqlCommand("[dbo].[Procedure]",connection))
        {
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    context.Response.Write(reader.GetString(reader.GetOrdinal("Result")));
                }

            }
        }

    }
    public bool IsReusable
    {
        get { return false; }
    }

    private void ResponsTestInfo(HttpContext context)
    {
        string result = "";
        result += "<p>AcceptTypes: " + context.Request.AcceptTypes + "</p>"
             + "<p>AnonymousID: " + context.Request.AnonymousID
             + "<p>ApplicationPath: " + context.Request.ApplicationPath + "</p>"
             + "<p>AppRelativeCurrentExecutionFilePath: " + context.Request.AppRelativeCurrentExecutionFilePath +
             "</p>"
             + "<p>Browser: " + context.Request.Browser.Browser + "</p>"
             + "<p>ClientCertificate: " + context.Request.ClientCertificate + "</p>"
             + "<p>ContentEncoding: " + context.Request.ContentEncoding + "</p>"
             + "<p>ContentLength: " + context.Request.ContentLength + "</p>"
             + "<p>ContentType: " + context.Request.ContentType + "</p>"
             + "<p>Cookies: " + context.Request.Cookies + "</p>"
             + "<p>CurrentExecutionFilePath: " + context.Request.CurrentExecutionFilePath + "</p>"
             + "<p>CurrentExecutionFilePathExtension: " + context.Request.CurrentExecutionFilePathExtension +
             "</p>"
             + "<p>FilePath: " + context.Request.FilePath + "</p>"
             + "<p>Files: " + context.Request.Files + "</p>"
             + "<p>Filter: " + context.Request.Filter + "</p>"
             + "<p>Form: " + context.Request.Form + "</p>"
             + "<p>Headers: " + context.Request.Headers + "</p>"
             + "<p>HttpChannelBinding: " + context.Request.HttpChannelBinding + "</p>"
             + "<p>HttpMethod: " + context.Request.HttpMethod + "</p>"
             + "<p>InputStream: " + context.Request.InputStream + "</p>"
             + "<p>IsAuthenticated: " + context.Request.IsAuthenticated + "</p>"
             + "<p>IsLocal: " + context.Request.IsLocal + "</p>"
             + "<p>IsSecureConnection: " + context.Request.IsSecureConnection + "</p>"
             + "<p>LogonUserIdentity: " + context.Request.LogonUserIdentity + "</p>"
             + "<p>Params: " + context.Request.Params + "</p>"
             + "<p>Path: " + context.Request.Path + "</p>"
             + "<p>PathInfo: " + context.Request.PathInfo + "</p>"
             + "<p>PhysicalApplicationPath: " + context.Request.PhysicalApplicationPath + "</p>"
             + "<p>PhysicalPath: " + context.Request.PhysicalPath + "</p>"
             + "<p>QueryString: " + context.Request.QueryString + "</p>"
             + "<p>RawUrl: " + context.Request.RawUrl + "</p>"
             + "<p>ReadEntityBodyMode: " + context.Request.ReadEntityBodyMode + "</p>"
             + "<p>RequestContext: " + context.Request.RequestContext + "</p>"
             + "<p>RequestType: " + context.Request.RequestType + "</p>"

             + "<p>TimedOutToken: " + context.Request.TimedOutToken + "</p>"
             + "<p>TlsTokenBindingInfo: " + context.Request.TlsTokenBindingInfo + "</p>"
             + "<p>TotalBytes: " + context.Request.TotalBytes + "</p>"
             + "<p>Unvalidated: " + context.Request.Unvalidated + "</p>"
             + "<p>Url: " + context.Request.Url + "</p>"
             + "<p>UrlReferrer: " + context.Request.UrlReferrer + "</p>"
             + "<p>UserAgent: " + context.Request.UserAgent + "</p>"
             + "<p>UserHostAddress: " + context.Request.UserHostAddress + "</p>"
             + "<p>UserHostName: " + context.Request.UserHostName + "</p>"
             + "<p>UserLanguages: " + context.Request.UserLanguages + "</p>";
        //+"<p>ServerVariables: " + context.Request.ServerVariables + "</p>"
        string ServerVariables = "<p>ServerVariables: ";
        foreach (var str in context.Request.ServerVariables)
        {
            ServerVariables += str + ", ";
        }
        int loop1, loop2;
        NameValueCollection coll;

        // Load ServerVariable collection into NameValueCollection object.
        coll = context.Request.ServerVariables;
        // Get names of all keys into a string array. 
        String[] arr1 = coll.AllKeys;
        for (loop1 = 0; loop1 < arr1.Length; loop1++)
        {
            context.Response.Write("Key: " + arr1[loop1] + "<br>");
            String[] arr2 = coll.GetValues(arr1[loop1]);
            for (loop2 = 0; loop2 < arr2.Length; loop2++)
            {
                context.Response.Write("Value " + loop2 + ": " + context.Server.HtmlEncode(arr2[loop2]) + "<br>");
            }
        }
        result += ServerVariables.Remove(ServerVariables.Length - 2, 2);

        context.Response.Write(result);

    }
}
